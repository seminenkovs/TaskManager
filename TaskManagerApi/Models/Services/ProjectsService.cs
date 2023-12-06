﻿using Microsoft.EntityFrameworkCore;
using TaskManager.Common.Models;
using TaskManagerApi.Models.Data;

namespace TaskManagerApi.Models.Services;

public class ProjectsService : AbstractionService, ICommonService<ProjectModel>
{
    private readonly ApplicationContext _db;

    public ProjectsService(ApplicationContext db)
    {
        _db = db;
    }
    public bool Create(ProjectModel model)
    {
        bool result =  DoAction(delegate ()
        {
            Project newProject = new Project(model);
            _db.Projects.Add(newProject);
            _db.SaveChanges();
        });

        return result;
    }

    public bool Delete(int id)
    {
        bool result = DoAction(delegate()
        {
            Project newProject = _db.Projects.FirstOrDefault(p => p.Id == id);
            _db.Projects.Remove(newProject);
            _db.SaveChanges();
        });

        return result;
    }

    public ProjectModel Get(int id)
    {
        Project project = _db.Projects.FirstOrDefault(p => p.Id == id);
        return project?.ToDto();
    }

    public async Task<IEnumerable<ProjectModel>> GetByUserId(int userId)
    {
        List<ProjectModel> result = new List<ProjectModel>();
        var admin = _db.ProjectAdmins.FirstOrDefault(a => a.UserId == userId);
        if (admin != null)
        {
            var projectsForAdmin = await _db.Projects.Where(p => p.AdminId == admin.Id)
                .Select(p => p.ToDto()).ToListAsync();
            result.AddRange(projectsForAdmin);
        }

        var projectsForUsers = await _db.Projects.Include(p => p.AllUsers)
            .Where(p => p.AllUsers.Any(u => u.Id == userId))
            .Select(p => p.ToDto()).ToListAsync();
        result.AddRange(projectsForUsers);

        return result;
    }

    public IQueryable<ProjectModel> GetAll()
    {
        return _db.Projects.Select(p => p.ToDto());
    }

    public bool Update(int id, ProjectModel model)
    {
        bool result = DoAction(delegate ()
        {
            Project newProject = _db.Projects.FirstOrDefault(p => p.Id == id);
            newProject.Name = model.Name;
            newProject.Description = model.Description;
            newProject.Photo = model.Photo;
            newProject.Status = model.Status;
            newProject.AdminId = model.AdminId;
            _db.Projects.Update(newProject);
            _db.SaveChanges();
        });

        return result;
    }

    public void AddUsersToProject(int id, List<int> usersIds)
    {
        Project project = _db.Projects.FirstOrDefault(p => p.Id == id);
        foreach (var usersId in usersIds)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == usersId);
            project.AllUsers.Add(user);
        }
        _db.SaveChanges();
    }

    public void RemoveUsersFromProject(int id, List<int> usersIds)
    {
        Project project = _db.Projects.Include(p => p.AllUsers).FirstOrDefault(p => p.Id == id);
        foreach (var usersId in usersIds)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == usersId);
            if (project.AllUsers.Contains(user))
            {
                project.AllUsers.Remove(user);
            }
        }
        _db.SaveChanges();
    }
}