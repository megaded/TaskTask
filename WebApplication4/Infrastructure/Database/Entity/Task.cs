using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Infrastructure.Database.Entity
{
    public class TaskModel
    {
        public Guid ID { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}