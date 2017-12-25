using AutoMapper;
using Nunana.DTOs;
using Nunana.Models;
using Nunana.Repositories;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Nunana.Controllers.api
{
    public class TenantsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        private readonly TenantRepository _repository;
        public TenantsController()
        {
            _context = new ApplicationDbContext();
            _repository = new TenantRepository(_context);
        }

        [HttpGet]
        public IHttpActionResult GetTenants(string query = null)
        {
            var dto = new List<TenantSearchDto>();

            if (String.IsNullOrWhiteSpace(query)) return Ok(dto);

            var tenants = _repository.GetTenantsWithNameQuery(query);
            dto = Mapper.Map<IEnumerable<Tenant>, List<TenantSearchDto>>(tenants);

            return Ok(dto);
        }
    }
}
