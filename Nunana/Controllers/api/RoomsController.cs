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
        private readonly IUnitOfWork _unitOfWork;

        public RoomsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult GetRooms(int roomType)
        {
            var query = new RoomQuery { RoomType = roomType, IsVacant = true };

            var vacantRooms = _unitOfWork.Rooms.GetRooms(query);

            var dto = Mapper.Map<IEnumerable<Room>, List<RoomDto>>(vacantRooms);

            return Ok(dto);
        }
    }
}

