namespace projeto_radar_backend.Models;
public record Client
{
	public int Id { get; set; }

	public string Name { get; set; } = default!;

	public string Telephone { get; set; } = default!;

	public string Email { get; set; } = default!;

}