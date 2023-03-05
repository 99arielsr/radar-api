using projeto_radar_backend.Models;
using radar_api.Repositories.Interfaces;

namespace radar_api.Repositories;
public class ClientRepository : IService
{

  private static List<Client> list = new List<Client>();

  public List<Client> GetAll()
  {
    return list;
  }

  public void Create(Client client)
  {
    list.Add(client);
  }

  public Client Update(Client client)
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

    return clientDB;
  }

  public void Delete(Client client)
  {
    list.Remove(client);
  }
}