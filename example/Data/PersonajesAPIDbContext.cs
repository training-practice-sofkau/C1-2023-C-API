using example.Models;
using Microsoft.EntityFrameworkCore;

namespace example.Data
{
	public class PersonajesAPIDbContext : DbContext
	{
		public DbSet<Personaje> Personajes { get; set; }
		public PersonajesAPIDbContext(DbContextOptions options) : base(options)
		{
		}
		public override int SaveChanges()
		{
			foreach (var item in ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Deleted &&
				e.Metadata.GetProperties().Any(x => x.Name == "BanActivo")))
			{
				item.State= EntityState.Unchanged;
				item.CurrentValues["BanActivo"] = false;
			}

			return base.SaveChanges();
		}
	}
}
