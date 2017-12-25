using System.ComponentModel.DataAnnotations;

namespace Nunana.Core.ViewModels
{
    public class TenantFormViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Emergency Contact First Name")]
        public string EmergencyContactFirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Emergency Contact Last Name")]
        public string EmergencyContactLastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Emergency Contact Address")]
        public string EmergencyContactAddress { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Emergency Contact Email")]
        public string EmergencyContactEmail { get; set; }

        [Required]
        [StringLength(15)]
        [Display(Name = "Emergency Contact Number ")]
        public string EmergencyContactPhoneNumber { get; set; }

    }
}