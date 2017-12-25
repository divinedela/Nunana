using AutoMapper;
using Nunana.Models;
using Nunana.Persistence;
using Nunana.Repositories;
using Nunana.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nunana.Controllers
{
    [Authorize]
    public class TenantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly TenantRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public TenantsController()
        {
            _context = new ApplicationDbContext();
            _repository = new TenantRepository(_context);
            _unitOfWork = new UnitOfWork(_context);
        }

        public ActionResult Index()
        {
            var tenants = _repository.GetTenants();

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

            _repository.Add(tenant);
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var tenant = _repository.GetTenant(id);
            if (tenant == null) return HttpNotFound();

            var viewModel = Mapper.Map<Tenant, TenantFormViewModel>(tenant);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TenantFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var tenant = _repository.GetTenant(viewModel.Id);
            if (tenant == null) return HttpNotFound();

            Mapper.Map(viewModel, tenant);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

    }

}