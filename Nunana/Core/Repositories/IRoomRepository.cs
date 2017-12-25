using System.Collections.Generic;
using Nunana.Core.Models;

namespace Nunana.Core.Repositories
{
    public interface IRoomRepository
    {
        IEnumerable<Room> GetRooms(RoomQuery roomQuery);
        Room GetRoom(int id);
        void Add(Room room);
    }
}