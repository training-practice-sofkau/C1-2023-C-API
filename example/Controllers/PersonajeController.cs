using example.Data;
using example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace example.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PersonajeController : ControllerBase
	{
		private readonly PersonajesAPIDbContext dbContext;

		public PersonajeController(PersonajesAPIDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		
		[HttpGet]
		public async Task<List<Personaje>> GetPersonajes()
		{
			return await dbContext.Personajes.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<Object> Get(int id)
		{
			var personaje = await dbContext.Personajes.FirstOrDefaultAsync(m => m.ID == id);
			if (personaje == null)
				return NotFound();
			return Ok(personaje);

		}
	}
}
