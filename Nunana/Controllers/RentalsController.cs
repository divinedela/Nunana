using AutoMapper;
using Nunana.Extensions;
using Nunana.Models;
using Nunana.Persistence;
using Nunana.ViewModels;
using System.Collections.Generic;
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
            var rentals = _unitOfWork.Rentals.GetNotCancelledRentals();

            var viewModel = Mapper.Map<IEnumerable<Rental>, List<RentalListViewModel>>(rentals);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Cancelled()
        {
            var cancelledRooms = _unitOfWork.Rentals.GetCancelledRentals();

            var viewModel = Mapper.Map<IEnumerable<Rental>, List<RentalListViewModel>>(cancelledRooms);

            return View(viewModel);
        }

        public ActionResult ExpiringThisMonth()
        {
            var firstDayOfMonth = DateTimeExtensions.CalculateFirstDayOfThisMonth();
            var lastDayOfMonth = DateTimeExtensions.CalculateLastDayOfThisMonth(firstDayOfMonth);

            var expiringRentals = _unitOfWork.Rentals.GetRentalsForTimePeriod(firstDayOfMonth, lastDayOfMonth);

            var viewModel = Mapper.Map<IEnumerable<Rental>, List<RentalListViewModel>>(expiringRentals);

            return View(viewModel);
        }
    }
}