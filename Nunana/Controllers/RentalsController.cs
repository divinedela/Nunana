﻿using Nunana.Models;
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

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var rentals = _context.Rentals
                .Include(r => r.Room)
                .Include(r => r.Tenant)
                .Where(r => !r.IsCancelled)
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
            var cancelledRooms = _context.Rentals
                .Include(r => r.Room)
                .Include(r => r.Tenant)
                .Where(r => r.IsCancelled)
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

            var rentals = _context.Rentals
                .Where(r => !r.IsCancelled)
                .Where(e => e.StartDate >= firstDayOfMonth && e.EndDate <= lastDayOfMonth)
                // .Where(e => e.EndDate <= lastDayOfMonth && e.StartDate >= firstDayOfMonth)
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