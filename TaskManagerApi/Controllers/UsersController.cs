using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Common.Models;
using TaskManagerApi.Models;
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

        [HttpGet("test")]
        public IActionResult TestApi()
        {
            return Ok("Hello World");
        }

        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                User newUser = new User(userModel.FirstName, userModel.LastName,
                    userModel.Email, userModel.Password, userModel.Status,
                    userModel.Phone, userModel.Photo);

                _db.Users.Add(newUser);
                _db.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }
    }
}
