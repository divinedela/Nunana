using System.Collections.Generic;
using Nunana.Models;

namespace Nunana.Repositories
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms(RoomQuery roomQuery);
        Room GetRoom(int id);
        void Add(Room room);
    }
}