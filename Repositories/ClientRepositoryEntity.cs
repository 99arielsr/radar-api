using Microsoft.EntityFrameworkCore;
using projeto_radar_backend.Models;
using radar_api.Repositories.Interfaces;

namespace radar_api.Repositories;

public class ClientContext : DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var conexao = Environment.GetEnvironmentVariable("DATABASE_URL");
		if (conexao is null) conexao = "Server=localhost;Database=dbradar;Uid=root;";
		optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
	}
	public DbSet<Client> Clients { get; set; } = default!;
}

public class ClientRepositoryEntity : IService
{
	private ClientContext context;
	public ClientRepositoryEntity() 
	{ 
		context = new ClientContext();
	}

	public async Task<List<Client>> GetAllAsync()
	{
		return await context.Clients.ToListAsync();
	}

	public async Task CreateAsync(Client client)
	{
		context.Clients.Add(client);
		await context.SaveChangesAsync();
	}

	public async Task<Client> UpdateAsync(Client client)
	{
		context.Entry(client).State = EntityState.Modified;
		await context.SaveChangesAsync();

		return client;
	}

	public async Task DeleteAsync(Client client)
	{
		var obj = await context.Clients.FindAsync(client.Id);
		if (obj is null) throw new Exception("Cliente não encontrado!");
		context.Clients.Remove(obj);
		await context.SaveChangesAsync();
	}
}

