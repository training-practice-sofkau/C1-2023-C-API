using Microsoft.EntityFrameworkCore;

namespace example.Models
{
    public class ProgrammerContext : DbContext
    {
        public ProgrammerContext(DbContextOptions<ProgrammerContext> options): base (options)
        {


        }

            
        public DbSet<Programmer> Programmers { get; set; }  
        
    
    }
}
