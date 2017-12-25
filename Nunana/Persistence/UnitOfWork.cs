using Nunana.Models;
using Nunana.Repositories;

namespace Nunana.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository Rooms { get; private set; }
        public TenantRepository Tenants { get; private set; }
        public RentalRepository Rentals { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Rooms = new RoomRepository(_context);
            Tenants = new TenantRepository(_context);
            Rentals = new RentalRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}