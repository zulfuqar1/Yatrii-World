using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using YatriiWorld.Application.DTOs.Reviews;
using YatriiWorld.MVC.Services.Interfaces;
using YatriiWorld.MVC.ViewModels.LoginRegister;
using YatriiWorld.MVC.ViewModels.Register;

namespace YatriiWorld.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITourClientService _tourService;
        private readonly IAccountClientService _accountService;
        public HomeController(ITourClientService tourService, IAccountClientService accountService)
        {
            _tourService = tourService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {

            var tours = await _tourService.GetAllAsync();
            return View(tours);
        }

        [Route("TourDetails/{id}")]
        public async Task<IActionResult> TourDetails(long id)
        {
            var tour = await _tourService.GetByIdAsync(id);
            return View(tour);
        }

        [HttpGet] public IActionResult Login() => View();
        [HttpGet] public IActionResult Register() => View();

        public IActionResult ContactUs() => View();
        public IActionResult AboutUs() => View();

     
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                await _accountService.RegisterAsync(model);
    
                return RedirectToAction("VerifyEmail", new { email = model.Email });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                string token = await _accountService.LoginAsync(model);

                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var rawPath = jwtToken.Claims.FirstOrDefault(c => c.Type == "Image")?.Value;
                string profilePicture;

                if (!string.IsNullOrEmpty(rawPath))
                {
                  
                    profilePicture = rawPath.Replace("\\", "/").TrimStart('/');
                }
                else
                {
                    profilePicture = "/assets/images/default-avatar.png";
                }

                var claims = jwtToken.Claims.ToList();

            
                claims.Add(new System.Security.Claims.Claim("UserProfilePicture", profilePicture));

           
                var claimsIdentity = new System.Security.Claims.ClaimsIdentity(
                    claims,
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                    System.Security.Claims.ClaimTypes.Name,
                    System.Security.Claims.ClaimTypes.Role
                );

                Response.Cookies.Append("JWTToken", token, new CookieOptions { HttpOnly = true });

                await HttpContext.SignInAsync(
                    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                    new System.Security.Claims.ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("verify"))
                    return RedirectToAction("VerifyEmail", new { email = model.UsernameOrEmail });

                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(string email, string code)
        {
            try
            {
                await _accountService.VerifyCodeAsync(email, code);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }


        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
           
            await HttpContext.SignOutAsync(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewCreateDto model)
        {
            try
            {
               
                var token = Request.Cookies["JWTToken"];

                using var client = new HttpClient();

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                else
                {
                    return Json(new { success = false, message = "Pease Login" });
                }
                var response = await client.PostAsJsonAsync("https://localhost:7029/api/Reviews", model);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"API Error: {errorContent}" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}