using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using Nunana.Models;
using Nunana.ViewModels;

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
            var rooms = _context.Rooms.ToList();

            var viewModel = Mapper.Map<List<Room>, List<RoomsListViewModel>>(rooms);

            return View(viewModel);
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

            var newRoom = Mapper.Map<RoomFormViewModel, Room>(viewModel);
            newRoom.CreatedBy = User.Identity.Name;

            _context.Rooms.Add(newRoom);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var room = _context.Rooms
                .SingleOrDefault(u => u.Id == id);

            if (room == null) return HttpNotFound();
            var viewModel = Mapper.Map<Room, RoomFormViewModel>(room);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoomFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var room = _context.Rooms.SingleOrDefault(u => u.Id == viewModel.Id);
            if (room == null) return HttpNotFound();

            Mapper.Map(viewModel, room);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Vacant()
        {
            var vacantRooms = _context.Rooms.Where(i => !i.IsCurrentlyRented).ToList();

            var viewModel = Mapper.Map<List<Room>, List<RoomsListViewModel>>(vacantRooms);

            return View(viewModel);
        }
    }
}