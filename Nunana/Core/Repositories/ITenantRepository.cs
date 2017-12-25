using System.Collections.Generic;
using Nunana.Core.Models;

namespace Nunana.Core.Repositories
{
    public interface ITenantRepository
    {
        IEnumerable<Tenant> GetTenants();
        Tenant GetTenant(int id);
        void Add(Tenant tenant);
        IEnumerable<Tenant> GetTenantsWithName(string query);
    }
}