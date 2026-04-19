using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Threading.Tasks;
using YatriiWorld.MVC.ViewModels.Categories;
using YatriiWorld.MVC.ViewModels.Product;
using YatriiWorld.MVC.ViewModels.Tours;

namespace YatriiWorld.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaxonomyController : Controller
    {
        private readonly RestClient _client;

        public TaxonomyController()
        {

            _client = new RestClient("https://localhost:7029/");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductTag(ProductTagCreateVM model)
        {
            if (!ModelState.IsValid) return View("Index");

            var request = CreateAuthorizedRequest("api/producttags", Method.Post);
            request.AddJsonBody(model);

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                TempData["Success"] = "Ürün Etiketi başarıyla oluşturuldu!";
            else
                TempData["Error"] = "Ürün Etiketi oluşturulurken hata oluştu.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTourTag(TourTagCreateVM model)
        {
            if (!ModelState.IsValid) return View("Index");

            var request = CreateAuthorizedRequest("api/tags", Method.Post);
            request.AddJsonBody(model);

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
                TempData["Success"] = "Tur Etiketi başarıyla oluşturuldu!";
            else
                TempData["Error"] = "Tur Etiketi oluşturulurken hata oluştu.";

            return RedirectToAction(nameof(Index));
        }

        private RestRequest CreateAuthorizedRequest(string resource, Method method)
        {
            var request = new RestRequest(resource, method);
            var token = Request.Cookies["jwt"];
            if (!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", $"Bearer {token}");
            return request;
        }
    }
}