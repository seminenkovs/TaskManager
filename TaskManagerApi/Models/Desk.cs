using TaskManager.Common.Models;

namespace TaskManagerApi.Models
{
    public class Desk : CommonObjects
    {
        public int Id { get; set; }
        public bool IsPrivate { get; set; }
        public string Columns { get; set; }

        public int AdminId { get; set; }
        public User Admin { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public List<Task> Tasks { get; set; } = new List<Task>();

        public Desk() { }

        public Desk(DeskModel deskModel) : base(deskModel)
        {
            Id = deskModel.Id;
            AdminId = deskModel.AdminId;
            IsPrivate = deskModel.IsPrivate;
            ProjectId = deskModel.ProjectId;

            if (deskModel.Columns.Any())
            {
                Columns = "[" + string.Join(",", deskModel.Columns) + "]";
            }
        }

        public DeskModel ToDto()
        {
            return new DeskModel()
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                CreationDate = this.CreationDate,
                Photo = this.Photo,
                AdminId = this.AdminId,
                IsPrivate = this.IsPrivate,
                Columns = this.Columns?.Replace("[", "")
                    .Replace("]", "")
                    .Split(","),
                ProjectId = this.ProjectId
            };
        }
    }
}
