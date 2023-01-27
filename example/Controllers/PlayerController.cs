using example.Data;
using Microsoft.AspNetCore.Mvc;

namespace example.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : Controller
    {
        public readonly PlayerApi playerApi;
        public PlayerController(PlayerApi playerApi)
        
        {
            this.playerApi = playerApi;
        }


        [HttpGet]
        public IActionResult GetPlayers()
        {

            return Ok(playerApi.players.ToList());
            
        }

        [HttpPost]
        public IActionResult AddPlayer()
    }
}
