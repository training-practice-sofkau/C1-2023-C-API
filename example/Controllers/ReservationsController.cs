using example.Data;
using Microsoft.AspNetCore.Mvc;

namespace example.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly ReservationsAPIDbContext dbContext;

        public ReservationsController(ReservationsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetReservations()
        {
            return Ok(dbContext.Reservations.ToList());
        }
    }
}
