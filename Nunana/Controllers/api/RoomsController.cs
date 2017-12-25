using AutoMapper;
using Nunana.DTOs;
using Nunana.Models;
using Nunana.Persistence;
using System.Collections.Generic;
using System.Web.Http;

namespace Nunana.Controllers.api
{
    public class RoomsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public RoomsController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        [HttpGet]
        public IHttpActionResult GetRooms(int roomType)
        {
            var vacantRooms = _unitOfWork.Rooms.GetVacantRoomsOfType(roomType);

            var dto = Mapper.Map<IEnumerable<Room>, List<RoomDto>>(vacantRooms);

            return Ok(dto);
        }
    }
}

