using example.Data;
using example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetMascota([FromRoute] Guid id) 
        {
            var mascota = await dbContext.Mascotas.FindAsync(id);

            if (mascota == null)
            {
                return NotFound();
            }
            return Ok(mascota);
           
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

            return Ok($"{mascotas.NombreDeLaMascota} Se ha registrado de forma correcta!");
          
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> ActualizarMascota([FromRoute] Guid id, ActualizarMascotaRequest actualizarMascotaRequest) 
        {
            var mascota = await dbContext.Mascotas.FindAsync(id);

            if (mascota !=null)
            {
                mascota.NombreDeLaMascota = actualizarMascotaRequest.NombreDeLaMascota;
                mascota.TipoDeMascota = actualizarMascotaRequest.TipoDeMascota;
                mascota.NombreDelTutor = actualizarMascotaRequest.NombreDelTutor;
                mascota.CorreoDelTutor = actualizarMascotaRequest.CorreoDelTutor;
                mascota.Celular = actualizarMascotaRequest.Celular;
                mascota.Direccion = actualizarMascotaRequest.Direccion;

                await dbContext.SaveChangesAsync();

                return Ok("El registro se ha actualizado de forma correcta!");
            }

            return NotFound();
        }



        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> EliminarMascota([FromRoute] Guid id)
        {
           var mascota = await dbContext.Mascotas.FindAsync(id);

            if (mascota != null)
            {
                dbContext.Update(mascota.Estado = 0);
                await dbContext.SaveChangesAsync();
                return Ok($"{mascota.NombreDeLaMascota} Se ha eliminado de forma correcta!");
            }

            return NotFound();
        }

       /* [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> EliminarMascota([FromRoute] Guid id)
        {
            var mascota = await dbContext.Mascotas.FindAsync(id);
            SqlCommand cmd = new SqlCommand();
            if (mascota != null)
            {
                cmd.CommandText = "ActualizarEstado";
                cmd.Parameters.Add("Id", System.Data.SqlDbType.UniqueIdentifier).Value = id;
                cmd.ExecuteNonQuery();
                return Ok($"{mascota.NombreDeLaMascota} Se ha eliminado de forma correcta!");
            }

            return NotFound();
        }*/
    }
}
