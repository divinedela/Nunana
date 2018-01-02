using Nunana.Core;
using Nunana.Core.Models;
using Nunana.Core.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Nunana.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var vacantQuery = new RoomQuery { IsVacant = true };
            var rentedQuery = new RoomQuery { IsVacant = false };

            var roomsChartViewModel = new RoomsChartViewModel
            {
                VacantRoomsCount = _unitOfWork.Rooms.GetRooms(vacantQuery).Count(),
                RentedRoomsCount = _unitOfWork.Rooms.GetRooms(rentedQuery).Count()
            };
            return View(roomsChartViewModel);
        }
    }
}