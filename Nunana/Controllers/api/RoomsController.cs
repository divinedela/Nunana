using AutoMapper;
using Nunana.DTOs;
using Nunana.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Nunana.Controllers.api
{
    public class RoomsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public RoomsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetRooms(int roomType)
        {
            var vacantRooms = _context.Rooms
                .Where(c => !c.IsCurrentlyRented && (int)c.Type == roomType)
                .ToList();

            var dto = Mapper.Map<List<Room>, List<RoomDto>>(vacantRooms);

            return Ok(dto);
        }
    }
}

