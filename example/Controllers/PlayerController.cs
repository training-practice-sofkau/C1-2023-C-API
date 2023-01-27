using example.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace example.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        public readonly PlayerApiContext playerApi;
        public PlayerController(PlayerApiContext playerApi)       

        {
            playerApi = playerApi;
        }


        [HttpGet]
        [Route("GetJugadores")]
        public async Task<ActionResult<IEnumerable<Player>>> GetJugadores()
        {
            return await playerApi.players.ToListAsync();
        }

        [HttpGet]
        [Route("GetJugador/{id}")]
        public async Task<ActionResult<Player>> GetJugador(int id)
        {
            var player = await playerApi.players.FirstOrDefaultAsync(m => m.id == id);
            if (player == null)
                return NotFound();
            return Ok(player);
        }

        [HttpPost]
        [Route("PostJugador")]
        public async Task<ActionResult<Player>> PostJugador(Player player)
        {
          
                playerApi.players.Add(player);
                await playerApi.SaveChangesAsync();
                return Ok();          
        }
    }
}
