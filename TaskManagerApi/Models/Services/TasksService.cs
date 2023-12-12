using TaskManager.Common.Models;
using TaskManagerApi.Models.Data;

namespace TaskManagerApi.Models.Services;

public class TasksService : AbstractionService, ICommonService<TaskModel>
{
    private readonly ApplicationContext _db;

    public TasksService(ApplicationContext db)
    {
        _db = db;
    }

    public bool Create(TaskModel model)
    {
        throw new NotImplementedException();
    }

    public bool Update(int id, TaskModel model)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public TaskModel Get(int id)
    {
        throw new NotImplementedException();
    }
}