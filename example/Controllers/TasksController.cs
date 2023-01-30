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
            try
            {
                var tasks = await dbContext.Tasks.Where(list => list.State != "inactive").ToListAsync();


                if (tasks.Count != 0 && tasks != null)
                {
                    return Ok(tasks);
                }
                return BadRequest(new { code = 404, message = "No hay elementos para mostrar" });

            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay elementos para mostrar: {e.Message}" });

            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTask([FromRoute] Guid id)
        {
            try
            {
                var tasks = await dbContext.Tasks.Where(list => list.State != "inactive" && list.Id == id).ToListAsync();

                if (tasks.Count != 0 && tasks != null)
                {
                    return Ok(tasks);
                }
                return BadRequest(new { code = 404, message = "No hay un elemento con este id" });

            }
            catch (Exception e)
            {
                return BadRequest(new { code = 404, message = $"No hay un elemento con este id: {e.Message}" });

            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(AddTaskRequest addTaskRequest)
        {
            try
            {
                var Task = new Tasks()
                {
                    Id = Guid.NewGuid(),
                    TaskName = addTaskRequest.TaskName,
                    TaskDescription = addTaskRequest.TaskDescription,
                    CreatedBy = addTaskRequest.CreatedBy,
                    Priority = addTaskRequest.Priority,
                    State = addTaskRequest.State
                };

                await dbContext.Tasks.AddAsync(Task);
                await dbContext.SaveChangesAsync();

                return Ok(Task);
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo añadir el elemento: {e.Message}" });
            }

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, UpdateTaskRequest updateTaskRequest)
        {
            try
            {
                var task = await dbContext.Tasks.FindAsync(id);

                if (task != null)
                {
                    task.TaskName = updateTaskRequest.TaskName;
                    task.TaskDescription = updateTaskRequest.TaskDescription;
                    task.CreatedBy = updateTaskRequest.CreatedBy;
                    task.Priority = updateTaskRequest.Priority;
                    task.State = updateTaskRequest.State;

                    await dbContext.SaveChangesAsync();
                    return Ok(task);
                }
                return BadRequest(new { code = 404, message = "No hay un elemento con este id" });
            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo modificar el elemento: {e.Message}" });
            }


        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            try
            {
                var tasks = await dbContext.Tasks.Where(list => list.State != "inactive" && list.Id == id).ToListAsync();

                if (tasks.Count != 0 && tasks != null)
                {
                    foreach (var item in tasks)
                    {
                        item.State = "inactive";
                    }
                    await dbContext.SaveChangesAsync();
                    return Ok(tasks);
                }
                return BadRequest(new { code = 404, message = "No hay un elemento con este id" });

            }
            catch (Exception e)
            {
                return BadRequest(new { code = 400, message = $"No se pudo eliminar el elemento: {e.Message}" });
            }
        }
    }
}
