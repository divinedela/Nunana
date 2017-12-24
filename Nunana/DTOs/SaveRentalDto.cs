namespace Nunana.DTOs
{
    public class SaveRentalDto
    {
        public int RoomId { get; set; }
        public int RoomType { get; set; }
        public int TenantId { get; set; }
        public string StartDate { get; set; }
        public int Months { get; set; }
    }
}