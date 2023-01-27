using example.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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





    }
}
