using Harmonia.API.DTOs;
using Harmonia.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Harmonia.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstrumentoController : ControllerBase
{
    private readonly IInstrumentoService _service;

    public InstrumentoController(IInstrumentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int:min(1)}", Name = "GetByIdAsync")]
    public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> CreateAsync(InstrumentoRequestDTO request)
    {
        var instrumento = await _service.CreateAsync(request);
        return CreatedAtRoute("GetByIdAsync", new { id = instrumento.InstrumentoId }, instrumento);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<IActionResult> UpdateAsync(int id, InstrumentoRequestDTO request) => Ok(await _service.UpdateAsync(id, request));

    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
