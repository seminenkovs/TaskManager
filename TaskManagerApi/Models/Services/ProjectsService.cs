using TaskManager.Common.Models;
using TaskManagerApi.Models.Data;

namespace TaskManagerApi.Models.Services;

public class ProjectsService : ICommonService<ProjectModel>
{
    private readonly ApplicationContext _db;

    public ProjectsService(ApplicationContext db)
    {
        _db = db;
    }
    public bool Create(ProjectModel model)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public bool Update(int id, ProjectModel model)
    {
        throw new NotImplementedException();
    }
}