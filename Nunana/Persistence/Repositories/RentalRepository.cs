using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Nunana.Core.Models;
using Nunana.Core.Repositories;

namespace Nunana.Persistence.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext _context;

        public RentalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Rental rental)
        {
            _context.Rentals.Add(rental);
        }

        public Rental GetRental(int roomId, int tenantId)
        {
            return _context.Rentals
                .SingleOrDefault(r => r.RoomId == roomId && r.TenantId == tenantId);
        }

        public IEnumerable<Rental> GetRentals(RentalQuery query)
        {
            var queryFromDb = _context.Rentals
                .Include(r => r.Room)
                .Include(r => r.Tenant)
                .AsQueryable();

            if (query.IsCancelled.HasValue && query.IsCancelled == true)
                queryFromDb = queryFromDb.Where(r => r.IsCancelled);

            if (query.IsCancelled.HasValue && query.IsCancelled == false)
                queryFromDb = queryFromDb.Where(r => !r.IsCancelled);

            if (query.StartDate.HasValue && query.EndDate.HasValue)
                queryFromDb = queryFromDb.Where(e => e.StartDate >= query.StartDate && e.EndDate <= query.EndDate);

            return queryFromDb.ToList();
        }
    }
}