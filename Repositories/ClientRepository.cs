using projeto_radar_backend.Models;
using radar_api.Repositories.Interfaces;

namespace radar_api.Repositories;
public class ClientRepository : IService
{

  private static List<Client> list = new List<Client>();

  public async Task<List<Client>> GetAllAsync()
  {
    return await Task.FromResult(list);
  }

  public async Task CreateAsync(Client client)
  {
    list.Add(client);
    await Task.FromResult(new {});
  }

  public async Task<Client> UpdateAsync(Client client)
  {
    if (client.Id == 0) throw new Exception("id não pode ser zero");

    var clientDB = list.Find(c => c.Id == client.Id);
    if (clientDB is null)
    {
      throw new Exception("O cliente informado não existe");
    }

    clientDB.Name= client.Name;
    clientDB.Telephone= client.Telephone;
    clientDB.Email= client.Email;

    return await Task.FromResult(clientDB);
  }

  public async Task DeleteAsync(Client client)
  {
    list.Remove(client);
    await Task.FromResult(new {});
  }
}