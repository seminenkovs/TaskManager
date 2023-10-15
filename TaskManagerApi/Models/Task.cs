namespace TaskManagerApi.Models
{
    public class Task : CommonObjects
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte[] File { get; set; }
    }
}
