using example.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace example.Data
{
    public class MascotasAPIDbContext : DbContext
    {
        public MascotasAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Mascotas> Mascotas { get; set; }

        
    }
}
