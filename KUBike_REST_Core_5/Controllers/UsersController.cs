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
        public int Get(string email)
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
        // api/users//updateUser/<id>
        [HttpPut]
        [Route("updateUser/{id}")]
        public bool Put(int id, [FromBody] User value)
        {
            return mgr.UpdateUser(id, value);
        }
        // GET: api/users/user/<id>
        [HttpGet]
        [Route("user/{id}")]
        public User Get(int id)
        {
            return mgr.HentEnMedId(id);
        }
        [HttpPut]
        [Route("deactivate/{id}")]
        public bool UserDeactive(int id)
        {
            return mgr.DeactivateUser(id);
        }
        //api/users/<id>
        [HttpDelete]
        [Route("delete/{id}")]
        public void Delete(int id)
        {
            mgr.DeleteUser(id);
        }
    }
}
