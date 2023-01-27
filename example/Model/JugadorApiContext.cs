using Microsoft.EntityFrameworkCore;


namespace example.Model
{
    public class JugadorApiContext : DbContext
    {
        public JugadorApiContext(DbContextOptions options) : base(options);

        public DbSet<Jugador> jugadors { get; set; }
    }
}
