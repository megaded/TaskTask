using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication4.Infrastructure;
using WebApplication4.Infrastructure.Database;
using WebApplication4.Infrastructure.Database.Entity;

namespace WebApplication4.Controllers
{
    public class TaskController : ApiController
    {
        private TaskModelRepository taskRepository;
        public TaskController()
        {
            taskRepository = new TaskModelRepository();
        }
        [HttpPost]
        [Route("task")]

        public HttpResponseMessage GetTask()
        {
            var createdTask = new Task<Tuple<HttpResponseMessage, TaskModel>>(() =>
            {
                var task = taskRepository.Create();
                return Tuple.Create(Request.CreateResponse(HttpStatusCode.Accepted, task.ID), task);
            });

            var runningTask = createdTask.ContinueWith<Guid>((Task<Tuple<HttpResponseMessage, TaskModel>> ttt) =>
            {
                var model = taskRepository.Get(ttt.Result.Item2.ID);
                model.Status = "running";
                taskRepository.Update(model);
                return model.ID;
            });
            var finishedTask = runningTask.ContinueWith((Task<Guid> t) =>
             {
                 var taskId = t.Result;
                 TimerCallback tm = new TimerCallback(FinishedTask);
                 Timer timer = new Timer(tm, taskId, 120000, 0);
             });
            createdTask.Start();
            return createdTask.Result.Item1;
        }
        [HttpGet]
        [Route("task/{taskId}")]

        public HttpResponseMessage GetTask(string taskId)
        {

            if (!Guid.TryParse(taskId, out Guid result))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            var taskModel = this.taskRepository.Get(result);
            return taskModel == null ? Request.CreateResponse(HttpStatusCode.NotFound) : Request.CreateResponse(HttpStatusCode.OK, new { status=taskModel.Status,timestamp=taskModel.TimeStamp});

        }

        private void FinishedTask(object taskId)
        {
            var taskModel = this.taskRepository.Get((Guid)taskId);
            taskModel.Status = "finished";
            this.taskRepository.Update(taskModel);
        }
    }
}
