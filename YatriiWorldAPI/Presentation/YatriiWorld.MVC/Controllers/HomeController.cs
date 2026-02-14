using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using YatriiWorld.MVC.ViewModels;

namespace YatriiWorld.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly RestClient _client;
        public HomeController()
        {
            _client = new RestClient("https://localhost:7029/");
        }
        public async Task<IActionResult> Index()
        {
            RestRequest request = new RestRequest("categories", Method.Get);

            var response = await _client.ExecuteAsync<List<GetCategoryItemVM>>(request);

          

            return View(response.Data);
        }
    }
}
