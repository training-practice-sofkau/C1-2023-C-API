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
			//Busca los personajes que no hayan sido eliminados y los retorna
			var personajeActivo = dbContext.Personajes.Where(r => r.BanActivo != false).ToList();
			return personajeActivo;

			//Muestra todos los personajes 
			//return await dbContext.Personajes.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<Object> Get(int id)
		{
			var personaje = await dbContext.Personajes.FirstOrDefaultAsync(m => m.ID == id);
			if (personaje == null)
				return NotFound("El personaje no existe");
			return Ok(personaje);
		}

		[HttpPost]
		public async Task<object> Post(Personaje personajeData)
		{
			dbContext.Add(personajeData);
			await dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpPut]
		public async Task<Object> Put(Personaje personajeData)
		{
			if (personajeData == null || personajeData.ID == 0)
				return BadRequest("El ID no es correcto. ");

			var personaje = await dbContext.Personajes.FindAsync(personajeData.ID);
			if (personaje == null)
				return NotFound("El personaje no existe. ");
			if (personaje.BanActivo == false)
				return NotFound("El ha sido eliminado. ");
			personaje.Name = personajeData.Name;
			personaje.Ability = personajeData.Ability;
			personaje.Power = personajeData.Power;
			await dbContext.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<Object> Delete(int id)
		{
			var personaje = await dbContext.Personajes.FindAsync(id);
			if (personaje.BanActivo == false) return NotFound("El personaje ya ha sido eliminado. ");
			if (personaje == null) return NotFound("ID incorrecto");
			dbContext.Personajes.Remove(personaje);
				dbContext.SaveChanges();
			return Ok();
		}
	}
}
