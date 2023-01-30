using Microsoft.EntityFrameworkCore;
using tasks.Models;

namespace tasks.Data
{
    public class TasksAPIDbContext : DbContext
    {
        public TasksAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Tasks> Tasks { get; set; }
    }
}
