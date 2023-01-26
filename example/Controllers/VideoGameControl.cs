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

        //Consigo los datos
        [HttpGet]
        public async Task<IActionResult>GetVideoJuego()
        {
            return Ok(await dbContext.VideoJuegos.ToListAsync());
        }

        //Metodo para Añadir un juego
        [HttpPost]
        public async Task<IActionResult>AddVideoGame(AddVideoJuego addVideoJuego) {
            var videoJuego = new VideoJuego()
            {
                Id = Guid.NewGuid(),
                Title = addVideoJuego.Title,
                Descripcion = addVideoJuego.Descripcion,
                Productor = addVideoJuego.Productor

            };
            await dbContext.VideoJuegos.AddAsync(videoJuego);
            await dbContext.SaveChangesAsync();
            return Ok(videoJuego);
        }

        //Metodo para actualizar asdasda
        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateVideoGame([FromRoute] Guid id, UpdateVideoJuego updateVideoJuego)
        {
            var videoJuego = dbContext.VideoJuegos.Find(id);
            if (videoJuego != null)
            {
                videoJuego.Title = updateVideoJuego.Title;
                videoJuego.Descripcion = updateVideoJuego.Descripcion;
                videoJuego.Productor = updateVideoJuego.Productor;

                await dbContext.SaveChangesAsync();

                return Ok(videoJuego);
            }
            return NotFound();
        }
    }
}
