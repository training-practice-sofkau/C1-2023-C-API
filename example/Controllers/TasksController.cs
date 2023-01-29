using tasks.Models;
using tasks.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace tasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly TasksAPIDbContext dbContext;

        public TasksController(TasksAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await dbContext.Tasks.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTask([FromRoute] Guid id)
        {
            var task = await dbContext.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
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

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, UpdateTaskRequest updateTaskRequest)
        {
            var task = await dbContext.Tasks.FindAsync(id);

            if (task != null)
            {
                task.TaskName = updateTaskRequest.TaskName;
                task.TaskDescription = updateTaskRequest.TaskDescription;
                task.CreatedBy = updateTaskRequest.CreatedBy;
                task.Priority = updateTaskRequest.Priority;

                await dbContext.SaveChangesAsync();
                return Ok(task);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            var task = await dbContext.Tasks.FindAsync(id);

            if (task != null)
            {
                dbContext.Tasks.Remove(task);
                await dbContext.SaveChangesAsync();
                return Ok(task);
            }
            return NotFound();
        }
    }
}
