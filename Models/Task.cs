using System;

namespace DapperSimpleTest.Models
{
    public class Task
    {
        public Guid Guid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime EndAt { get; set; }
        public User User { get; set; }
    }
}