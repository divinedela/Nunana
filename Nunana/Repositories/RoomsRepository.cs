using Nunana.Models;
using System.Collections.Generic;
using System.Linq;

namespace Nunana.Repositories
{
    public class RoomsRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetRooms()
        {
            return _context.Rooms.ToList();
        }
        public Room GetRoom(int id)
        {
            return _context.Rooms.SingleOrDefault(i => i.Id == id);
        }

        public IEnumerable<Room> GetVacantRooms()
        {
            return _context.Rooms.Where(i => !i.IsCurrentlyRented).ToList();
        }
    }
}