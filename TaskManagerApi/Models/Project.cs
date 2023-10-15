namespace TaskManagerApi.Models
{
    public class Project : CommonObjects
    {
        public int Id { get; set; }
        public List<User> AllUsers { get; set; } = new List<User>();
        public List<Desk> AllDesks { get; set; } = new List<Desk>();
    }
}
