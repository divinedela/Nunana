using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Nunana.Core.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 2)]
        public string RoomNumber { get; private set; }

        public RoomType Type { get; private set; }
        public DateTime DateCreated { get; private set; }
        public bool IsCurrentlyRented { get; private set; }

        [Required]
        public string CreatedBy { get; private set; }

        public ICollection<Rental> Rentals { get; set; }

        protected Room()
        {

        }
        public Room(string roomNumber, RoomType roomType, string creator)
        {
            DateCreated = DateTime.Now;
            CreatedBy = creator;
            Type = roomType;
            RoomNumber = roomNumber;

            Rentals = new Collection<Rental>();
        }

        public void SetVacant()
        {
            IsCurrentlyRented = false;
        }

        public void SetOccupied()
        {
            IsCurrentlyRented = true;
        }
    }

    public enum RoomType
    {
        OneBedroom = 1, Studio = 2, TwoBedroom = 3
    }
}