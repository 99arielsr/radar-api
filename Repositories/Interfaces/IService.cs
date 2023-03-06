using projeto_radar_backend.Models;

namespace radar_api.Repositories.Interfaces;
public interface IService
{
  Task<List<Client>> GetAllAsync();
  Task CreateAsync(Client client);
  Task<Client> UpdateAsync(Client client);
  Task DeleteAsync(Client client);
}