using AutoMapper;
using Nunana.DTOs;
using Nunana.Extensions;
using Nunana.Models;
using Nunana.Persistence;
using System.Web.Http;

namespace Nunana.Controllers.api
{
    public class RentalsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            var startDate = DateTimeExtensions.ConvertToDateTime(saveRentalDto.StartDate);
            var endDate = DateTimeExtensions.GetRentalEndDate(saveRentalDto, startDate);

            var rentalFromMap = Mapper.Map<SaveRentalDto, Rental>(saveRentalDto);
            var userName = User.Identity.Name;
            var newRental = new Rental(rentalFromMap.RoomId, rentalFromMap.TenantId, startDate, endDate, userName);

            room.SetOccupied();

            _unitOfWork.Rentals.Add(newRental);
            _unitOfWork.Complete();

            return Ok();
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