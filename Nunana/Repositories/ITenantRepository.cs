using System.Collections.Generic;
using Nunana.Models;

namespace Nunana.Repositories
{
    public interface ITenantRepository
    {
        IEnumerable<Tenant> GetTenants();
        Tenant GetTenant(int id);
        void Add(Tenant tenant);
        IEnumerable<Tenant> GetTenantsWithName(string query);
    }
}