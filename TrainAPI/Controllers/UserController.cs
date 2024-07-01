using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;
using TrainAPI.Interface;
using TrainAPI.Models;

namespace TrainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService) {
             
            userService = _userService;
        }
        [HttpGet]
        [Route("fetchSeat")]
        public int getSeatCount() {
            return userService.GetSeatsCount();
        }

        [HttpPost]
        [Route("getAvailableSeats")]
        public List<SeatChart> getSeats(int num)
        {
            return userService.GetAvailableSeats(num);
        }

        [HttpGet]
        [Route("getAllSeats")]
        public List<List<SeatChart>> GetSeatChart()
        {
            return userService.GetSeats();
        }

        [HttpGet]
        [Route("bookSeats")]
        public int bookSeats()
        {
             return userService.BookSeats();
        }
    }
}
