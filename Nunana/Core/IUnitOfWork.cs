using Nunana.Core.Repositories;

namespace Nunana.Core
{
    public interface IUnitOfWork
    {
        IRoomRepository Rooms { get; }
        ITenantRepository Tenants { get; }
        IRentalRepository Rentals { get; }
        void Complete();
    }
}