using example.Model;
using Microsoft.EntityFrameworkCore;


namespace example.Data
{
    public class JugadorApiContext : DbContext
    {

        public JugadorApiContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<Jugador> jugadors { get; set; }
    }
}
