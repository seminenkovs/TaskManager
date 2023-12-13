using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Common.Models;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Data;
using TaskManagerApi.Models.Services;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _usersService;

        public AccountController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
        }

        [Authorize]
        [HttpGet("info")]
        public IActionResult GetCurrentUserInfo()
        {
            string username = HttpContext.User.Identity.Name;
            var user = _db.Users.FirstOrDefault(u => u.Email == username);

            if (user != null)
            {
                return Ok(user.ToDto());
            }

            return NotFound();
        }

        [HttpPost("token")]
        public IActionResult GetToken()
        {
            var userData = _usersService.GetUserLoginPassFromBasicAuth(Request);
            var login = userData.Item1;
            var pass = userData.Item2;
            var identity = _usersService.GetIdentity(login,pass);
            var now =DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer:AuthOptions.ISSUER,
                audience:AuthOptions.AUDIENCE,
                notBefore: now,
                claims:identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPatch("update")]
        public IActionResult UpdateUser([FromBody] UserModel userModel)
        {
            if (userModel != null)
            {
                string username = HttpContext.User.Identity.Name;

                User userForUpdate = _db.Users.FirstOrDefault(u => u.Email == username);
                if (userForUpdate != null)
                {
                    userForUpdate.FirstName = userModel.FirstName;
                    userForUpdate.LastName = userModel.LastName;
                    userForUpdate.Password = userModel.Password;
                    userForUpdate.Phone = userModel.Phone;
                    userForUpdate.Photo = userModel.Photo;

                    _db.Users.Update(userForUpdate);
                    _db.SaveChanges();

                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }
    }
}
