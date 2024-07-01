namespace TrainAPI.Models
{
    public class SeatChart
    {
        public int Id { get; set; }
        public string? SeatNumber { get; set; }
        public Status SeatStatus { get; set; }
        public int RowNumber { get; set; }

    }
}
