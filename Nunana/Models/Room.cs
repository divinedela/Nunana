using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Nunana.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 2)]
        public string RoomNumber { get; set; }

        public RoomType Type { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCurrentlyRented { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public ICollection<Rental> Rentals { get; set; }

        public Room()
        {
            DateCreated = DateTime.Now;
            Rentals = new Collection<Rental>();
        }
    }

    public enum RoomType
    {
        OneBedroom = 1, Studio = 2, TwoBedroom = 3
    }
}