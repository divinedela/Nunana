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
                return NotFound();

            if (room.IsCurrentlyRented)
                return BadRequest("Room is already rented out");

            var tenant = _context.Tenants.SingleOrDefault(r => r.Id == saveRentalDto.TenantId);
            if (tenant == null)
                return NotFound();

            var startDate = ConvertToDateTime(saveRentalDto.StartDate);
            var numberOfMonths = saveRentalDto.Months;
            var endDate = startDate.AddMonths(numberOfMonths);

            var rentalFromMap = Mapper.Map<SaveRentalDto, Rental>(saveRentalDto);
            var userName = User.Identity.Name;
            var newRental = new Rental(rentalFromMap.RoomId, rentalFromMap.TenantId, startDate, endDate, userName);

            room.IsCurrentlyRented = true;

            _context.Rentals.Add(newRental);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int roomId, int tenantId)
        {
            var rental = _context.Rentals.SingleOrDefault(r => r.RoomId == roomId && r.TenantId == tenantId);
            if (rental == null) return NotFound();

            if (rental.IsCancelled) return BadRequest("Rental already cancelled");

            var userName = User.Identity.Name;
            rental.Cancel(userName);

            var room = _context.Rooms.SingleOrDefault(r => r.Id == roomId);
            if (room == null) return NotFound();

            if (!room.IsCurrentlyRented) return BadRequest("Room is not rented currently ");

            room.IsCurrentlyRented = false;

            _context.SaveChanges();

            return Ok();
        }
    }
}