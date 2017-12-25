using AutoMapper;
using Nunana.Models;
using Nunana.Persistence;
using Nunana.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nunana.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public RoomsController()
        {
            _context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(_context);
        }

        public ActionResult Index()
        {
            var rooms = _unitOfWork.Rooms.GetRooms(null);

            var viewModel = Mapper.Map<IEnumerable<Room>, List<RoomsListViewModel>>(rooms);

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

            var roomFromMap = Mapper.Map<RoomFormViewModel, Room>(viewModel);

            var newRoom = new Room(roomFromMap.RoomNumber, roomFromMap.Type, User.Identity.Name);

            _unitOfWork.Rooms.Add(newRoom);
            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var room = _unitOfWork.Rooms.GetRoom(id);

            if (room == null) return HttpNotFound();

            var viewModel = Mapper.Map<Room, RoomFormViewModel>(room);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RoomFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            var room = _unitOfWork.Rooms.GetRoom(viewModel.Id);
            if (room == null) return HttpNotFound();

            Mapper.Map(viewModel, room);

            _unitOfWork.Complete();

            return RedirectToAction("Index");
        }

        public ActionResult Vacant()
        {
            var query = new RoomQuery { IsVacant = true };

            var vacantRooms = _unitOfWork.Rooms.GetRooms(query);

            var viewModel = Mapper.Map<IEnumerable<Room>, List<RoomsListViewModel>>(vacantRooms);

            return View(viewModel);
        }
    }
}