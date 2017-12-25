using AutoMapper;
using Nunana.DTOs;
using Nunana.Models;
using Nunana.Persistence;
using Nunana.Repositories;
using System;
using System.Globalization;
using System.Web.Http;

namespace Nunana.Controllers.api
{
    public class RentalsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly RentalRepository _rentalRepository;
        private readonly RoomRepository _roomRepository;
        private readonly TenantRepository _tenantRepository;
        private readonly UnitOfWork _unitOfWork;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
            _rentalRepository = new RentalRepository(_context);
            _roomRepository = new RoomRepository(_context);
            _tenantRepository = new TenantRepository(_context);
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

            var room = _roomRepository.GetRoom(saveRentalDto.RoomId);
            if (room == null)
                return NotFound();

            if (room.IsCurrentlyRented)
                return BadRequest("Room is already rented out");

            var tenant = _tenantRepository.GetTenant(saveRentalDto.TenantId);
            if (tenant == null)
                return NotFound();

            var startDate = ConvertToDateTime(saveRentalDto.StartDate);
            var endDate = GetRentalEndDate(saveRentalDto, startDate);

            var rentalFromMap = Mapper.Map<SaveRentalDto, Rental>(saveRentalDto);
            var userName = User.Identity.Name;
            var newRental = new Rental(rentalFromMap.RoomId, rentalFromMap.TenantId, startDate, endDate, userName);

            room.SetOccupied();

            _rentalRepository.Add(newRental);
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
            var rental = _rentalRepository.GetRental(roomId, tenantId);
            if (rental == null) return NotFound();

            if (rental.IsCancelled) return BadRequest("Rental already cancelled");

            var userName = User.Identity.Name;
            rental.Cancel(userName);

            var room = _roomRepository.GetRoom(roomId);
            if (room == null) return NotFound();

            if (!room.IsCurrentlyRented) return BadRequest("Room is not rented currently ");

            room.SetVacant();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}