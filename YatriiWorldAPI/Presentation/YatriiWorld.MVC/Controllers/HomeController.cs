using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using System.Diagnostics;
using YatriiWorld.Application.DTOs.Tours;
using YatriiWorld.Domain.Entities;
using YatriiWorld.MVC.ViewModels.Categories;
using YatriiWorld.MVC.ViewModels.Tours;

namespace YatriiWorld.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly RestClient _client;
        public HomeController()
        {
            _client = new RestClient("https://localhost:7029/");
        }

        public async Task<IActionResult> Index(string tagFilter = null)
        {


            // API

            //tours
            RestRequest request = new RestRequest("api/tours", Method.Get);
            var response = await _client.ExecuteAsync<List<TourListVM>>(request);
            if (response.Data == null)
                return View(new List<TourListVM>());
            var tours = response.Data;
            if (!string.IsNullOrEmpty(tagFilter))
            {
                tours = tours
                    .Where(t => t.Tags.Any(tag => tag.Name.Equals(tagFilter, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }
            return View(tours);



        }
        //categories
    }
}
