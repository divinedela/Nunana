using Nunana.DTOs;
using Nunana.Models;
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
            var vacantRooms = _context.Rooms.Where(c => !c.IsCurrentlyRented && (int)c.Type == roomType)
                .Select(a => new RoomDto
                {
                    Id = a.Id,
                    RoomNumber = a.RoomNumber,
                }).ToList();

            return Ok(vacantRooms);
        }
    }
}

