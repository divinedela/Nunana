using AutoMapper;
using Nunana.DTOs;
using Nunana.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Http;

namespace Nunana.Controllers.api
{
    public class RentalsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        private static DateTime ConvertToDateTime(string dateString)
        {
            return DateTime.ParseExact(dateString,
                "ddd MMM dd yyyy HH:mm:ss 'GMT'K '(GMT Standard Time)'",
                CultureInfo.InvariantCulture);
        }

        [HttpPost]
        public IHttpActionResult CreateRental(SaveRentalDto saveRentalDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var room = _context.Rooms.SingleOrDefault(r => r.Id == saveRentalDto.RoomId);
            if (room == null)
                return BadRequest("Room does not exist");

            if (room.IsCurrentlyRented)
                return BadRequest("Room is already rented out");

            var tenant = _context.Tenants.SingleOrDefault(r => r.Id == saveRentalDto.TenantId);
            if (tenant == null)
                return BadRequest("Tenant does not exist");

            var rental = Mapper.Map<SaveRentalDto, Rental>(saveRentalDto);

            var startDate = ConvertToDateTime(saveRentalDto.StartDate);
            var numberOfMonths = saveRentalDto.Months;
            var endDate = startDate.AddMonths(numberOfMonths);

            rental.StartDate = startDate;
            rental.EndDate = endDate;
            rental.CreatedBy = User.Identity.Name;

            room.IsCurrentlyRented = true;

            _context.Rentals.Add(rental);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int roomId, int tenantId)
        {
            var rental = _context.Rentals.SingleOrDefault(r => r.RoomId == roomId && r.TenantId == tenantId);
            if (rental == null) return NotFound();

            if (rental.IsCancelled) return BadRequest("Rental already cancelled");

            var username = User.Identity.Name;
            rental.IsCancelled = true;
            rental.CancelledBy = username;
            rental.DateCancelled = DateTime.Now;

            var room = _context.Rooms.SingleOrDefault(r => r.Id == roomId);
            if (room == null) return NotFound();

            if (!room.IsCurrentlyRented) return BadRequest("Room is not rented currently ");

            room.IsCurrentlyRented = false;

            _context.SaveChanges();

            return Ok();
        }
    }
}