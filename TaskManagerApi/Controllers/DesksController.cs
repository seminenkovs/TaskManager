using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models.Data;
using TaskManagerApi.Models.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DesksController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _usersService;

        public DesksController(ApplicationContext db, UsersService usersService)
        {
            _db = db;
            _usersService = usersService;
        }


        // GET: api/<DesksController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DesksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DesksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DesksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DesksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
