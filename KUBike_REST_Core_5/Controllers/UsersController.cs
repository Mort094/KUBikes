using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KUBike_REST_Core_5.DBUTil;
using lib;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KUBike_REST_Core_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ManageUser mgr = new ManageUser();

        // GET: api/<CyclesController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return mgr.HentAlle();
        }

        // GET api/<CyclesController>/5
        [HttpGet("{email}")]
        public User Get(string email)
        {
            return mgr.HentEn(email);
        }

        // POST api/<CyclesController>
        [HttpPost]
        public void Post([FromBody] User value)
        {
            mgr.OpretUser(value);
        }

        [HttpGet("{email}/{password}")]
        public bool Login(string email, string password)
        {
            return mgr.Login(email, password);
        }
    }
}
