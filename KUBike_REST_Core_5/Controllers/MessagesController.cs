using KUBike_REST_Core_5.DBUTil;
using lib;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KUBike_REST_Core_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {

        private readonly ManageMessages mgr = new ManageMessages();

        // GET: api/<MessagesController>
        [HttpGet]
        public IList<Message> Get()
        {
            return mgr.HentAlle();
        }

        // GET: api/<MessagesController>
        [HttpGet]
        [Route("cykel/{id}")]
        public IList<Message> GetWithBike(int id)
        {
            return mgr.HentAlleMedCykel(id);
        }

        // GET api/<MessagesController>/5
        [HttpGet("henten/{id}")]
        public Message Get(int id)
        {
            return mgr.HentEn(id);
        }

        // POST api/<MessagesController>
        [HttpPost]
        public void Post([FromBody] Message value)
        {
            mgr.OpretMessage(value);
        }

        [HttpPut]
        [Route("statusone/{id}")]

        public bool one(int id)
        {
            return mgr.SetStatusOne(id);
        }

        [HttpPut]
        [Route("status2/{id}")]

        public bool two(int id)
        {
            return mgr.SetStatustwo(id);
        }

        [HttpPut]
        [Route("status3/{id}")]

        public bool three(int id)
        {
            return mgr.SetStatusthree(id);
        }

        /*
         // DELETE api/<MessagesController>/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         } */
    }
}
