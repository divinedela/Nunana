using System.ComponentModel.DataAnnotations;
using Nunana.Core.Models;

namespace Nunana.Core.ViewModels
{
    public class RoomFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Room Number can have maximum of 10 characters and a minimum of 2")]
        public string RoomNumber { get; set; }

        [Required]
        [Display(Name = "Room Type")]
        public RoomType Type { get; set; }
    }
}