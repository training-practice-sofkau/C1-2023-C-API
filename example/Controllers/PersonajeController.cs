using example.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace example.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PersonajeController : ControllerBase
	{

		private static readonly string[] Habilidades = new[]
		{
			"Fuerza", "Psiquico", "Velocidad", "Volar", "Invisibilidad", "Cyborg"
		};

		private readonly ILogger<PersonajeController> _logger;

		public PersonajeController(ILogger<PersonajeController> logger)
		{
			_logger = logger;
		}

		[HttpGet(Name = "GetPersonaje")]
		public IEnumerable<Personaje> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new Personaje
			{
				ID = Random.Shared.Next(0, 100),
				Name = "Personaje "+index,
				Ability = Habilidades[Random.Shared.Next(Habilidades.Length)],
				Power = Random.Shared.Next(0, 1000),
				Health = Random.Shared.Next(0, 1000),
			})
			.ToArray();
		}

	}
}
