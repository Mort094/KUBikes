using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lib;
using KUBike_REST_Core_5.DBUTil;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KUBike_REST_Core_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly ManageTrip mgr = new ManageTrip();
        // GET: api/<TripController>
        [HttpGet]
        public IEnumerable<Trip> Get()
        {
            return mgr.HentAlle();
        }

        // GET api/<TripController>/5
        [HttpGet("{id}")]
        public Trip Get(int id)
        {
            return mgr.HentEn(id);
        }
        [HttpGet]
        [Route("getwithuser/{id}/")]
        public Trip Get(int id, int userid)
        {
            return mgr.HentEnMedBruger(id, userid);
        }

        // POST api/<TripController>
        [HttpPost]
        public void Post([FromBody] Trip value)
        {
            mgr.OpretTrip(value);
        }

        [HttpPut]
        [Route("slutTrip/{id}")]
        public bool AfslutTrip(int id)
        {
            return mgr.AfslutTrip(id);
        }

        /*
        // PUT api/<TripController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TripController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        } */
    }
}
