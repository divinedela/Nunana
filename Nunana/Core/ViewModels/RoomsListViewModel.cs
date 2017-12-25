using System.ComponentModel.DataAnnotations;

namespace Nunana.Core.ViewModels
{
    public class RoomsListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }

        [Display(Name = "Is Currently Rented?")]
        public bool IsCurrentlyRented { get; set; }

        [Display(Name = "Room Type")]
        public string RoomType { get; set; }
    }
}