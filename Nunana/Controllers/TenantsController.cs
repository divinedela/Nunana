using AutoMapper;
using Nunana.Persistence;
using System.Collections.Generic;
using System.Web.Mvc;
using Nunana.Core;
using Nunana.Core.Models;
using Nunana.Core.ViewModels;

namespace Nunana.Controllers
{
    [Authorize]
    public class TenantsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TenantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var tenants = _unitOfWork.Tenants.GetTenants();

            var viewModel = Mapper.Map<IEnumerable<Tenant>, List<TenantsListViewModel>>(tenants);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TenantFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var tenant = Mapper.Map<TenantFormViewModel, Tenant>(viewModel);

            _unitOfWork.Tenants.Add(tenant);
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var tenant = _unitOfWork.Tenants.GetTenant(id);
            if (tenant == null) return HttpNotFound();

            var viewModel = Mapper.Map<Tenant, TenantFormViewModel>(tenant);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TenantFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var tenant = _unitOfWork.Tenants.GetTenant(viewModel.Id);
            if (tenant == null) return HttpNotFound();

            Mapper.Map(viewModel, tenant);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

    }

}