using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication4.Infrastructure.Database
{
    public class TaskInitializer : DropCreateDatabaseAlways<TaskModelContext>
    {
        protected override void Seed(TaskModelContext context)
        {
        }
    }
}