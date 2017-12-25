using System.ComponentModel.DataAnnotations;

namespace Nunana.Core.ViewModels
{
    public class TenantsListViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Display(Name = "Date Registered")]
        public string DateRegistered { get; set; }

        [Display(Name = "Emergency Contact Number")]
        public string EmergencyContactPhoneNumber { get; set; }
    }
}