﻿using System.Collections.Generic;
using System.Linq;
using Nunana.Core.Models;
using Nunana.Core.Repositories;

namespace Nunana.Persistence.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly ApplicationDbContext _context;

        public TenantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tenant> GetTenants()
        {
            return _context.Tenants.ToList();
        }

        public Tenant GetTenant(int id)
        {
            return _context.Tenants.SingleOrDefault(u => u.Id == id);
        }

        public void Add(Tenant tenant)
        {
            _context.Tenants.Add(tenant);
        }

        public IEnumerable<Tenant> GetTenantsWithName(string query)
        {
            return _context.Tenants
                .Where(c => c.FirstName.Contains(query) || c.LastName.Contains(query))
                .ToList();
        }
    }
}