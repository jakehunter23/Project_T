using TrainAPI.Models;

namespace TrainAPI.Interface
{
    public interface IUserService
    {
        public List<List<SeatChart>> GetSeats();

        public int GetSeatsCount();


        public int BookSeats();

        public List<SeatChart> GetAvailableSeats(int num);
    }
}
