using System.Text.Json.Serialization;

namespace radar_api.DTOs
{
  public record ClientDTO
  {
    [JsonPropertyName("nome")]
    public string Name { get; set; } = default!;
    [JsonPropertyName("telefone")]
    public string Telephone { get; set; } = default!;
    public string Email { get; set; } = default!;
  }
}
