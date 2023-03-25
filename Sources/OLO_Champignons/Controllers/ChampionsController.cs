using Microsoft.AspNetCore.Mvc;
using Model;
using DTO;
using Api.Mapper;
using Api.Pagination;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OLO_Champignons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionsController : ControllerBase
    {
        public IDataManager dataManager;

        private readonly ILogger<ChampionsController> _logger;

        public ChampionsController(IDataManager d, ILogger<ChampionsController> log)
        {
            dataManager = d;
            _logger = log;
        }

        // GET: api/<Champion>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]PageRequest pageRequest)
        {
            _logger.LogInformation($"Request to get champions with index " +
                $"{pageRequest.Index} and count {pageRequest.Count}");

            if(pageRequest.Count > 10)
            {
                _logger.LogWarning("too many champions requested");
                // return erreur ?
            }
            try
            {
                var champions = (await dataManager.ChampionsMgr.GetItems(pageRequest.Index,
                    pageRequest.Count)).Select(champion => champion?.ToDto());

                var page = new Page()
                {
                    MyChampions = champions,
                    Index = pageRequest.Index,
                    Count = pageRequest.Count,
                    TotalCount = await dataManager.ChampionsMgr.GetNbItems()
                 };
                return Ok(page);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "error while trying to get champions");

                return BadRequest(ex.Message);
            }

            // return objet page 3 int + i champion: return Ok(paege)
        }

        
        // GET api/<Champion>/Akali
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            _logger.LogInformation($"Request to get champion with NAME: {name}");

            try
            {
               var champions = (await dataManager.ChampionsMgr.GetItems(0,
                (await dataManager.ChampionsMgr.GetNbItems()))).Select(champion => champion?.ToDto());

                ChampionDto? champion = champions.FirstOrDefault(c => c.Name.Equals(name));
                if (champion == null)
                {
                    // retournera un status code 404 (not found) si le champion na pas été trouvé
                    _logger.LogWarning($"the champion requested wasn't found, name: {name}");
                    return NotFound();
                }
                else
                {
                    return Ok(champion);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"error while trying to get champion with name: {name}");

                return BadRequest(ex.Message);
            }
        }

        // POST api/<Champion>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ChampionDto champion)
        {
            _logger.LogInformation("Request to post champion");

            try
            {
                // la méthode CreatedAction retourne => 201 Created: The server acknowledged the created resource. 
                return CreatedAtAction(nameof(GetByName), new { champion.Name },
                (await dataManager.ChampionsMgr.AddItem(ChampionMapper.ToModel(champion))).ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error when trying to post new champion");
                return BadRequest(ex.Message);
            }
            
        }

        // PUT api/<Champion>/Akali
        [HttpPut("{name}")]
        public async Task<IActionResult> Put(string name, [FromBody] ChampionDto newChampion) 
        {
            _logger.LogInformation($"Request to put champion where name is: {name}");
            try
            {   
                var champion = await dataManager.ChampionsMgr.GetItemsByName(name, 0, await dataManager.ChampionsMgr.GetNbItems());
                if(champion.First() == null)
                {
                    _logger.LogWarning($"the champion requested wasn't found, name: {name}");
                    return NotFound();
                }

                Champion? modified = await dataManager.ChampionsMgr.UpdateItem(champion.First(), newChampion.ToModel());
                return Ok(modified?.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error when trying to put champion");
                return BadRequest(ex.Message);  
            }
            
        }

        // DELETE api/<Champion>/Akali
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name) 
        {
            _logger.LogInformation($"Request to delete champion where name is: {name}");

            try
            {
                var champion = await dataManager.ChampionsMgr.GetItemsByName(name, 0, await dataManager.ChampionsMgr.GetNbItems());

                if (champion == null)
                {
                    _logger.LogWarning($"the champion requested wasn't found, name: {name}");
                    return NotFound();
                }
                else
                {
                    return Ok(await dataManager.ChampionsMgr.DeleteItem(champion.First()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error when trying to delete champion");
                return BadRequest(ex.Message);
            }
        }

        /*
 * This route allows to get the skins of a champion
 */
        // GET api/<Champion>/Akali/skin
        [HttpGet("{name}/skins")]
        public async Task<IActionResult> GetChampionSkins(string name)
        {
            _logger.LogInformation($"Request to get champion skins where name is: {name}");

            try
            {
                var champions = await dataManager.ChampionsMgr.GetItemsByName(name, 0, await dataManager.ChampionsMgr.GetNbItems());
                if (!champions.Any())
                {
                    _logger.LogWarning($"The champion requested wasn't found, name: {name}");
                    return NotFound();
                }

                var skins = await dataManager.SkinsMgr.GetItemsByChampion(champions.First(), 0, await dataManager.ChampionsMgr.GetNbItems());
                var skinDtos = skins.Select(skin => skin?.ToDto());
                if (!skinDtos.Any())
                {
                    _logger.LogWarning($"The champion has no skins, name: {name}");
                    return Ok(new List<SkinDto>());
                }

                return Ok(skinDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when trying to get champion's skins");
                return BadRequest(ex.Message);
            }
        }
    }
}
