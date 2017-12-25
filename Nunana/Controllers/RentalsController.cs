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
        private readonly IUnitOfWork _unitOfWork;

        public RentalsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var query = new RentalQuery { IsCancelled = false };
            var rentals = _unitOfWork.Rentals.GetRentals(query);

            var viewModel = Mapper.Map<IEnumerable<Rental>, List<RentalListViewModel>>(rentals);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Cancelled()
        {
            var query = new RentalQuery { IsCancelled = true };
            var cancelledRooms = _unitOfWork.Rentals.GetRentals(query);

            var viewModel = Mapper.Map<IEnumerable<Rental>, List<RentalListViewModel>>(cancelledRooms);

            return View(viewModel);
        }

        public ActionResult ExpiringThisMonth()
        {
            var firstDayOfMonth = DateTimeExtensions.CalculateFirstDayOfThisMonth();
            var lastDayOfMonth = DateTimeExtensions.CalculateLastDayOfThisMonth(firstDayOfMonth);

            var query = new RentalQuery { StartDate = firstDayOfMonth, EndDate = lastDayOfMonth };
            var expiringRentals = _unitOfWork.Rentals.GetRentals(query);

            var viewModel = Mapper.Map<IEnumerable<Rental>, List<RentalListViewModel>>(expiringRentals);

            return View(viewModel);
        }
    }
}