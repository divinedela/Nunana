using System.Collections.Generic;
using System.Linq;
using Nunana.Core.Models;
using Nunana.Core.Repositories;

namespace Nunana.Persistence.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetRooms(RoomQuery roomQuery)
        {
            var query = _context.Rooms.AsQueryable();

            if (roomQuery != null && roomQuery.RoomType.HasValue)
                query = query.Where(c => (int)c.Type == roomQuery.RoomType.Value);

            if (roomQuery != null && roomQuery.IsVacant.HasValue && roomQuery.IsVacant.Value)
                query = query.Where(i => !i.IsCurrentlyRented);

            return query.ToList();
        }

        public Room GetRoom(int id)
        {
            return _context.Rooms.SingleOrDefault(i => i.Id == id);
        }

        public void Add(Room room)
        {
            _context.Rooms.Add(room);
        }
    }
}