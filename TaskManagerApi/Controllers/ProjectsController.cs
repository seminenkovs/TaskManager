using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Models;
using TaskManagerApi.Models;
using TaskManagerApi.Models.Data;
using TaskManagerApi.Models.Services;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UsersService _usersService;
        private readonly ProjectsService _projectsService;

        public ProjectsController(ApplicationContext db)
        {
            _db = db;
            _usersService = new UsersService(db);
            _projectsService = new ProjectsService(db);
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectModel>> Get()
        {
            var user = _usersService.GetUser(HttpContext.User.Identity.Name);
            if (user.Status == UserStatus.Admin)
            {
                return await _projectsService.GetAll().ToListAsync();
            }
            else
            {
                return await _projectsService.GetByUserId(user.Id);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var project = _projectsService.Get(id);
            return project == null ? NotFound() : Ok(project);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProjectModel projectModel)
        {
            if (projectModel != null)
            {
                var user = _usersService.GetUser(HttpContext.User.Identity.Name);
                if (user != null)
                {
                    if (user.Status == UserStatus.Admin || user.Status == UserStatus.Editor)
                    {
                        var admin = _db.ProjectAdmins.FirstOrDefault(a => a.UserId == user.Id);
                        if (admin == null)
                        {
                            admin = new ProjectAdmin(user);
                            _db.ProjectAdmins.Add(admin);
                        }
                        projectModel.AdminId = admin.Id;

                        bool result = _projectsService.Create(projectModel);
                        return result ? Ok() : NotFound();
                    }
                }
                return Unauthorized();
            }
            return BadRequest();
        }

        [HttpPatch]
        public IActionResult Update(int id, [FromBody] ProjectModel projectModel)
        {
            if (projectModel != null)
            {
                var user = _usersService.GetUser(HttpContext.User.Identity.Name);
                if (user != null)
                {
                    if (user.Status == UserStatus.Admin || user.Status == UserStatus.Editor)
                    {
                        bool result = _projectsService.Update(id, projectModel);
                        return result ? Ok() : NotFound();
                    }
                    return Unauthorized();
                }
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool result = _projectsService.Delete(id);

            return result ? Ok() : NotFound();
        }

        [HttpPatch("{id}/users")]
        public IActionResult AddUsersToProject(int id, [FromBody] List<int> usersIds)
        {
            if (usersIds != null)
            {
                var user = _usersService.GetUser(HttpContext.User.Identity.Name);
                if (user != null)
                {
                    if (user.Status == UserStatus.Admin || user.Status == UserStatus.Editor)
                    {
                        var project = _projectsService.Get(id);
                        var usersForAdd = _usersService.GetAllByIds(usersIds);
                        project.AllUsers.AddRange(usersForAdd);
                        _db.SaveChanges();
                    }
                    return Unauthorized();
                }
            }
            return BadRequest();
        }
    }
}
