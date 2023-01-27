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

		[HttpPost]
		public async Task<OkResult> Post(Personaje personaje)
		{
			dbContext.Add(personaje);
			await dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPut]
		public async Task<Object> Put(Personaje personajeData)
		{
			if (personajeData == null || personajeData.ID == 0)
				return BadRequest();

			var product = await dbContext.Personajes.FindAsync(personajeData.ID);
			if (product == null)
				return NotFound();
			product.Name = personajeData.Name;
			product.Ability = personajeData.Ability;
			product.Power = personajeData.Power;
			await dbContext.SaveChangesAsync();
			return Ok();
		}
	}
}
