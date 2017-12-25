using Nunana.Models;
using System;
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
            return _context.Rentals
                .SingleOrDefault(r => r.RoomId == roomId && r.TenantId == tenantId);
        }

        public IEnumerable<Rental> GetRentalsWithRoomsAndTenants()
        {
            return _context.Rentals
                .Include(r => r.Room)
                .Include(r => r.Tenant)
                .ToList();
        }

        public IEnumerable<Rental> GetNotCancelledRentals()
        {
            return _context.Rentals
                .Include(r => r.Room)
                .Include(r => r.Tenant)
                .Where(r => !r.IsCancelled)
                .ToList();
        }

        public IEnumerable<Rental> GetCancelledRentals()
        {
            return _context.Rentals
                .Include(r => r.Room)
                .Include(r => r.Tenant)
                .Where(r => !r.IsCancelled)
                .ToList();
        }

        public IEnumerable<Rental> GetRentalsForTimePeriod(DateTime startDate, DateTime endDate)
        {
            return _context.Rentals
                .Include(r => r.Room)
                .Include(r => r.Tenant)
                .Where(r => !r.IsCancelled)
                .Where(e => e.StartDate >= startDate && e.EndDate <= endDate);
        }
    }
}