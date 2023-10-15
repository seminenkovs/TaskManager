namespace TaskManagerApi.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[] Photo { get; set; }
        public List<User> AllUsers { get; set; }
        //public List<Desk> AllDesks { get; set; }
    }
}
