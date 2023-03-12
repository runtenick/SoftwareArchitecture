using Microsoft.AspNetCore.Mvc;
using Model;
using DTO;
using Api.Mapper;
using Api.Pagination;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OLO_Champignons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionsController : ControllerBase
    {
        // public or private ? 
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
            var champions = (await dataManager.ChampionsMgr.GetItems(pageRequest.Index,
               pageRequest.Count)).Select(champion => champion?.ToDto());

            if(pageRequest.Count > 10)
            {
                _logger.LogWarning("too many champions requested");
                // return erreur ?
            }
            return Ok(champions);
        }

        
        // GET api/<Champion>/Akali
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var champions = (await dataManager.ChampionsMgr.GetItems(0,
               (await dataManager.ChampionsMgr.GetNbItems()))).Select(champion => champion?.ToDto());

            return Ok(champions.Where(c => c.Name.Equals(name)));
        }

        // POST api/<Champion>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ChampionDto champion)
        {
            try
            {
                return CreatedAtAction(nameof(GetByName), new { champion.Name },
                (await dataManager.ChampionsMgr.AddItem(ChampionMapper.ToModel(champion))).ToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // PUT api/<Champion>/5
        [HttpPut("{name}")]
        public async Task<IActionResult> Put(string name, [FromBody] ChampionDto newChampion) 
        {
            try
            {   
                var champion = await dataManager.ChampionsMgr.GetItemsByName(name, 0, await dataManager.ChampionsMgr.GetNbItems());
                Champion modified = await dataManager.ChampionsMgr.UpdateItem(champion.First(), newChampion.ToModel());
                return Ok(modified.ToDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // DELETE api/<Champion>/5
        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(String name) // faire plutot avec nom
        {
            try
            {
                var champion = await dataManager.ChampionsMgr.GetItemsByName(name, 0, await dataManager.ChampionsMgr.GetNbItems());

                if (champion != null)
                {
                   return Ok(await dataManager.ChampionsMgr.DeleteItem(champion.First()));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
