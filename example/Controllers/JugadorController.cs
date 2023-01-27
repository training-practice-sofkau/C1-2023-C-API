using example.Data;
using example.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using example.ReponseDto;
using System.Net;

namespace example.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class JugadorController : Controller
    {
        public readonly JugadorApiContext jugadorApiContext;
        public JugadorController(JugadorApiContext jugadorApiContext)

        {
            this.jugadorApiContext = jugadorApiContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetJugadores()
        {
            EstadoRespuesta respuesta = new EstadoRespuesta();
            try
            {
                respuesta.CodigoEstado = (int)HttpStatusCode.OK;
                respuesta.mensaje = "consulta exitosa";
                respuesta.Datos = jugadorApiContext.jugadors.ToListAsync();
                return Ok(respuesta);
            } 
            catch (Exception e)
            {
                respuesta.CodigoEstado = (int)HttpStatusCode.BadRequest;
                respuesta.mensaje = e.Message;
                return BadRequest(respuesta);
            }
            
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetJugador([FromRoute] Guid id)
        {
            
            var jugador = await jugadorApiContext.jugadors.FindAsync(id);

            if (jugador == null)
            {
                return NotFound();
            }
            return Ok(jugador);
        }

        [HttpPost]
        [Route("PostJugador")]
        public async Task<IActionResult> AddJugador(AddJugadores AddJugadores)
        {

            var jugador = new Jugador()
            {
                Id = Guid.NewGuid(),
                name = AddJugadores.name,
                pais = AddJugadores.pais,
                equipo = AddJugadores.equipo,
                posicion = AddJugadores.posicion,

            };

            await jugadorApiContext.jugadors.AddAsync(jugador);
            await jugadorApiContext.SaveChangesAsync();

            return Ok(jugador);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> ActualizarJugador([FromRoute] Guid id, UpdateJugadores updateJugadores)

         {
            var jugador = await jugadorApiContext.jugadors.FindAsync(id);

            if (jugador != null)
            {
                jugador.name = updateJugadores.name;
                jugador.pais = updateJugadores.pais;
                jugador.equipo = updateJugadores.equipo;
                jugador.posicion = updateJugadores.posicion;
            }

            await jugadorApiContext.SaveChangesAsync();
            return Ok();

        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteJugador([FromRoute] Guid id)
        {
            var jugador = await jugadorApiContext.jugadors.FindAsync(id);

            if (jugador != null)
            {
                jugadorApiContext.Remove(jugador);
                await jugadorApiContext.SaveChangesAsync();
                return Ok(jugador);
            }
            return NotFound();
        }
    }

 }
