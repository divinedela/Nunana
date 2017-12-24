using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nunana.Models
{
    public class Rental
    {
        [Key]
        [Column(Order = 1)]
        public int RoomId { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int TenantId { get; private set; }

        public Room Room { get; set; }

        public Tenant Tenant { get; set; }

        public DateTime DateCreated { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        [Required]
        public string CreatedBy { get; private set; }

        public bool IsCancelled { get; private set; }
        public string CancelledBy { get; private set; }
        public DateTime? DateCancelled { get; private set; }

        public void Cancel(string userName)
        {
            IsCancelled = true;
            CancelledBy = userName;
            DateCancelled = DateTime.Now;
        }

        protected Rental()
        {
        }

        public Rental(int roomId, int tenantId, DateTime startDate, DateTime endDate, string creator)
        {
            StartDate = startDate;
            EndDate = endDate;
            CreatedBy = creator;
            RoomId = roomId;
            TenantId = tenantId;
            DateCreated = DateTime.Now;
        }
    }
}