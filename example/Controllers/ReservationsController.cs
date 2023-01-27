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
            var activeReservations = await dbContext.Reservations.Where(r => !r.IsDeleted).ToListAsync();

            return Ok(activeReservations);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetReservation([FromRoute] Guid id)
        {
            var reservation = await dbContext.Reservations.FindAsync(id);

            if (reservation != null)
            {
                return Ok(reservation);
            }

            return NotFound();
        }

        //[HttpGet]


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

            dbContext.Reservations.AddAsync(reservation);
            dbContext.SaveChanges();

            return Ok(reservation);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateReservation([FromRoute] Guid id, UpdateReservationRequest UpdateReservationRequest)
        {
            var reservation = await dbContext.Reservations.FindAsync(id);

            if (reservation != null)
            {
                reservation.ClientName = UpdateReservationRequest.ClientName;
                reservation.Location = UpdateReservationRequest.Location;
                reservation.Date = UpdateReservationRequest.Date;

                await dbContext.SaveChangesAsync();

                return Ok(reservation);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] Guid id)
        {
            var reservation = await dbContext.Reservations.FindAsync(id);

            if (reservation != null)
            {
                dbContext.Reservations.Remove(reservation);
                dbContext.SaveChanges();

                return Ok(reservation);
            }

            return NotFound();
        }


    }
}
