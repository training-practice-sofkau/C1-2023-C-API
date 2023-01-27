using example.Data;
using example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
	}
}
