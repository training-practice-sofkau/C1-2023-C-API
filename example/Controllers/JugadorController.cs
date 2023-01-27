using example.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace example.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class JugadorController : Controller
    {
        public readonly JugadorApiContext jugadorApiContext;
        public JugadorController(JugadorApiContext jugadorApiContext)

        {
            jugadorApiContext = jugadorApiContext;
        }


        [HttpGet]
        [Route("GetJugadores")]
        public async Task<ActionResult<IEnumerable<Jugador>>> GetJugadores()
        {
            return await jugadorApiContext.jugadors.ToListAsync();
        }

        [HttpGet]
        [Route("GetJugador/{id}")]
        public async Task<ActionResult<Jugador>> GetJugador(int id)
        {
            var jugador = await jugadorApiContext.jugadors.FirstOrDefaultAsync(m => m.id == id);
            if (jugador == null)
                return NotFound();
            return Ok(jugador);
        }

        [HttpPost]
        [Route("PostJugador")]
        public async Task<ActionResult<Jugador>> PostJugador(Jugador jugador)
        {

            jugadorApiContext.Add(jugador);
            await jugadorApiContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("PutJugador/{id}")]
        public async Task<IActionResult> PutJugador(int id, Jugador jugador)
        {
            if (id != jugador.id)
            {
                throw new ArgumentException("id del jugador no coinciden");
            }

            jugadorApiContext.Entry(jugador).State = EntityState.Modified;
            await jugadorApiContext.SaveChangesAsync();
            return Ok();

        }

    }
}
