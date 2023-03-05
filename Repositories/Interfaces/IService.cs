using projeto_radar_backend.Models;

namespace radar_api.Repositories.Interfaces;
public interface IService
{
  List<Client> GetAll();
  void Create(Client client);
  Client Update(Client client);
  void Delete(Client client);
}