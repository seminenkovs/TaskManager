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
    public class TasksController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _usersService;
        private readonly TasksService _tasksService;

        public TasksController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
            _tasksService = new TasksService(db);
        }

        [HttpGet]
        public async Task<IEnumerable<CommonModel>> GetTasksByDesk(int deskId)
        {
            return await _tasksService.GetAll(deskId).ToListAsync();
        }

        [HttpGet("user")]
        public async Task<IEnumerable<CommonModel>> GetTasksForCurrentUser()
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                return await _tasksService.GetTasksForUser(user.Id).ToListAsync();
            }

            return Array.Empty<CommonModel>();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           var task = _tasksService.Get(id);
           return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TaskModel taskModel)
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                if (taskModel != null)
                {
                    taskModel.CreatorId = user.Id;
                    bool result = _tasksService.Create(taskModel);
                    return result ? Ok() : NotFound();
                }
                return BadRequest();
            }

            return Unauthorized();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] TaskModel taskModel)
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user != null)
            {
                if (taskModel != null)
                {
                    bool result = _tasksService.Update(id, taskModel);
                    return result ? Ok() : NotFound();
                }
                return BadRequest();
            }

            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _tasksService.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}
