namespace TaskManagerApi.Models
{
    public class Project : CommonObjects
    {
        public List<User> AllUsers { get; set; }
        public List<Desk> AllDesks { get; set; }
    }
}
