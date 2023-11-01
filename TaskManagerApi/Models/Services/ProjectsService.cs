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
            Project newProject = new Project();
        });
        return result;
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