using System;
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

        public Room()
        {
            DateCreated = DateTime.Now;
        }
    }

    public enum RoomType
    {
        OneBedroom = 1, Studio = 2, TwoBedroom = 3
    }
}