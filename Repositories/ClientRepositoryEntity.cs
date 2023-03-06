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

	private string? connection = null;

	public List<Client> GetAll()
	{
		return context.Clients.ToList();
	}

	public void Create(Client client)
	{
		context.Clients.Add(client);
		context.SaveChanges();
	}

	public Client Update(Client client)
	{
		context.Entry(client).State = EntityState.Modified;
		context.SaveChanges();

		return client;
	}

	public void Delete(Client client)
	{
		var obj = context.Clients.Find(client.Id);

		if (obj is null) throw new Exception("Cliente não encontrado!");

		context.Clients.Remove(obj);
		context.SaveChanges();
	}
}

