using AutoMapper;
using Nunana.Models;
using Nunana.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Nunana.Controllers
{
    [Authorize]
    public class TenantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TenantsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var tenants = _context.Tenants.ToList();

            var viewModel = Mapper.Map<List<Tenant>, List<TenantsListViewModel>>(tenants);

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

            var tenant = new Tenant
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email,
                EmergencyContactFirstName = viewModel.EmergencyContactFirstName,
                EmergencyContactLastName = viewModel.EmergencyContactLastName,
                EmergencyContactAddress = viewModel.EmergencyContactAddress,
                EmergencyContactEmail = viewModel.EmergencyContactEmail,
                EmergencyContactPhoneNumber = viewModel.EmergencyContactPhoneNumber
            };

            _context.Tenants.Add(tenant);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var tenant = _context.Tenants.SingleOrDefault(u => u.Id == id);
            if (tenant == null) return HttpNotFound();

            var tenantViewModel = new TenantFormViewModel
            {
                FirstName = tenant.FirstName,
                LastName = tenant.LastName,
                Address = tenant.Address,
                PhoneNumber = tenant.PhoneNumber,
                Email = tenant.Email,
                EmergencyContactFirstName = tenant.FirstName,
                EmergencyContactLastName = tenant.EmergencyContactLastName,
                EmergencyContactAddress = tenant.EmergencyContactAddress,
                EmergencyContactEmail = tenant.EmergencyContactEmail,
                EmergencyContactPhoneNumber = tenant.EmergencyContactPhoneNumber
            };

            return View(tenantViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TenantFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var tenant = _context.Tenants.SingleOrDefault(u => u.Id == viewModel.Id);
            if (tenant == null) return HttpNotFound();

            tenant.FirstName = viewModel.FirstName;
            tenant.LastName = viewModel.LastName;
            tenant.Address = viewModel.Address;
            tenant.PhoneNumber = viewModel.PhoneNumber;
            tenant.Email = viewModel.Email;
            tenant.EmergencyContactFirstName = viewModel.FirstName;
            tenant.EmergencyContactLastName = viewModel.EmergencyContactLastName;
            tenant.EmergencyContactAddress = viewModel.EmergencyContactAddress;
            tenant.EmergencyContactEmail = viewModel.EmergencyContactEmail;
            tenant.EmergencyContactPhoneNumber = viewModel.EmergencyContactPhoneNumber;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}