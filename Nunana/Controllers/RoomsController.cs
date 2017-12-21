using Nunana.Models;
using Nunana.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Nunana.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var rooms = _context.Rooms
                .Select(a => new RoomsListViewModel
                {
                    Id = a.Id,
                    IsCurrentlyRented = a.IsCurrentlyRented,
                    RoomNumber = a.RoomNumber,
                    RoomType = a.Type.ToString()
                }).ToList();

            return View(rooms);
        }
    }
}