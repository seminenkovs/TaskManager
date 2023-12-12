using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Models;
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
        private readonly DesksService _desksService;


        public DesksController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
            _desksService = new DesksService(db);
        }

        [HttpGet]
        public async Task<IEnumerable<CommonModel>> GetDesksForCurrentUser()
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                return await _desksService.GetAll(user.Id).ToListAsync();
            }

            return Array.Empty<CommonModel>();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            
        }

        [HttpGet("project/{projectId}")]
        public IActionResult GetProjectDesks(int projectId)
        {
            
        }

        [HttpPost]
        public IActionResult Create([FromBody] DeskModel deskModel)
        {
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] DeskModel deskModel)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
        }
    }
}
