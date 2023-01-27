using example.Data;
using example.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetReservations()
        {
            return Ok(await dbContext.Reservations.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(AddReservationRequest AddReservationRequest)
        {
            var reservation = new Reservation
            {
                ReservationId = Guid.NewGuid(),
                ClientName = AddReservationRequest.ClientName,
                Location = AddReservationRequest.Location,
                Date = AddReservationRequest.Date
            };

            await dbContext.Reservations.AddAsync(reservation);
            await dbContext.SaveChangesAsync();

            return Ok(reservation);
        }
    }
}
