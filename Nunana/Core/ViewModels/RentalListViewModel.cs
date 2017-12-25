using System.ComponentModel.DataAnnotations;

namespace Nunana.Core.ViewModels
{
    public class RentalListViewModel
    {
        public int RoomId { get; set; }
        public int TenantId { get; set; }

        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }

        [Display(Name = "Tenant Name")]
        public string TenantName { get; set; }

        [Display(Name = "Rental Start Date")]
        public string RentalStartDate { get; set; }

        [Display(Name = "Rental End Date")]
        public string RentalEndDate { get; set; }

        [Display(Name = "Number of Months")]
        public string NumberOfMonths { get; set; }

        [Display(Name = "Created By")]
        public string CreatorName { get; set; }

        [Display(Name = "Cancelled By")]
        public string CancelledBy { get; set; }

        [Display(Name = "Date Cancelled")]
        public string DateCancelled { get; set; }
    }
}