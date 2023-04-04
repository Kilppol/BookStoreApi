using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoberturaController : ControllerBase
{
    private readonly CoberturaService _coberturaService;
    private readonly ILogger<CoberturaController>  _logger;

    public CoberturaController(CoberturaService coberturaService, ILogger<CoberturaController> logger)
    {
        _coberturaService = coberturaService;
        _logger = logger;
    }
        

    [HttpGet]
    public async Task<List<Cobertura>> Get()
    {
        _logger.LogInformation("Get All Coberturas called at: "+DateTime.Now.ToString());
        return await _coberturaService.GetAsync();
    }
        

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Cobertura>> Get(string id)
    {
        var cobertura = await _coberturaService.GetAsync(id);

        if (cobertura is null)
        {
            _logger.LogInformation("Cobertura not found: "+DateTime.Now.ToString());
            return NotFound();
        }
        _logger.LogInformation($"Cobertura{cobertura.IdCob} founded at : "+DateTime.Now.ToString());
        return cobertura;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Cobertura newCobertura)
    {
        await _coberturaService.CreateAsync(newCobertura);
        _logger.LogInformation($"Cobertura Created at: {DateTime.Now.ToString()}");
        return CreatedAtAction(nameof(Get), new { id = newCobertura.IdCob }, newCobertura);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Cobertura updatedCobertura)
    {
        var cobertura = await _coberturaService.GetAsync(id);

        if (cobertura is null)
        {
             _logger.LogInformation("Book not founded on update: "+DateTime.Now.ToString());
            return NotFound();
        }

        updatedCobertura.IdCob = cobertura.IdCob;

        await _coberturaService.UpdateAsync(id, updatedCobertura);
         _logger.LogInformation($"Cobertura Updated {cobertura.IdCob} at: {DateTime.Now.ToString()}");
        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var cobertura = await _coberturaService.GetAsync(id);

        if (cobertura is null)
        {
             _logger.LogInformation("Cobertura not founded on delete: "+DateTime.Now.ToString());
            return NotFound();
        }

        await _coberturaService.RemoveAsync(id);
        _logger.LogInformation($"Book Deleted {cobertura.IdCob} at: {DateTime.Now.ToString()}");
        return NoContent();
    }
}