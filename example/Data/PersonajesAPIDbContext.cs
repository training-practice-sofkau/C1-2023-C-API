using example.Models;
using Microsoft.EntityFrameworkCore;

namespace example.Data
{
	public class PersonajesAPIDbContext : DbContext
	{
		public PersonajesAPIDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Personaje> Personajes { get; set; }
	}
}
