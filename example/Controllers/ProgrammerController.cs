using example.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammerController : ControllerBase
    {

        private readonly ProgrammerContext _dbContext;


        public ProgrammerController(ProgrammerContext dbContext)
        {


            _dbContext = dbContext;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Programmer>>> GetProgrammer() {

            if (_dbContext == null) {

                return NotFound();
            }


            return await _dbContext.Programmers.ToListAsync();

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Programmer>> GetProgrammer(int id)
        {

            if (_dbContext == null)
            {

                return NotFound();
            }

            var programmer = await _dbContext.Programmers.FindAsync(id);
            if (programmer==null)
            {
                return NotFound();

            }
            
            
            return programmer;

        }


        [HttpPost]
        public async Task<ActionResult<Programmer>> PostProgrammer(Programmer programmer) {

            _dbContext.Programmers.Add(programmer);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProgrammer), new { id = programmer.Id }, programmer);
        
        
        
        }


        [HttpPut]
        public async Task<ActionResult<Programmer>> PutProgrammer(int id , Programmer programmer) {

            if (id != programmer.Id)
            {

                return BadRequest();

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

            var programmer = await _dbContext.Programmers.FindAsync(id);
           
            if (programmer == null)
            {

                return NotFound();

            }
            _dbContext.Programmers.Remove(programmer);

            await _dbContext.SaveChangesAsync();


            return Ok();        
        }









        private bool ProgrammerAvailable(int id)
        {

            return (_dbContext.Programmers?.Any(x => x.Id == id)).GetValueOrDefault();
        
        }




    }
}
