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

        [HttpGet]
        [Route("allusertrips/{id}")]
        public IEnumerable<int> GetUser(int id)
        {
            return mgr.HentAlleUserTrips(id);
        }

        // GET api/<TripController>/5
        [HttpGet("{id}")]
        public Trip Get(int id)
        {
            return mgr.HentEn(id);
        }
        [HttpGet]
        [Route("getwithuser/{userid}/{cycleid}")]
        public int Get(int userid, int cycleid)
        {
            return mgr.HentEnMedBruger(userid, cycleid);
        }

        // POST api/<TripController>
        [HttpPost]
        public void Post([FromBody] Trip value)
        {
            mgr.OpretTrip(value);
        }

        [HttpPut]
        [Route("slutTrip/{id}")]
        public bool AfslutTrip(int id, string time)
        {
            return mgr.AfslutTrip(id, time);
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
