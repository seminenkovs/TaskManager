using TaskManager.Common.Models;

namespace TaskManagerApi.Models
{
    public class Project : CommonObjects
    {
        public int Id { get; set; }
        public ProjectStatus Status { get; set; }

        public int? AdminId { get; set; }
        public ProjectAdmin Admin { get; set; }
        public List<User> AllUsers { get; set; } = new List<User>();
        public List<Desk> AllDesks { get; set; } = new List<Desk>();
    }
}
