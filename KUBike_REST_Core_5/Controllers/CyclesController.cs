using System.Collections.Generic;
using KUBike_REST_Core_5.DBUTil;
using lib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KUBike_REST_Core_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CyclesController : ControllerBase
    {
        private readonly ManageCycle mgr = new ManageCycle();

        // GET: api/<CyclesController>
        [HttpGet]
        public IEnumerable<Cycle> Get()
        {
            return mgr.HentAlle();
        }

        // GET api/<CyclesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            mgr.HentEn(id);
        }

        //// POST api/<CyclesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}
        
        //PUT api/<CyclesController>/5
        [HttpPut]
        [Route("start/{id}")]

        public bool Start(int id)
        {
            return mgr.StartRute(id);
        }

        [HttpPut]
        [Route("slut/{id}")]
        public bool Slut(int id)
        {
            return mgr.SlutRute(id);
        }
        //// DELETE api/<CyclesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}