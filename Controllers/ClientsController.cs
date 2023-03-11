using Microsoft.AspNetCore.Mvc;
using projeto_radar_backend.Models;
using radar_api.DTOs;
using radar_api.Repositories.Interfaces;
using radar_api.Services;

namespace radar_api.Controllers;

[Route("clients")]
public class ClientsController : ControllerBase
{
  private IService _service;
  public ClientsController(IService service)
  {
    _service = service;
  }

  [HttpGet("")]
  public async Task<IActionResult> Index()
  {
    var clients = await _service.GetAllAsync();
    return StatusCode(200, clients);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> Details([FromRoute] int? id)
  {
    var client = (await _service.GetAllAsync()).Find(c => c.Id == id);
    return StatusCode(200, client);
  }

  [HttpPost("")]
  public async Task<IActionResult> Create([FromBody] ClientDTO clientDTO)
  {
    var client = BuilderService<Client>.Builder(clientDTO);
    await _service.CreateAsync(client);
    return StatusCode(201, client);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Client client)
  {
    if (id != client.Id) return StatusCode(400, new {
      Message = "O id do cliente precisa bater com o da URL"
    });

    var clientDB = await _service.UpdateAsync(client);

    return StatusCode(200, clientDB);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete([FromRoute] int id)
  {
    var clientDB = (await _service.GetAllAsync()).Find(c => c.Id == id);
    if (clientDB is null) return StatusCode(404, new {
      Message = "O cliente informado não existe"
    });

    await _service.DeleteAsync(clientDB);
    return StatusCode(204);
  }
}