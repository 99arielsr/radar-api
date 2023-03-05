using Microsoft.AspNetCore.Mvc;
using projeto_radar_backend.Models;
using radar_api.Repositories.Interfaces;

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
  public IActionResult Index()
  {
    var clients = _service.GetAll();

    return StatusCode(200, clients);
  }

  [HttpGet("{id}")]
  public IActionResult Details([FromRoute] int? id)
  {
    var client = _service.GetAll().Find(c => c.Id == id);
    return StatusCode(200, client);
  }

  [HttpPost("")]
  public IActionResult Create([FromBody] Client client)
  {
    _service.Create(client);
    return StatusCode(201, client);
  }

  [HttpPut("{id}")]
  public IActionResult Update([FromRoute] int id, [FromBody] Client client)
  {
    if (id != client.Id) return StatusCode(400, new {
      Message = "O id do cliente precisa bater com o da URL"
    });

    var clientDB = _service.Update(client);

    return StatusCode(200, clientDB);
  }

  [HttpDelete("{id}")]
  public IActionResult Delete([FromRoute] int id)
  {
    var clientDB = _service.GetAll().Find(c => c.Id == id);
    if (clientDB is null) return StatusCode(404, new {
      Message = "O cliente informado não existe"
    });

    _service.Delete(clientDB);
    return StatusCode(204);
  }
}