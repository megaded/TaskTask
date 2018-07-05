using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication4.Infrastructure.Database;
using WebApplication4.Infrastructure.Database.Entity;

namespace WebApplication4.Infrastructure
{
    public class TaskModelRepository:IRepository<TaskModel> 
    {
        private TaskModelContext context;
        public TaskModelRepository()
        {
            context = new TaskModelContext();
        }
        public void Add(TaskModel entity)
        {
            context.Tasks.Add(entity);
            context.SaveChanges();
        }

        public TaskModel Get(Guid id)
        {
            return context.Tasks.FirstOrDefault(x => x.ID == id);
        }

        public void Update(TaskModel entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
        public TaskModel Create()
        {
           var newtask = new TaskModel()
            {
                ID = Guid.NewGuid(),
                Status = "created",
                TimeStamp = DateTime.Now
            };
            this.Add(newtask);
            return newtask;
        }
    }
}