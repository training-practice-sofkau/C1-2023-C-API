using example.Data;
using example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace example.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotasController : Controller
    {
        private readonly MascotasAPIDbContext dbContext;
        public MascotasController(MascotasAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetMascotas()
        {
            return Ok(await dbContext.Mascotas.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddMascotas(AgregarMascotaRequest agregarMascotaRequest) 
        {
            var mascotas = new Mascotas()
            {
                Id = Guid.NewGuid(),
                NombreDeLaMascota = agregarMascotaRequest.NombreDeLaMascota,
                TipoDeMascota = agregarMascotaRequest.TipoDeMascota,
                NombreDelTutor = agregarMascotaRequest.NombreDelTutor,
                CorreoDelTutor = agregarMascotaRequest.CorreoDelTutor,
                Celular = agregarMascotaRequest.Celular,
                Direccion = agregarMascotaRequest.Direccion

            };

            await dbContext.Mascotas.AddAsync(mascotas);
            await dbContext.SaveChangesAsync();

            return Ok(mascotas);
          
        }
    }
}
