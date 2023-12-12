using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManager.Common.Models;
using TaskManagerApi.Models.Data;

namespace TaskManagerApi.Models.Services;

public class DesksService : AbstractionService, ICommonService<DeskModel>
{
    private readonly ApplicationContext _db;

    public DesksService(ApplicationContext db)
    {
        _db = db;
    }

    public bool Create(DeskModel model)
    {
        bool result = DoAction(delegate ()
        {
            Desk newDesk = new Desk(model);
            _db.Desks.Add(newDesk);
            _db.SaveChanges();
        });

        return result;
    }

    public bool Update(int id, DeskModel model)
    {
        bool result = DoAction(delegate ()
        {
            Desk desk = _db.Desks.FirstOrDefault(d => d.Id == id);
            desk.Name = model.Name;
            desk.Description = model.Description;
            desk.Photo = model.Photo;
            desk.AdminId = model.AdminId;
            desk.IsPrivate = model.IsPrivate;
            desk.ProjectId = model.ProjectId;
            desk.Columns = JsonConvert.SerializeObject(model.Columns);

            _db.Desks.Update(desk);
            _db.SaveChanges();
        });
        return result;
    }

    public bool Delete(int id)
    {
        bool result = DoAction(delegate ()
        {
            Desk newDesk = _db.Desks.FirstOrDefault(d => d.Id == id);
            _db.Desks.Remove(newDesk);
            _db.SaveChanges();
        });

        return result;
    }

    public DeskModel Get(int id)
    {
        Desk desk = _db.Desks.Include(d => d.Tasks)
            .FirstOrDefault(d => d.Id == id);
        var deskModel = desk?.ToDto();
        if (deskModel != null)
        {
            deskModel.TasksIds = desk.Tasks.Select(t => t.Id).ToList();
        }
        return deskModel;
    }

    public IQueryable<CommonModel> GetAll()
    {
        return _db.Desks.Select(d => d.ToDto() as CommonModel);
    }
}