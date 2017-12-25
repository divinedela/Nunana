using AutoMapper;
using Nunana.Persistence;
using System.Collections.Generic;
using System.Web.Http;
using Nunana.Core;
using Nunana.Core.DTOs;
using Nunana.Core.Models;

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

