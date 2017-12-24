﻿using Nunana.Models;
using System.Collections.Generic;
using System.Linq;

namespace Nunana.Repositories
{
    public class TenantRepository
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
    }
}