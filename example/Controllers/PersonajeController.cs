using example.Data;
using Microsoft.AspNetCore.Mvc;

namespace example.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PersonajeController : Controller
	{
		private readonly PersonajesAPIDbContext dbContext;

		public PersonajeController(PersonajesAPIDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult GetPersonajes()
		{
			return Ok(dbContext.Personajes.ToList());
		}
	}
}
