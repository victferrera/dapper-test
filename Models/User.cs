using System;
using System.Collections.Generic;

namespace DapperSimpleTest.Models
{
    public class User
    {
        public Guid Guid { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<Task> Tasks { get; set; }
    }
}