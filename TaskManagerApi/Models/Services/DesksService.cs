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
        throw new NotImplementedException();
    }

    public bool Update(int id, DeskModel model)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DeskModel Get(int id)
    {
        Desk desk = _db.Desks.FirstOrDefault(d => d.Id == id);
        return desk?.ToDto();
    }
}