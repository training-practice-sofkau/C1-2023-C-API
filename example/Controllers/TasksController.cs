using tasks.Models;
using tasks.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : Controller
    {
        private readonly TasksAPIDbContext dbContext;

        public TasksController(TasksAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet(Name = "GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await dbContext.Tasks.ToListAsync());
        }

        [HttpPost(Name = "AddTask")]
        public async Task<IActionResult> AddTask(AddTaskRequest addTaskRequest)
        {
            var Task = new Tasks()
            {
                Id = Guid.NewGuid(),
                TaskName = addTaskRequest.TaskName,
                TaskDescription = addTaskRequest.TaskDescription,
                CreatedBy = addTaskRequest.CreatedBy,
                Priority = addTaskRequest.Priority
            };

            await dbContext.Tasks.AddAsync(Task);
            await dbContext.SaveChangesAsync();

            return Ok(Task);
        }
    }
}
