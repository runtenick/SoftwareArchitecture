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
            return CreatedAtAction(nameof(GetByName), new { champion.Name },
                (await dataManager.ChampionsMgr.AddItem(ChampionMapper.ToModel(champion))).ToDto());
        }

        // PUT api/<Champion>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ChampionDto newChampion) // faire plutot avec nom que id 
        {
            var champions = (await dataManager.ChampionsMgr.GetItems(0,
               await dataManager.ChampionsMgr.GetNbItems())).Select(champion => champion?.ToDto());

            
            Champion modified = await dataManager.ChampionsMgr.UpdateItem(champions.First(champion => champion.Id.Equals(id)).ToModel(),newChampion.ToModel());
            return Ok(modified.ToDto());
        }

        // DELETE api/<Champion>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) // faire plutot avec nom
        {
            var champions = (await dataManager.ChampionsMgr.GetItems(0,
                await dataManager.ChampionsMgr.GetNbItems())).Select(champion => champion?.ToDto());

            bool deleted = await dataManager.ChampionsMgr.DeleteItem(champions.First(champion => champion.Id.Equals(id)).ToModel());
            return Ok(champions.First(champion => champion.Id.Equals(id)));
        }
    }
}
