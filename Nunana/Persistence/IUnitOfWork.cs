using Nunana.Repositories;

namespace Nunana.Persistence
{
    public interface IUnitOfWork
    {
        IRoomRepository Rooms { get; }
        ITenantRepository Tenants { get; }
        IRentalRepository Rentals { get; }
        void Complete();
    }
}