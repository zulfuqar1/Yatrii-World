using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using YatriiWorld.Application.DTOs.Tickets;
using YatriiWorld.MVC.ViewModels.User;

namespace YatriiWorld.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {


        public async Task<IActionResult> Profile()
        {
        
            var token = Request.Cookies["JWTToken"];

            if (string.IsNullOrEmpty(token))
            {

                return RedirectToAction("Login", "Home");
            }


            var userIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "nameid" || c.Type == "sub")?.Value;
            var userName = User.Identity?.Name;
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var profilePic = User.Claims.FirstOrDefault(c => c.Type == "UserProfilePicture")?.Value;

            UserDetailsVM userDetails = null;

            try
            {
                using var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                var response = await client.GetAsync("https://localhost:7029/api/Users/GetProfile");

                if (response.IsSuccessStatusCode)
                {

                    userDetails = await response.Content.ReadFromJsonAsync<UserDetailsVM>();
                }
            }
            catch (Exception ex)
            {
             
                Console.WriteLine("API error: " + ex.Message);
            }

     
            if (userDetails == null)
            {
                userDetails = new UserDetailsVM
                {
                    Id = long.TryParse(userIdString, out long parsedId) ? parsedId : 0,
                    FirstName = userName ?? "unknown",
                    LastName = "",
                    Email = userEmail,
                    ProfileImageUrl = profilePic 
                };
            }
            else
            {
          
                if (string.IsNullOrEmpty(userDetails.ProfileImageUrl))
                {
                    userDetails.ProfileImageUrl = profilePic;
                }
            }

            return View("~/Views/Home/UserProfile.cshtml", userDetails);
        }


        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserDetailsVM model, IFormFile? ProfileImage)
        {
            var token = Request.Cookies["JWTToken"];
            if (string.IsNullOrEmpty(token)) return RedirectToAction("Login", "Home");

            // 🚀 ACİL ÇÖZÜM: Kullanıcı ID'sini doğrudan token claim'lerinden çekip zorla Modele atıyoruz.
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "nameid" || c.Type == "sub")?.Value;
            if (long.TryParse(userIdString, out long loggedInUserId))
            {
                model.Id = loggedInUserId;
            }

            try
            {
                if (ProfileImage != null && ProfileImage.Length > 0)
                {
                    var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "images", "UserPP");
                    if (!Directory.Exists(uploadDirectory)) Directory.CreateDirectory(uploadDirectory);

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfileImage.FileName);
                    var filePath = Path.Combine(uploadDirectory, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProfileImage.CopyToAsync(stream);
                    }
                    model.ProfileImageUrl = "images/UserPP/" + fileName;
                }

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using var content = new MultipartFormDataContent();

                // 🎯 EKSİK OLAN KRİTİK SATIR BURASIYDI! (ID'yi API'ye gönderiyoruz)
                content.Add(new StringContent(model.Id.ToString()), "Id");

                content.Add(new StringContent(model.FirstName ?? ""), "FirstName");
                content.Add(new StringContent(model.LastName ?? ""), "LastName");
                content.Add(new StringContent(model.Email ?? ""), "Email");
                content.Add(new StringContent(model.PhoneNumber ?? ""), "PhoneNumber");
                content.Add(new StringContent(model.Bio ?? ""), "Bio");
                content.Add(new StringContent(model.Address ?? ""), "Address");
                content.Add(new StringContent(model.City ?? ""), "City");
                content.Add(new StringContent(model.Region ?? ""), "Region");
                content.Add(new StringContent(model.Country ?? ""), "Country");
                content.Add(new StringContent(model.ZipCode ?? ""), "ZipCode");

                if (!string.IsNullOrEmpty(model.ProfileImageUrl))
                {
                    content.Add(new StringContent(model.ProfileImageUrl), "ProfileImageUrl");
                }

                var response = await client.PutAsync("https://localhost:7029/api/Users/UpdateProfile", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToAction("Profile");
                }

                var errorMsg = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", "API Error: " + errorMsg);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "System Error: " + ex.Message);
            }

            return View("~/Views/Home/UserProfile.cshtml", model);
        }





        [HttpGet]
        public async Task<IActionResult> MyTickets()
        {
            var token = Request.Cookies["JWTToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Home");
            }

            List<TicketDetailsDto> tickets = new List<TicketDetailsDto>();

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync("https://localhost:7029/api/Users/MyTickets");

                if (response.IsSuccessStatusCode)
                {
                    tickets = await response.Content.ReadFromJsonAsync<List<TicketDetailsDto>>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ticket API error: " + ex.Message);
                TempData["ErrorMessage"] = "Biletler yüklenirken bir sorun oluştu.";
            }

            return View("~/Views/Home/UserTickets.cshtml", tickets);
        }







    } 
}