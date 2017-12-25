using Nunana.Models;
using Nunana.Persistence;
using Nunana.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Nunana.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        public ActionResult Index()
        {
            var rentals = _unitOfWork.Rentals.GetNotCancelledRentals()
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

        public ActionResult Cancelled()
        {
            var cancelledRooms = _unitOfWork.Rentals.GetCancelledRentals()
                .Select(a => new RentalListViewModel
                {
                    RoomNumber = a.Room.RoomNumber,
                    TenantName = a.Tenant.FirstName + ", " + a.Tenant.LastName,
                    RentalStartDate = a.StartDate.ToString(),
                    RentalEndDate = a.EndDate.ToString(),
                    RoomId = a.RoomId,
                    TenantId = a.TenantId,
                    CreatorName = a.CreatedBy,
                    CancelledBy = a.CancelledBy,
                    DateCancelled = a.DateCancelled.ToString(),
                    NumberOfMonths = DbFunctions.DiffMonths(a.StartDate, a.EndDate).ToString()
                }).ToList();

            return View(cancelledRooms);
        }

        public ActionResult ExpiringThisMonth()
        {
            var date = DateTime.Today;

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

            var rentals = _unitOfWork.Rentals.GetRentalsForTimePeriod(firstDayOfMonth, lastDayOfMonth)
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
    }
}