using Nunana.Models;
using Nunana.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Nunana.Controllers
{
    [Authorize]
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RoomFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var userName = User.Identity.Name;
            var newRoom = new Room
            {
                CreatedBy = userName,
                RoomNumber = viewModel.RoomNumber,
                Type = viewModel.Type
            };
            _context.Rooms.Add(newRoom);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}