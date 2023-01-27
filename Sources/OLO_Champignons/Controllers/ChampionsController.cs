using Microsoft.AspNetCore.Mvc;
using Model;
using StubLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OLO_Champignons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionsController : ControllerBase
    {
        // GET: api/<Champion>
        [HttpGet]
        public IEnumerable<Champion> Get()
        {
            StubData stubData = new StubData();
            var champions = stubData.ChampionsMgr.GetItems(0,6);


            //return champions;
            return new List<Champion>();
        }

        // GET api/<Champion>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Champion>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
