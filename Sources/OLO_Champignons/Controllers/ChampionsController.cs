using Microsoft.AspNetCore.Mvc;
using Model;
using StubLib;
using DTO;
using Api.Mapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OLO_Champignons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionsController : ControllerBase
    {
        private IDataManager dataManager;

        public ChampionsController(IDataManager d)
        {
            dataManager = d;
        }

        // GET: api/<Champion>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var champions = (await dataManager.ChampionsMgr.GetItems(0, 
               (await dataManager.ChampionsMgr.GetNbItems()))).Select(champion => champion?.ToDto());
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Champion>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
