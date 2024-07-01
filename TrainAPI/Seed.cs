using TrainAPI.Data;
using TrainAPI.Models;

namespace TrainAPI
{
    public class Seed
    {
        private readonly UserDBContext dataContext;
        public Seed(UserDBContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.SeatCharts.Any())
            {
               List<SeatChart> seats = new List<SeatChart>();
                var seatconfig = new string[] { "A", "B", "C", "D", "E", "F", "G" };

                for(int i = 0; i < 80; i++)
                {
                    var seat = new SeatChart()
                    {
                        SeatStatus = Status.Available,
                        SeatNumber = ((i / 7)+1) + seatconfig[(i % 7)],
                        RowNumber = (i / 7)+1,
                    };
                    seats.Add(seat);
                }
                dataContext.SeatCharts.AddRange(seats);
                dataContext.SaveChanges();
            }
         
        }
    }
}
