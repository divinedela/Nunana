using Nunana.Models;

namespace Nunana.ViewModels
{
    public class RentalFormViewModel
    {
        public int Id { get; set; }
        public RoomType RoomType { get; set; }
        public int NumberOfMonths { get; set; }
    }
}