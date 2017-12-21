using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nunana.Models
{
    public class Rental
    {
        [Key]
        [Column(Order = 1)]
        public int RoomId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int TenantId { get; set; }

        [Required]
        public Room Room { get; set; }

        [Required]
        public Tenant Tenant { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public bool IsCancelled { get; set; }
        public string CancelledBy { get; set; }
        public DateTime? DateCancelled { get; set; }

        public Rental()
        {
            DateCreated = DateTime.Now;
        }

    }
}