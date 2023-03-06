using MySql.Data.MySqlClient;
using projeto_radar_backend.Models;
using radar_api.Repositories.Interfaces;

namespace radar_api.Repositories;

public class ClientRepositoryMySql : IService
{
  public ClientRepositoryMySql() 
  { 
    connection = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (connection is null) connection = "Server=localhost;Uid=root;Database=dbradar;";
  }

  private string? connection = null;

  public async Task<List<Client>> GetAllAsync()
  {
    var list = new List<Client>();
    using (var conn = new MySqlConnection(connection))
    {
      conn.Open();
      var query = $"select * from clients;";

      var command = new MySqlCommand(query, conn);
      var dataReader = await command.ExecuteReaderAsync();
      while (dataReader.Read())
      {
        list.Add(new Client
        {
          Id = Convert.ToInt32(dataReader["id"]),
          Name = dataReader["name"].ToString() ?? "",
          Telephone = dataReader["telephone"].ToString() ?? "",
          Email = dataReader["email"].ToString() ?? "",
        });
      }

      conn.Close();
    }

    return list;
  }

  public async Task CreateAsync(Client client)
  {
    using(var conn = new MySqlConnection(connection))
    {
      conn.Open();
      var query = $"insert into clients(name,telephone,email)values(@name,@telephone,@email);";
      var command = new MySqlCommand(query, conn);
      command.Parameters.Add(new MySqlParameter("@name", client.Name));
      command.Parameters.Add(new MySqlParameter("@telephone", client.Telephone));
      command.Parameters.Add(new MySqlParameter("@email", client.Email));

      await command.ExecuteNonQueryAsync();

      conn.Close();
    }    
  }

  public async Task<Client> UpdateAsync(Client client)
  {
    using (var conn = new MySqlConnection(connection))
    {
      conn.Open();
      var query = $"update clients set name=@name,telephone=@telephone,email=@email where id = @id;";
      var command = new MySqlCommand(query, conn);
      command.Parameters.Add(new MySqlParameter("@id", client.Id));
      command.Parameters.Add(new MySqlParameter("@name", client.Name));
      command.Parameters.Add(new MySqlParameter("@telephone", client.Telephone));
      command.Parameters.Add(new MySqlParameter("@email", client.Email));

      await command.ExecuteNonQueryAsync();

      conn.Close();
    }

    return client;
  }

  public async Task DeleteAsync(Client client)
  {
    using(var conn = new MySqlConnection(connection))
    {
      conn.Open();
      var query = $"delete from clients where id = @id;";
      var command = new MySqlCommand(query, conn);
      command.Parameters.Add(new MySqlParameter("@id", client.Id));
      await command.ExecuteNonQueryAsync();
      conn.Close();
    }
  }
}

