namespace TaskManagerApi.Models
{
    public class Desk : CommonObjects
    {
        public bool IsPrivate { get; set; }
        public string Columns { get; set; }
        public User Admin { get; set; }
    }
}
