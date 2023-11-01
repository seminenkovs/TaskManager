namespace TaskManagerApi.Models
{
    public class Task : CommonObjects
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte[] File { get; set; }
        public string Column { get; set; }
        public int? CreatorId { get; set; }
        public User Creator { get; set; }
        public int? ExecutorId { get; set; }
        public int DeskId { get; set; }
        public Desk Desk { get; set; }
    }
}
