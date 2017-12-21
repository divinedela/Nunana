using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Nunana.Models
{
    public class Tenant
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        public string FullName { get { return FirstName + ", " + LastName; } }

        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string EmergencyContactFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string EmergencyContactLastName { get; set; }

        [Required]
        [StringLength(15)]
        public string EmergencyContactPhoneNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string EmergencyContactAddress { get; set; }

        [Required]
        [EmailAddress]
        public string EmergencyContactEmail { get; set; }

        public DateTime DateCreated { get; set; }
        public ICollection<Rental> Rentals { get; set; }

        public Tenant()
        {
            DateCreated = DateTime.Now;
            Rentals = new Collection<Rental>();
        }
    }
}