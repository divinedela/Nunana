using Nunana.Core;
using Nunana.Core.Models;
using Nunana.Core.Repositories;
using Nunana.Persistence.Repositories;

namespace Nunana.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRoomRepository Rooms { get; private set; }
        public ITenantRepository Tenants { get; private set; }
        public IRentalRepository Rentals { get; private set; }

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