using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using YatriiWorld.MVC.ViewModels.Tours;

namespace YatriiWorld.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TourTagController : Controller
    {
        private readonly RestClient _client;

        public TourTagController()
        {
            // Kendi portuna göre ayarlı
            _client = new RestClient("https://localhost:7029/");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["jwt"];
            var request = new RestRequest("api/tags", Method.Get);

            if (!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", $"Bearer {token}");

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var tags = JsonSerializer.Deserialize<List<TourTagVM>>(response.Content, options);
                return View(tags ?? new List<TourTagVM>());
            }

            return View(new List<TourTagVM>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TourTagCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var token = Request.Cookies["jwt"];
                var request = new RestRequest("api/tags", Method.Post);

                request.AlwaysMultipartFormData = true;

                if (!string.IsNullOrEmpty(token))
                    request.AddHeader("Authorization", $"Bearer {token}");

                request.AddParameter("Name", model.Name);

                var response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "API Error: " + response.ErrorMessage);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "System Error: " + ex.Message);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            var token = Request.Cookies["jwt"];
            var request = new RestRequest($"api/tags/{id}", Method.Get);

            if (!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", $"Bearer {token}");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var model = JsonSerializer.Deserialize<TourTagVM>(response.Content, options);
                return View(model);
            }

            TempData["Error"] = $"API Hatası (Update Açılamadı): {response.StatusCode} - {response.ErrorMessage}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TourTagVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var token = Request.Cookies["jwt"];
                var request = new RestRequest("api/tags", Method.Put);

                request.AlwaysMultipartFormData = true;

                if (!string.IsNullOrEmpty(token))
                    request.AddHeader("Authorization", $"Bearer {token}");

                request.AddParameter("Id", model.Id);
                request.AddParameter("Name", model.Name);

                var response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Update Error: " + response.ErrorMessage);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "System Error: " + ex.Message);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var token = Request.Cookies["jwt"];
            var request = new RestRequest($"api/tags/{id}", Method.Delete);

            if (!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", $"Bearer {token}");

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                TempData["Error"] = $"Silme işlemi başarısız. Hata: {response.StatusCode}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}