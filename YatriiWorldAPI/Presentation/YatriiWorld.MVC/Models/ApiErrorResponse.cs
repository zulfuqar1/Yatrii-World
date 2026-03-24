using System.Text.Json.Serialization;

namespace YatriiWorld.MVC.Models
{
    public class ApiErrorResponse
    {
        [JsonPropertyName("error")]
        public string? Error { get; set; }
    }
}
