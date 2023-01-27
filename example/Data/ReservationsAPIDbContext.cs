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

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Reservation>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.IsDeleted = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsDeleted = true;
                        break;
                }
            }

            return base.SaveChanges();
        }

    }
}
