using Nunana.Models;
using Nunana.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Nunana.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var rentals = _context.Rentals
                .Include(r => r.Room)
                .Include(r => r.Tenant)
                .Where(r => r.IsCancelled == false)
                .Select(a => new RentalListViewModel
                {
                    RoomNumber = a.Room.RoomNumber,
                    TenantName = a.Tenant.FirstName + ", " + a.Tenant.LastName,
                    RentalStartDate = a.StartDate.ToString(),
                    RentalEndDate = a.EndDate.ToString(),
                    RoomId = a.RoomId,
                    TenantId = a.TenantId,
                    CreatorName = a.CreatedBy,
                    NumberOfMonths = DbFunctions.DiffMonths(a.StartDate, a.EndDate).ToString()
                }).ToList();

            return View(rentals);
        }


        public ActionResult Create()
        {
            return View();
        }
    }

}