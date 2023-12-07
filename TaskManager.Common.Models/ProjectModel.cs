namespace TaskManager.Common.Models
{
    public class ProjectModel : CommonModel
    {
        public ProjectStatus Status { get; set; }
        public int? AdminId { get; set; }
        public List<int> AllUsersIds { get; set; }
        public List<int> AllDesksIds { get; set; } 
    }
}