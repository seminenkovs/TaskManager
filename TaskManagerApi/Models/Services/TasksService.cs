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
        bool result = DoAction(delegate ()
        {
            Task newTask = new Task(model);
            _db.Tasks.Add(newTask);
            _db.SaveChanges();
        });

        return result;
    }

    public bool Update(int id, TaskModel model)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        bool result = DoAction(delegate ()
        {
            Task task = _db.Tasks.FirstOrDefault(t => t.Id == id);
            _db.Tasks.Remove(task);
            _db.SaveChanges();
        });

        return result;
    }

    public TaskModel Get(int id)
    {
        throw new NotImplementedException();
    }
}