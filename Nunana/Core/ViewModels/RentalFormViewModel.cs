using Nunana.Core.Models;

namespace Nunana.Core.ViewModels
{
    public class RentalFormViewModel
    {
        public int Id { get; set; }
        public RoomType RoomType { get; set; }
        public int NumberOfMonths { get; set; }
    }
}