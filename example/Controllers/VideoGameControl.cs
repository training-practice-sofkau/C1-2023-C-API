using example.Data;
using example.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace example.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class VideoGameControl : Controller
    {
        private readonly VideoJuegoData dbContext;
        public VideoGameControl(VideoJuegoData dbContext) {
            this.dbContext = dbContext;
        }

        //Consigo los video juegos
        [HttpGet]
        public async Task<IActionResult> GetVideoGames()
        {
            return Ok(await dbContext.VideoJuegos.ToListAsync());
        }
        //Consigo un video juego
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetVideoGame([FromRoute] Guid id)
        {
            try
            {
                var videoJuego = await dbContext.VideoJuegos.FindAsync(id);
                if (videoJuego == null && videoJuego.Estado == "N")
                {
                    return NotFound();
                }
                return Ok(videoJuego);
            }
            catch (Exception)
            {
                return NotFound("Not fount id error 404");
            }
        }

        //Metodo para Añadir un juego
        [HttpPost]
        public async Task<IActionResult>AddVideoGame(AddVideoJuego addVideoJuego) {
            var videoJuego = new VideoJuego()
            {
                Id = Guid.NewGuid(),
                Title = addVideoJuego.Title,
                Descripcion = addVideoJuego.Descripcion,
                Productor = addVideoJuego.Productor,
                Estado = "S"

            };
            await dbContext.VideoJuegos.AddAsync(videoJuego);
            await dbContext.SaveChangesAsync();
            return Ok(videoJuego);
        }

        //Metodo para actualizar
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateVideoGame([FromRoute] Guid id, UpdateVideoJuego updateVideoJuego)
        {
            var videoJuego = await dbContext.VideoJuegos.FindAsync(id);
            try
            {
                if (videoJuego != null)
                {
                    videoJuego.Title = updateVideoJuego.Title;
                    videoJuego.Descripcion = updateVideoJuego.Descripcion;
                    videoJuego.Productor = updateVideoJuego.Productor;
                    videoJuego.Estado = "S";

                    await dbContext.SaveChangesAsync();

                    return Ok(videoJuego);
                }
                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
               return NotFound("Not fount id error 404");
            }
        }

        //Metodo Eliminar
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DaleteVideoGame([FromRoute] Guid id)
        {
            var videoJuego = await dbContext.VideoJuegos.FindAsync(id);
            videoJuego.Estado = "N";

            try
            {
                if (videoJuego != null)
                {
                    //dbContext.Remove(videoJuego);
                    await dbContext.SaveChangesAsync();
                    return Ok(videoJuego);
                }
                return NotFound();
            }
            catch (Exception){
                return NotFound("Not fount id error 404");
            }
        }

    }
}
