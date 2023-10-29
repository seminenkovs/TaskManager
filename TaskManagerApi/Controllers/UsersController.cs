using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpPatch("update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                User userForUpdate = _db.Users.FirstOrDefault(u => u.Id == id);
                if (userForUpdate != null)
                {
                    userForUpdate.FirstName  = userModel.FirstName;
                    userForUpdate.LastName = userModel.LastName;
                    userForUpdate.Password = userModel.Password;
                    userForUpdate.Phone = userModel.Phone;
                    userForUpdate.Photo = userModel.Photo;
                    userForUpdate.Status = userModel.Status;
                    userForUpdate.Email = userModel.Email;

                    _db.Users.Update(userForUpdate);
                    _db.SaveChanges();
                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            User userForDelete = _db.Users.FirstOrDefault(u =>u.Id == id);
            if (userForDelete != null)
            {
                _db.Users.Remove(userForDelete);
                _db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return  await _db.Users.Select(u => u.ToDto()).ToListAsync();
        }
    }
}
