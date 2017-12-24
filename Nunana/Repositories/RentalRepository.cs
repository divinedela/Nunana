using Nunana.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Nunana.Repositories
{
    public class RentalRepository
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
            return _context.Rentals.SingleOrDefault(r => r.RoomId == roomId && r.TenantId == tenantId);
        }

        public IEnumerable<Rental> GetRentalsWithRoomsAndTenants()
        {
            return _context.Rentals
                .Include(r => r.Room)
                .Include(r => r.Tenant).ToList();
        }

        public IEnumerable<Rental> GetRentals()
        {
            return _context.Rentals.ToList();
        }
    }
}