using Nunana.DTOs;
using Nunana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Nunana.Controllers.api
{
    public class TenantsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public TenantsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetTenants(string query = null)
        {
            var tenantsQuery = _context.Tenants;
            var tenants = new List<TenantSearchDto>();

            if (!String.IsNullOrWhiteSpace(query))
            {
                tenants = tenantsQuery.Where(c => c.FirstName.Contains(query) || c.LastName.Contains(query))
                    .Select(a => new TenantSearchDto
                    {
                        Id = a.Id,
                        Name = a.FirstName + " " + a.LastName
                    }).ToList();
            }

            return Ok(tenants);
        }
    }
}
