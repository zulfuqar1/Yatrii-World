using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using YatriiWorld.MVC.ViewModels.Categories;
using YatriiWorld.MVC.ViewModels.Product;

namespace YatriiWorld.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly RestClient _client;
        private readonly JsonSerializerOptions _options;

        public ProductController()
        {
            _client = new RestClient("https://localhost:7029/");
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        private RestRequest CreateAuthorizedRequest(string resource, Method method, string token)
        {
            var request = new RestRequest(resource, method);
            if (!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", $"Bearer {token}");
            return request;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["jwt"];
            var request = CreateAuthorizedRequest("api/products", Method.Get, token);

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var products = JsonSerializer.Deserialize<List<ProductVM>>(response.Content, _options);
                return View(products ?? new List<ProductVM>());
            }
            return View(new List<ProductVM>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ProductCreateVM();
            var token = Request.Cookies["jwt"];

            var catRes = await _client.ExecuteAsync(CreateAuthorizedRequest("api/categories", Method.Get, token));
            if (catRes.IsSuccessful && !string.IsNullOrEmpty(catRes.Content))
                model.AvailableCategories = JsonSerializer.Deserialize<List<CategoryDetailsVM>>(catRes.Content, _options);

            var tagRes = await _client.ExecuteAsync(CreateAuthorizedRequest("api/producttags", Method.Get, token));
            if (tagRes.IsSuccessful && !string.IsNullOrEmpty(tagRes.Content))
                model.AvailableTags = JsonSerializer.Deserialize<List<ProductTagVM>>(tagRes.Content, _options);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            var token = Request.Cookies["jwt"];

            if (!ModelState.IsValid)
            {
                var catRes = await _client.ExecuteAsync(CreateAuthorizedRequest("api/categories", Method.Get, token));
                if (catRes.IsSuccessful && !string.IsNullOrEmpty(catRes.Content)) model.AvailableCategories = JsonSerializer.Deserialize<List<CategoryDetailsVM>>(catRes.Content, _options);

                var tagRes = await _client.ExecuteAsync(CreateAuthorizedRequest("api/producttags", Method.Get, token));
                if (tagRes.IsSuccessful && !string.IsNullOrEmpty(tagRes.Content)) model.AvailableTags = JsonSerializer.Deserialize<List<ProductTagVM>>(tagRes.Content, _options);

                return View(model);
            }

            var request = CreateAuthorizedRequest("api/products", Method.Post, token);
            request.AlwaysMultipartFormData = true;

            request.AddParameter("Name", model.Name ?? "");
            request.AddParameter("Description", model.Description ?? "");
            request.AddParameter("Price", model.Price.ToString(System.Globalization.CultureInfo.InvariantCulture));
            request.AddParameter("StockQuantity", model.StockQuantity.ToString());
            request.AddParameter("CategoryId", model.CategoryId.ToString());

            if (model.SelectedTagIds != null && model.SelectedTagIds.Any())
            {
                foreach (var tagId in model.SelectedTagIds)
                {
                    request.AddParameter("SelectedTagIds", tagId.ToString());
                }
            }

            if (model.UploadedImages != null && model.UploadedImages.Any())
            {
                foreach (var file in model.UploadedImages)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    request.AddFile("UploadedImages", ms.ToArray(), file.FileName, file.ContentType);
                }
            }

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", $"API Hatası: {response.StatusCode} - {response.Content}");

            var catResRetry = await _client.ExecuteAsync(CreateAuthorizedRequest("api/categories", Method.Get, token));
            if (catResRetry.IsSuccessful && !string.IsNullOrEmpty(catResRetry.Content)) model.AvailableCategories = JsonSerializer.Deserialize<List<CategoryDetailsVM>>(catResRetry.Content, _options);

            var tagResRetry = await _client.ExecuteAsync(CreateAuthorizedRequest("api/producttags", Method.Get, token));
            if (tagResRetry.IsSuccessful && !string.IsNullOrEmpty(tagResRetry.Content)) model.AvailableTags = JsonSerializer.Deserialize<List<ProductTagVM>>(tagResRetry.Content, _options);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            var token = Request.Cookies["jwt"];

            var request = CreateAuthorizedRequest($"api/products/{id}", Method.Get, token);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
                return RedirectToAction(nameof(Index));

            var model = JsonSerializer.Deserialize<ProductUpdateVM>(response.Content, _options);

            var catRes = await _client.ExecuteAsync(CreateAuthorizedRequest("api/categories", Method.Get, token));
            if (catRes.IsSuccessful && !string.IsNullOrEmpty(catRes.Content))
                model.AvailableCategories = JsonSerializer.Deserialize<List<CategoryDetailsVM>>(catRes.Content, _options);

            var tagRes = await _client.ExecuteAsync(CreateAuthorizedRequest("api/producttags", Method.Get, token));
            if (tagRes.IsSuccessful && !string.IsNullOrEmpty(tagRes.Content))
                model.AvailableTags = JsonSerializer.Deserialize<List<ProductTagVM>>(tagRes.Content, _options);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductUpdateVM model)
        {
            var token = Request.Cookies["jwt"];

            if (!ModelState.IsValid)
            {
                var catRes = await _client.ExecuteAsync(CreateAuthorizedRequest("api/categories", Method.Get, token));
                if (catRes.IsSuccessful && !string.IsNullOrEmpty(catRes.Content)) model.AvailableCategories = JsonSerializer.Deserialize<List<CategoryDetailsVM>>(catRes.Content, _options);

                var tagRes = await _client.ExecuteAsync(CreateAuthorizedRequest("api/producttags", Method.Get, token));
                if (tagRes.IsSuccessful && !string.IsNullOrEmpty(tagRes.Content)) model.AvailableTags = JsonSerializer.Deserialize<List<ProductTagVM>>(tagRes.Content, _options);

                return View(model);
            }

            var request = CreateAuthorizedRequest($"api/products/{model.Id}", Method.Put, token);
            request.AlwaysMultipartFormData = true;

            request.AddParameter("Id", model.Id.ToString());
            request.AddParameter("Name", model.Name ?? "");
            request.AddParameter("Description", model.Description ?? "");
            request.AddParameter("Price", model.Price.ToString(System.Globalization.CultureInfo.InvariantCulture));
            request.AddParameter("CategoryId", model.CategoryId.ToString());

            if (model.TagIds != null && model.TagIds.Any())
            {
                foreach (var tagId in model.TagIds)
                {
                    request.AddParameter("TagIds", tagId.ToString());
                }
            }

            if (model.DeletedImageUrls != null && model.DeletedImageUrls.Any())
            {
                foreach (var url in model.DeletedImageUrls)
                {
                    request.AddParameter("DeletedImageUrls", url);
                }
            }

            if (model.UploadedImages != null && model.UploadedImages.Any())
            {
                foreach (var file in model.UploadedImages)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    request.AddFile("UploadedImages", ms.ToArray(), file.FileName, file.ContentType);
                }
            }

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", $"API Hatası: {response.StatusCode} - {response.Content}");
            var catResRetry = await _client.ExecuteAsync(CreateAuthorizedRequest("api/categories", Method.Get, token));
            if (catResRetry.IsSuccessful && !string.IsNullOrEmpty(catResRetry.Content)) model.AvailableCategories = JsonSerializer.Deserialize<List<CategoryDetailsVM>>(catResRetry.Content, _options);

            var tagResRetry = await _client.ExecuteAsync(CreateAuthorizedRequest("api/producttags", Method.Get, token));
            if (tagResRetry.IsSuccessful && !string.IsNullOrEmpty(tagResRetry.Content)) model.AvailableTags = JsonSerializer.Deserialize<List<ProductTagVM>>(tagResRetry.Content, _options);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(long id)
        {
            var token = Request.Cookies["jwt"];

            var request = CreateAuthorizedRequest($"api/products/{id}", Method.Delete, token);
            var response = await _client.ExecuteAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}