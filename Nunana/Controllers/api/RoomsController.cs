using AutoMapper;
using Nunana.DTOs;
using Nunana.Models;
using Nunana.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace Nunana.Controllers.api
{
    public class RoomsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly RoomRepository _repository;

        public RoomsController()
        {
            _context = new ApplicationDbContext();
            _repository = new RoomRepository(_context);
        }

        [HttpGet]
        public IHttpActionResult GetRooms(int roomType)
        {
            var vacantRooms = _repository.GetVacantRoomsOfType(roomType);

            var dto = Mapper.Map<IEnumerable<Room>, List<RoomDto>>(vacantRooms);

            return Ok(dto);
        }
    }
}

