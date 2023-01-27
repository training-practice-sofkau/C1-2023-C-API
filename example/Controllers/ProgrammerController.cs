using example.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace example.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    public class ProgrammerController : ControllerBase
    {

        private readonly ProgrammerContext _dbContext;
        private Programmer pro = new Programmer();



        public ProgrammerController(ProgrammerContext dbContext)
        {

            _dbContext = dbContext;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Programmer>>>GetProgrammer() {

            if (_dbContext == null)
            {

                return BadRequest();

            }


            var activeRecords = _dbContext.Programmers.Where(r => r.IsActive != 0).ToList();

            // return await _dbContext.Programmers.ToListAsync();

            return  activeRecords;

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Programmer>> GetProgrammer(int id)
        {



            if (_dbContext == null)
            {

                return NotFound();
            }

            var programmer = await _dbContext.Programmers.FindAsync(id);
            if (programmer == null)
            {
                return NotFound();

            }


            return programmer;

        }


        [HttpPost]
        public async Task<ActionResult<Programmer>> PostProgrammer(Programmer programmer) {

            if (string.IsNullOrWhiteSpace(programmer.CompleteName))
            {


                return BadRequest(new
                {
                    code = 400,
                    message = "El nombre es un dato requerido no dejar en blanco por favor"
                }) ;
                
               
            }

            if (string.IsNullOrWhiteSpace(programmer.Email))
            {


                return BadRequest(new
                {
                    code = 400,
                    message = "El correo electronico es un dato requerido no dejar en blanco por favor"
                });

            }

            _dbContext.Programmers.Add(programmer);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProgrammer), new { id = programmer.Id }, programmer);
        
        }


        [HttpPut]
        public async Task<ActionResult<Programmer>> PutProgrammer(int id , Programmer programmer) {

            if (id != programmer.Id)
            {

               // return HttpStatusCodeResult( "");

            }

            _dbContext.Entry(programmer).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {

                    if (!ProgrammerAvailable(id))
                    {
                        return NotFound();
                    }
                    else {

                        throw;
                    
                    }

            }

            return Ok();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProgrammer(int id) {

            if (_dbContext.Programmers == null){


                return NotFound();


            }

          //  var program = await _dbContext.Programmers.FindAsync(id);
           
           /* if (program == null)
            {

                return NotFound();

            }
           */

            var recordToUpdate = _dbContext.Programmers.FirstOrDefault(r => r.Id == id);

            if (recordToUpdate != null )
            {

                recordToUpdate.IsActive = 0;
                _dbContext.SaveChanges();


            }

            //
           // _dbContext.Programmers.Remove(program);

           // await _dbContext.SaveChangesAsync();

            return Ok();        
        }


        private bool ProgrammerAvailable(int id)
        {

            return (_dbContext.Programmers?.Any(x => x.Id == id)).GetValueOrDefault();
        
        }




    }
}
