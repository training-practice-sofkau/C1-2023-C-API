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
        public async Task<ActionResult<IEnumerable<Programmer>>>GetProgrammer() {

            if (_dbContext == null) {

                return NotFound();            
            }
        
        
        return await _dbContext.Programmers.ToListAsync();
        
        }

        [HttpGet]
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




    }
}
