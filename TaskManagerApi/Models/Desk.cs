namespace TaskManagerApi.Models
{
    public class Desk
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[] Photo { get; set; }
        public bool IsPrivate { get; set; }
        public string Columns { get; set; }
        public User Admin { get; set; }
    }
}
