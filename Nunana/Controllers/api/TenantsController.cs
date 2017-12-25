using AutoMapper;
using Nunana.Persistence;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Nunana.Core;
using Nunana.Core.DTOs;
using Nunana.Core.Models;

namespace Nunana.Controllers.api
{
    public class TenantsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TenantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult GetTenants(string query = null)
        {
            var dto = new List<TenantSearchDto>();

            if (String.IsNullOrWhiteSpace(query)) return Ok(dto);

            var tenants = _unitOfWork.Tenants.GetTenantsWithName(query);
            dto = Mapper.Map<IEnumerable<Tenant>, List<TenantSearchDto>>(tenants);

            return Ok(dto);
        }
    }
}
