using AutoMapper;
using Nunana.DTOs;
using Nunana.Models;
using Nunana.Persistence;
using System;
using System.Globalization;
using System.Web.Http;

namespace Nunana.Controllers.api
{
    public class RentalsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
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

            var room = _unitOfWork.Rooms.GetRoom(saveRentalDto.RoomId);
            if (room == null)
                return NotFound();

            if (room.IsCurrentlyRented)
                return BadRequest("Room is already rented out");

            var tenant = _unitOfWork.Tenants.GetTenant(saveRentalDto.TenantId);
            if (tenant == null)
                return NotFound();

            var startDate = ConvertToDateTime(saveRentalDto.StartDate);
            var endDate = GetRentalEndDate(saveRentalDto, startDate);

            var rentalFromMap = Mapper.Map<SaveRentalDto, Rental>(saveRentalDto);
            var userName = User.Identity.Name;
            var newRental = new Rental(rentalFromMap.RoomId, rentalFromMap.TenantId, startDate, endDate, userName);

            room.SetOccupied();

            _unitOfWork.Rentals.Add(newRental);
            _unitOfWork.Complete();

            return Ok();
        }

        private static DateTime GetRentalEndDate(SaveRentalDto saveRentalDto, DateTime startDate)
        {
            var numberOfMonths = saveRentalDto.Months;
            var endDate = startDate.AddMonths(numberOfMonths);
            return endDate;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int roomId, int tenantId)
        {
            var rental = _unitOfWork.Rentals.GetRental(roomId, tenantId);
            if (rental == null) return NotFound();

            if (rental.IsCancelled) return BadRequest("Rental already cancelled");

            var userName = User.Identity.Name;
            rental.Cancel(userName);

            var room = _unitOfWork.Rooms.GetRoom(roomId);
            if (room == null) return NotFound();

            if (!room.IsCurrentlyRented) return BadRequest("Room is not rented currently ");

            room.SetVacant();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}