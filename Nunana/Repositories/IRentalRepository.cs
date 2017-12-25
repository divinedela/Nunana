using System.Collections.Generic;
using Nunana.Models;

namespace Nunana.Repositories
{
    public interface IRentalRepository
    {
        void Add(Rental rental);
        Rental GetRental(int roomId, int tenantId);
        IEnumerable<Rental> GetRentals(RentalQuery query);
    }
}