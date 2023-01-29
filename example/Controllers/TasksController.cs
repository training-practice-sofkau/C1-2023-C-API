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

    }
}
