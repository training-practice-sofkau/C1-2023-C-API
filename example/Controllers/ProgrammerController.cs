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
       

        public ProgrammerController(ProgrammerContext dbContext)
        {

            _dbContext = dbContext;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Programmer>>>GetProgrammer() {

            

            if (_dbContext == null)
            {

                return BadRequest( new { 
                
                        code = 404,
                        message = "Ocurrio algo inesperado"
                
                
                });

            }

            var activeRecords =  _dbContext.Programmers.Where(r => r.IsActive != 0).ToList();
            
            //var activeRecords = _dbContext.Programmers.Where(r => r.IsActive != 0).ToList();

            // return await _dbContext.Programmers.ToListAsync();

            return  activeRecords;

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Programmer>> GetProgrammer(int id){

            var programmer = await _dbContext.Programmers.FindAsync(id);
           // var activeRecords = _dbContext.Programmers.Where(r => r.IsActive != 0).ToList();
            
                if (_dbContext == null)
                {

                    return NotFound();

                }

                if (programmer == null || programmer.IsActive == 0  )
                {

                    return BadRequest(new
                    {

                        code = 400,
                        message = "No existe usuario con ese id por favor ingresar Id valido"

                    });

                }


            return programmer;
        }



        [HttpPost]
        public async Task<ActionResult<Programmer>> PostProgrammer(Programmer programmer) {

            int aux = 1;
            if (string.IsNullOrWhiteSpace(programmer.CompleteName)) {


                return BadRequest(new
                {
                    code = 400,
                    message = "El nombre es un dato requerido no dejar en blanco por favor"
                });


            }
          

            if (string.IsNullOrWhiteSpace(programmer.Email))
            {


                return BadRequest(new
                {
                    code = 400,
                    message = $"El correo electronico es un dato requerido no dejar en blanco por favor",
                  
                }) ;

            }

            /*
            if (programmer.PhoneNumber == int.Parse(aux.GetType())){



            }

            */


            _dbContext.Programmers.Add(programmer);
            await _dbContext.SaveChangesAsync();
           return CreatedAtAction(nameof(GetProgrammer), new { id = programmer.Id }, programmer);
        
        }


        [HttpPut]
        public async Task<ActionResult<Programmer>> PutProgrammer(int id , Programmer programmer) {

            string aux ="hi";

            _dbContext.Entry(programmer).State = EntityState.Modified;

            try
            {

                if (string.IsNullOrWhiteSpace(programmer.CompleteName))
                {


                    return BadRequest(new
                    {
                        code = 400,
                        message = "El nombre es un dato requerido no dejar en blanco por favor"
                    });


                }


                if (string.IsNullOrWhiteSpace(programmer.Email))
                {


                    return BadRequest(new
                    {
                        code = 400,
                        message = "El correo electronico es un dato requerido no dejar en blanco por favor"
                    });

                }





                if (programmer.IsActive == 0)
                {


                    return BadRequest(new
                    {


                        message = "Usuario no existe"


                    });

                }

                if ((programmer.PhoneNumber.GetType()) == aux.GetType())
                {

                    return BadRequest(new
                    {


                        message = "No puedes ingresar texto en campos numericos "
                    });



                }

                await _dbContext.SaveChangesAsync();

            }

            catch (DbUpdateConcurrencyException)
            {

                    if (!ProgrammerAvailable(id))
                    {
                        return NotFound("Not fount line 128");
                    }
                    else {

                        throw;
                    
                    }

            }

           

            
            return Ok(new { 
            
                message= "Usuario Actualizado efectivamente"
            
            });

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProgrammer(int id) {

            var programmer = await _dbContext.Programmers.FindAsync(id);
           
          
            if (_dbContext.Programmers == null){

                return NotFound();
            }

            if (programmer == null || programmer.IsActive == 0)
            {

                return BadRequest(new
                {

                    code = 400,
                    message = "No existe usuario con ese id por favor ingresar Id valido para poderlo eliminar"

                });

            }


            var recordToUpdate = _dbContext.Programmers.FirstOrDefault(r => r.Id == id);

           
            if (recordToUpdate != null)
            {

                recordToUpdate.IsActive = 0;
                _dbContext.SaveChanges();
            }
            else {


                return NotFound();
            }

            //
           // _dbContext.Programmers.Remove(program);

           // await _dbContext.SaveChangesAsync();

            return Ok(new{ 
            
                code = 200,
                message = $"El usuario con id {id} fue eliminado"

            
            });        
        }


        private bool ProgrammerAvailable(int id)
        {

            return (_dbContext.Programmers?.Any(x => x.Id == id)).GetValueOrDefault();
        
        }




    }

}

