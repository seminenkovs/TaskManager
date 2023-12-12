using TaskManager.Common.Models;

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

        public CommonObjects(CommonModel model)
        {
            Name = model.Name;
            Description = model.Description;
            CreationDate = DateTime.Now;
            Photo = model.Photo;
        }
    }
}