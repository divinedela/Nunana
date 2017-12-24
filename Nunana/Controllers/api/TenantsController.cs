using AutoMapper;
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
            var dto = new List<TenantSearchDto>();
            var tenantsQuery = _context.Tenants;

            if (!String.IsNullOrWhiteSpace(query))
            {
                var tenants = tenantsQuery
                    .Where(c => c.FirstName.Contains(query) || c.LastName.Contains(query))
                    .ToList();
                dto = Mapper.Map<List<Tenant>, List<TenantSearchDto>>(tenants);
            }

            return Ok(dto);
        }
    }
}
