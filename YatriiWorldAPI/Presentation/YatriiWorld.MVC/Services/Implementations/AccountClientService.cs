using YatriiWorld.Application.DTOs.Tokens;
using YatriiWorld.MVC.Services.Interfaces;
using YatriiWorld.MVC.ViewModels.LoginRegister;
using YatriiWorld.MVC.ViewModels.Register;

public class AccountClientService : IAccountClientService
{
    private readonly HttpClient _httpClient;

    public AccountClientService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("YatriiApiClient");
    }
    public async Task RegisterAsync(RegisterVM model)
    {
        var response = await _httpClient.PostAsJsonAsync("accounts/register", model);
        await HandleErrorAsync(response);
    }

    public async Task VerifyCodeAsync(string email, string code)
    {
        var response = await _httpClient.PostAsJsonAsync("accounts/verifycode", new { email, code });
        await HandleErrorAsync(response);
    }
    public async Task<string> LoginAsync(LoginVM model)
    {
        var response = await _httpClient.PostAsJsonAsync("accounts/login", new
        {
            UserNameOrEmail = model.UsernameOrEmail,
            Password = model.Password
        });

        await HandleErrorAsync(response);

        var result = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
        return result.AccessToken;
    }
    private async Task HandleErrorAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode) return;

      
        var rawError = await response.Content.ReadAsStringAsync();

    
        if (rawError.Contains("verify") || rawError.Contains("confirm"))
        {
            throw new Exception("verify");
        }

      
        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest ||
            response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
       
            throw new Exception("Invalid username, email or password.");
        }

 
        throw new Exception("An unexpected error occurred. Please try again later.");
    }
}