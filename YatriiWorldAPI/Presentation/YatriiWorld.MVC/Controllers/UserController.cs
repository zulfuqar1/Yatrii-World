using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
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
        public async Task<IActionResult> UpdateProfile(UserDetailsVM model, IFormFile? ProfileImage)
        {

            var token = Request.Cookies["JWTToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Home");
            }

            try
            {


                using var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.PutAsJsonAsync("https://localhost:7029/api/Users/UpdateProfile", model);

                if (response.IsSuccessStatusCode)
                {
                
                    TempData["SuccessMessage"] = "Your profile has been successfully updated!";
                    return RedirectToAction("Profile");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Update failed: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred:" + ex.Message);
            }

            return View("~/Views/Home/UserProfile.cshtml", model);
        }
    } 
}