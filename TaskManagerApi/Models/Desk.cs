﻿namespace TaskManagerApi.Models
{
    public class Desk : CommonObjects
    {
        public int Id { get; set; }
        public bool IsPrivate { get; set; }
        public string Columns { get; set; }

        public int AdminID { get; set; }
        public User Admin { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}