namespace TaskManagerApi.Models
{
    public class CommonObjects
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[] Photo { get; set; }

        public CommonObjects()
        {
            CreationDate = DateTime.Now;
        }
    }
}