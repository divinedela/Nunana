using AutoMapper;
using Nunana.DTOs;
using Nunana.Models;
using Nunana.Persistence;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Nunana.Controllers.api
{
    public class TenantsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;
        public TenantsController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        [HttpGet]
        public IHttpActionResult GetTenants(string query = null)
        {
            var dto = new List<TenantSearchDto>();

            if (String.IsNullOrWhiteSpace(query)) return Ok(dto);

            var tenants = _unitOfWork.Tenants.GetTenantsWithNameQuery(query);
            dto = Mapper.Map<IEnumerable<Tenant>, List<TenantSearchDto>>(tenants);

            return Ok(dto);
        }
    }
}
