using example.Model;
using Microsoft.EntityFrameworkCore;
namespace example.Data
{
    public class VideoJuegoData : DbContext
    { 
        public VideoJuegoData(DbContextOptions options) : base(options) { }
        public DbSet<VideoJuego> VideoJuegos { get; set;}
    }
}
