using Nunana.Models;
using Nunana.ViewModels;
using System.Linq;
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
            var tenants = _context.Tenants.Select(a => new TenantsListViewModel
            {
                FullName = a.FirstName + ", " + a.LastName,
                PhoneNumber = a.PhoneNumber,
                Email = a.Email,
                Id = a.Id,
                EmergencyContactNumber = a.EmergencyContactFirstName + ", " + a.EmergencyContactLastName
            }).ToList();

            return View(tenants);
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

    }

}