using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using YatriiWorld.MVC.Exceptions;
using YatriiWorld.MVC.Models;
using YatriiWorld.MVC.Services.Interfaces;
using YatriiWorld.MVC.ViewModels.Categories;

namespace YatriiWorld.MVC.Services.Implementations
{
    public class CategoryClientService : ICategoryClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public CategoryClientService(IHttpClientFactory factory, IHttpContextAccessor contextAccessor)
        {
            _httpClient = factory.CreateClient("Api");
            _contextAccessor = contextAccessor;
            _attachToken();
        }

        private void _attachToken()
        {
            var token = _contextAccessor.HttpContext?.Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private async Task _handleErrorAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode) 
                return;
            var json = await response.Content.ReadAsStringAsync();
            var errorObj = JsonSerializer.Deserialize<ApiErrorResponse>(json, _jsonOptions());
            var message = errorObj?.Error ?? "An unexpected error occurred.";
            throw response.StatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundException(message),
                HttpStatusCode.Conflict => new AlreadyExistsException(message),
                HttpStatusCode.BadRequest => new BadRequestException(message),
                HttpStatusCode.Forbidden => new ForbiddenException(message),
                _ => new Exception(message)
            };
        }

        private JsonSerializerOptions _jsonOptions() => new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<CategoryItemVM>> GetAllAsync(int page = 1, int take = 10)
        {
            var response = await _httpClient.GetAsync($"categories?page={page}&take={take}");
            await _handleErrorAsync(response);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CategoryItemVM>>(json, _jsonOptions())!;
        }

        public async Task<List<CategoryItemVM>> GetAllArchivedAsync(int page = 1, int take = 10)
        {
            var response = await _httpClient.GetAsync($"categories/archived?page={page}&take={take}");
            await _handleErrorAsync(response);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<CategoryItemVM>>(json, _jsonOptions())!;
        }

        public async Task<CategoryDetailsVM> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"categories/{id}");
            await _handleErrorAsync(response);
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CategoryDetailsVM>(json, _jsonOptions())!;
        }

        public async Task CreateAsync(CategoryCreateVM vm)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(vm.Name), nameof(vm.Name) }
            };
            var response = await _httpClient.PostAsync("categories", content);
            await _handleErrorAsync(response);
        }

        public async Task UpdateAsync(CategoryUpdateVM vm)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(vm.Name), nameof(vm.Name) }
            };
            var response = await _httpClient.PutAsync($"categories/{vm.Id}", content);
            await _handleErrorAsync(response);
        }

        public async Task DeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");
            await _handleErrorAsync(response);
        }

        public async Task SoftDeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}/soft");
            await _handleErrorAsync(response);
        }

        public async Task RestoreAsync(long id)
        {
            var response = await _httpClient.PatchAsync($"categories/{id}/restore", null);
            await _handleErrorAsync(response);
        }
    }
}
