using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;

namespace example.Model
{
    public class PlayerApiContext : DbContext
    {
        public PlayerApiContext(DbContextOptions options) : base(options)
        {

            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity < id
            modelBuilder.Entity<name>().ToTable("Pais");
            modelBuilder.Entity<seleccionClub>().ToTable("Demarcacion");
            modelBuilder.Entity<posicion>().ToTable("Pie");
            modelBuilder.Entity<pais>().ToTable("Nacionalidad"

        public DbSet<Player> players { get; set; }
    }
}
