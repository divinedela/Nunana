using System;

namespace Nunana.Models
{
    public class RentalQuery
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCancelled { get; set; }
    }
}