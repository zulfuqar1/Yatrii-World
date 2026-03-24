using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using YatriiWorld.MVC.Exceptions;
using YatriiWorld.MVC.Models;
using YatriiWorld.MVC.Services.Interfaces;
using YatriiWorld.MVC.ViewModels.Tours;

public class TourClientService : ITourClientService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _contextAccessor;

    public TourClientService(IHttpClientFactory factory, IHttpContextAccessor contextAccessor)
    {
        _httpClient = factory.CreateClient("Api");
        _contextAccessor = contextAccessor;
        AttachToken();
    }

    private void AttachToken()
    {
        var token = _contextAccessor.HttpContext?.Request.Cookies["token"];
        if (!string.IsNullOrEmpty(token))
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private async Task HandleErrorAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) return;
        var json = await response.Content.ReadAsStringAsync();
        var errorObj = JsonSerializer.Deserialize<ApiErrorResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var message = errorObj?.Error ?? "Unexpected error";

        throw response.StatusCode switch
        {
            HttpStatusCode.NotFound => new NotFoundException(message),
            HttpStatusCode.Conflict => new AlreadyExistsException(message),
            HttpStatusCode.BadRequest => new BadRequestException(message),
            HttpStatusCode.Forbidden => new ForbiddenException(message),
            _ => new Exception(message)
        };
    }

    private JsonSerializerOptions JsonOptions() => new() { PropertyNameCaseInsensitive = true };

    public async Task<List<TourListVM>> GetAllAsync(int page = 1, int take = 10)
    {
        var response = await _httpClient.GetAsync($"tours?page={page}&take={take}");
        await HandleErrorAsync(response);
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<TourListVM>>(json, JsonOptions())!;
    }

    public async Task<TourDetailsVM> GetByIdAsync(long id)
    {
        var response = await _httpClient.GetAsync($"tours/{id}");
        await HandleErrorAsync(response);
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TourDetailsVM>(json, JsonOptions())!;
    }

    public async Task CreateAsync(TourCreateVM vm)
    {
        var content = new MultipartFormDataContent
        {
            { new StringContent(vm.Name), nameof(vm.Name) },
            { new StringContent(vm.Price.ToString()), nameof(vm.Price) },
            { new StringContent(vm.Description ?? ""), nameof(vm.Description) },
            { new StringContent(vm.CategoryId.ToString()), nameof(vm.CategoryId) }
        };



        var response = await _httpClient.PostAsync("tours", content);
        await HandleErrorAsync(response);
    }

    public async Task UpdateAsync(TourUpdateVM vm)
    {
        var content = new MultipartFormDataContent
        {
            { new StringContent(vm.Id.ToString()), nameof(vm.Id) },
            { new StringContent(vm.Name), nameof(vm.Name) },
            { new StringContent(vm.Price.ToString()), nameof(vm.Price) },
            { new StringContent(vm.Description ?? ""), nameof(vm.Description) },
            { new StringContent(vm.CategoryId.ToString()), nameof(vm.CategoryId) }
        };



        var response = await _httpClient.PutAsync($"tours/{vm.Id}", content);
        await HandleErrorAsync(response);
    }

    public async Task SoftDeleteAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"tours/{id}/soft");
        await HandleErrorAsync(response);
    }

    public async Task RestoreAsync(long id)
    {
        var response = await _httpClient.PatchAsync($"tours/{id}/restore", null);
        await HandleErrorAsync(response);
    }

    public async Task DeleteAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"tours/{id}");
        await HandleErrorAsync(response);
    }
}
