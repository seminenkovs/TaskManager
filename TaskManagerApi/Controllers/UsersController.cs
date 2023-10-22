using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models.Data;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _db;

        public UsersController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] object userModel)
        {
            if (userModel != null)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
