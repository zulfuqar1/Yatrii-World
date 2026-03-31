using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.MVC.Services.Implementations;
using YatriiWorld.MVC.Services.Interfaces;
using YatriiWorld.MVC.ViewModels.Booking;

namespace YatriiWorld.MVC.Controllers
{
    [Route("Shop")]
    public class ShopController : Controller
    {
        private readonly ITourClientService _tourService;

        private readonly ITicketClientService _ticketService;

        public ShopController(ITourClientService tourService, ITicketClientService ticketService)
        {
            _tourService = tourService;
            _ticketService = ticketService;
        }

        [Route("TourShop")]
        public async Task<IActionResult> TourShop()
        {
            var tours = await _tourService.GetAllAsync();
            return View(tours);
        }





        [HttpGet("Booking/{id}")]
        [Authorize]
        public async Task<IActionResult> Booking(int id, int count = 1)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if (tour == null) return NotFound();

            ViewBag.Tour = tour;
            var model = new TicketCreateVM
            {
                TourId = id,
                TotalPersonCount = count
            };

            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> ConfirmBooking(TicketCreateVM model)
        {
            ModelState.Remove("Email");


            var userIdString = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                               ?? User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

            if (!string.IsNullOrEmpty(userIdString) && long.TryParse(userIdString, out long userId))
            {
                model.AppUserId = userId;
            }
            else
            {
                ModelState.AddModelError("", "Security error: User ID not found. Please log in again.");
                ViewBag.Tour = await _tourService.GetByIdAsync(model.TourId);
                return View("Booking", model);
            }
            // --------------------------------------------

            if (!ModelState.IsValid)
            {
                ViewBag.Tour = await _tourService.GetByIdAsync(model.TourId);
                return View("Booking", model);
            }

        
            if (string.IsNullOrEmpty(model.Email))
            {
                model.Email = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value
                               ?? User.Identity?.Name;
            }

            if (string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = "000-000-00-00";
            }

            var isSuccess = await _ticketService.CreateBookingAsync(model);

            if (isSuccess)
            {
                TempData["BookingData"] = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                return RedirectToAction("Confirmation");
            }

            ModelState.AddModelError("", "The reservation was rejected by API..");
            ViewBag.Tour = await _tourService.GetByIdAsync(model.TourId);
            return View("Booking", model);
        }



        [HttpGet]
        public async Task<IActionResult> Confirmation()
        {
        
            if (TempData["BookingData"] == null)
            {
                return RedirectToAction("TourShop");
            }


            var modelJson = TempData["BookingData"].ToString();
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<TicketCreateVM>(modelJson);


            ViewBag.Tour = await _tourService.GetByIdAsync(model.TourId);

 
            ViewBag.BookingNumber = "YTR-" + new Random().Next(10000, 99999) + "-" + model.TourId;

            return View(model);
        }


    }
}