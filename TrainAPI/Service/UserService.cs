using TrainAPI.Data;
using TrainAPI.Interface;
using TrainAPI.Models;

namespace TrainAPI.Service
{
    public class UserService : IUserService
    {
        private readonly UserDBContext userDBContext;
        public UserService(UserDBContext context) {
            this.userDBContext = context;
        }
        public int BookSeats()
        {
            var seats = userDBContext.SeatCharts.Where(x=> x.SeatStatus == Status.Selected).ToList();

            foreach (var seat in seats)
            {
                seat.SeatStatus = Status.Booked;
            }
            userDBContext.SaveChanges();

            return seats.Count;
        }

        public List<SeatChart> GetAvailableSeats(int num)
        {
            var seats = new List<SeatChart>();
            var availableSeats = userDBContext.SeatCharts.Where(x => x.SeatStatus == Status.Available).GroupBy(x => x.RowNumber).Select(g => new { g.Key, Count = g.Count() }).ToList();
            var seatCount = this.GetSeatsCount();
            var counter = 0;
            if (seatCount >= num && num>0 && num < 8)
            {
                foreach (var seat in availableSeats)
                {
                    if (seat.Count >= num)
                    {
                        seats = userDBContext.SeatCharts.Where(x => x.SeatStatus == Status.Available && x.RowNumber == seat.Key).Take(num).ToList();
                        counter++;
                        break;
                    }
                }
                if (!seats.Any())
                {
                    for (int i = 0; i < availableSeats.Count()-1; i++)
                    {
                        if (availableSeats[i].Count + availableSeats[i + 1].Count >= num)
                        {
                            seats = userDBContext.SeatCharts.Where(x => x.SeatStatus == Status.Available && x.RowNumber>= availableSeats[i].Key).Take(num).ToList();
                            counter++;
                            break;
                        }
                    }                    
                }
                if (counter == 0)
                {
                    seats = userDBContext.SeatCharts.Where(x => x.SeatStatus == Status.Available).Take(num).ToList();
                }
                foreach(var seat in seats)
                {
                    seat.SeatStatus = Status.Selected;
                }
                userDBContext.SaveChanges();
                return seats;
                
            }
            else
            {
                throw new Exception("Not enough seats available");
            }
        }

        public List<List<SeatChart>> GetSeats()
        {
            return userDBContext.SeatCharts.GroupBy(x => x.RowNumber).Select(seats => seats.ToList()).ToList();
        }

        public int GetSeatsCount()
        {
            return userDBContext.SeatCharts.Where(x=>x.SeatStatus == Status.Available).Count();
        }
    }
}
