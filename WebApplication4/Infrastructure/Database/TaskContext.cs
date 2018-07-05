using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication4.Infrastructure.Database.Entity;

namespace WebApplication4.Infrastructure.Database
{
    public class TaskModelContext:DbContext
    {
        public DbSet<TaskModel> Tasks { get; set; }

    }
}