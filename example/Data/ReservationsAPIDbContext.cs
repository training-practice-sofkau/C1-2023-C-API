using example.Models;
using Microsoft.EntityFrameworkCore;

namespace example.Data
{
    public class ReservationsAPIDbContext : DbContext
    {
        public ReservationsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Reservation> Reservations { get; set; }
    }
}
