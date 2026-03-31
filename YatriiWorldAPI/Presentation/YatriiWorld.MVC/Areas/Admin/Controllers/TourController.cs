using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using YatriiWorld.Domain.Entities;
using YatriiWorld.MVC.ViewModels.Categories;
using YatriiWorld.MVC.ViewModels.Tours;

namespace YatriiWorld.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TourController : Controller
    {
        private readonly RestClient _client;

        public TourController()
        {
            _client = new RestClient("https://localhost:7029/");
        }


        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["jwt"];
            var request = new RestRequest("api/tours", Method.Get);
            if (!string.IsNullOrEmpty(token)) request.AddHeader("Authorization", $"Bearer {token}");

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var tours = JsonSerializer.Deserialize<List<TourListVM>>(response.Content, options);
                return View(tours ?? new List<TourListVM>());
            }
            return View(new List<TourListVM>());
        }

    
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TourCreateVM();
            await LoadViewData(model);
            return View(model);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TourCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                await LoadViewData(model);
                return View(model);
            }

            try
            {
                var token = Request.Cookies["jwt"];
                var request = new RestRequest("api/tours", Method.Post);
              
                request.AlwaysMultipartFormData = true;

                if (!string.IsNullOrEmpty(token)) request.AddHeader("Authorization", $"Bearer {token}");

           
                TimeSpan dateDiff = model.EndDate - model.StartDate;
                int durationDays = dateDiff.Days;
             
                if (durationDays < 0)
                {
                    ModelState.AddModelError("", "End Date cannot be earlier than Start Date!");
                    await LoadViewData(model);
                    return View(model);
                }
             

                request.AddParameter("Name", model.Name);
                request.AddParameter("Title", model.Title);
                request.AddParameter("Description", model.Description);
                request.AddParameter("Price", model.Price);
                request.AddParameter("Capacity", model.Capacity);
                request.AddParameter("Country", model.Country);
                request.AddParameter("City", model.City);
                request.AddParameter("StartDate", model.StartDate.ToString("yyyy-MM-dd"));
                request.AddParameter("EndDate", model.EndDate.ToString("yyyy-MM-dd"));
                request.AddParameter("DurationInDays", durationDays);
                request.AddParameter("CategoryId", model.CategoryId);
                request.AddParameter("AmaizingPlacesCount", model.AmaizingPlacesCount);
                request.AddParameter("TransportType", (int)model.TransportType);

                if (model.SelectedTagIds != null)
                {
                    foreach (var tagId in model.SelectedTagIds)
                    {
                        request.AddParameter("SelectedTagIds", tagId);
                    }
                }

                if (model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (var photo in model.Photos)
                    {
                       
                        using var ms = new MemoryStream();
                        await photo.CopyToAsync(ms);
                        byte[] fileBytes = ms.ToArray();

                      
                        request.AddFile("Photos", fileBytes, photo.FileName, photo.ContentType);
                    }
                }

                var response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "API Error: " + response.ErrorMessage);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "System Error: " + ex.Message);
            }

            await LoadViewData(model);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var token = Request.Cookies["jwt"];
            var request = new RestRequest($"api/tours/{id}", Method.Delete);

            if (!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", $"Bearer {token}");

            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        private async Task LoadViewData(TourCreateVM model)
        {
            var token = Request.Cookies["jwt"];

         
            var catReq = new RestRequest("api/categories", Method.Get);
            if (!string.IsNullOrEmpty(token)) catReq.AddHeader("Authorization", $"Bearer {token}");
            var catRes = await _client.ExecuteAsync<List<CategoryDetailsVM>>(catReq);

         
            var tagReq = new RestRequest("api/tags", Method.Get);
            if (!string.IsNullOrEmpty(token)) tagReq.AddHeader("Authorization", $"Bearer {token}");
            var tagRes = await _client.ExecuteAsync<List<TourTagVM>>(tagReq);

            model.AvailableCategories = catRes.Data ?? new List<CategoryDetailsVM>();
            model.AvailableTags = tagRes.Data ?? new List<TourTagVM>();
        }






        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            var token = Request.Cookies["jwt"];
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };




            var request = new RestRequest($"api/tours/{id}", Method.Get);
            if (!string.IsNullOrEmpty(token)) request.AddHeader("Authorization", $"Bearer {token}");

            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
            {
                return RedirectToAction(nameof(Index));
            }

            var model = JsonSerializer.Deserialize<TourUpdateVM>(response.Content, options);

            if (model != null)
            {
               
                if (model.Tags != null && model.Tags.Any())
                {
                    model.SelectedTagIds = model.Tags.Select(x => x.Id).ToList();
                }
                else
                {
                    model.SelectedTagIds = new List<long>();
                }

                await LoadViewDataForUpdate(model);

                return View("Update", model);
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TourUpdateVM model)
        {
           
            if (!ModelState.IsValid)
            {
                await LoadViewDataForUpdate(model);
                return View(model);
            }

            try
            {
                var token = Request.Cookies["jwt"];
                var request = new RestRequest("api/tours", Method.Put);
                request.AlwaysMultipartFormData = true;

                if (!string.IsNullOrEmpty(token)) request.AddHeader("Authorization", $"Bearer {token}");

       
                request.AddParameter("Id", model.Id);
                request.AddParameter("Name", model.Name);
                request.AddParameter("Title", model.Title);
                request.AddParameter("Description", model.Description);
                request.AddParameter("Price", model.Price.ToString(System.Globalization.CultureInfo.InvariantCulture));
                request.AddParameter("Capacity", model.Capacity);
                request.AddParameter("Country", model.Country);
                request.AddParameter("City", model.City);
                request.AddParameter("StartDate", model.StartDate.ToString("yyyy-MM-dd"));
                request.AddParameter("EndDate", model.EndDate.ToString("yyyy-MM-dd"));
                request.AddParameter("CategoryId", model.CategoryId);
                request.AddParameter("AmaizingPlacesCount", model.AmaizingPlacesCount);
                request.AddParameter("TransportType", (int)model.TransportType);


                if (model.SelectedTagIds != null)
                {
                    foreach (var tagId in model.SelectedTagIds)
                        request.AddParameter("SelectedTagIds", tagId);
                }

                if (model.Photos != null)
                {
                    foreach (var photo in model.Photos)
                    {
                        using var ms = new MemoryStream();
                        await photo.CopyToAsync(ms);
                        request.AddFile("Photos", ms.ToArray(), photo.FileName, photo.ContentType);
                    }
                }

                var response = await _client.ExecuteAsync(request);

                if (response.IsSuccessful)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("", "Update Error " + response.ErrorMessage);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error " + ex.Message);
            }

            await LoadViewDataForUpdate(model);
            return View(model);
        }

        private async Task LoadViewDataForUpdate(TourUpdateVM model)
        {
            var token = Request.Cookies["jwt"];
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var catReq = new RestRequest("api/categories", Method.Get);
            if (!string.IsNullOrEmpty(token)) catReq.AddHeader("Authorization", $"Bearer {token}");
            var catRes = await _client.ExecuteAsync(catReq);
            if (catRes.IsSuccessful && !string.IsNullOrEmpty(catRes.Content))
                model.AvailableCategories = JsonSerializer.Deserialize<List<CategoryDetailsVM>>(catRes.Content, options);
            var tagReq = new RestRequest("api/tags", Method.Get);
            if (!string.IsNullOrEmpty(token)) tagReq.AddHeader("Authorization", $"Bearer {token}");
            var tagRes = await _client.ExecuteAsync(tagReq);
            if (tagRes.IsSuccessful && !string.IsNullOrEmpty(tagRes.Content))
                model.Tags = JsonSerializer.Deserialize<List<TourTagVM>>(tagRes.Content, options);

            model.AvailableCategories ??= new List<CategoryDetailsVM>();
            model.Tags ??= new List<TourTagVM>();
        }



    }
}