using Harmonia.API.DTOs;
using Harmonia.API.Paginations;
using Harmonia.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using X.PagedList;

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

    [HttpGet("Paginacao")]
    public async Task<IActionResult> GetPagedAsync([FromQuery] InstrumentoParameters param)
    {
        var instrumentosPaginados = await _service.GetPagedAsync(param);
        AddPaginationHeader(instrumentosPaginados);
        return Ok(instrumentosPaginados);
    }

    [HttpGet("Filtro")]
    public async Task<IActionResult> GetByFilterPagedAsync([FromQuery] InstrumentoFiltro filtro)
    {
        var instrumentosPaginados = await _service.GetByFilterPagedAsync(filtro);
        AddPaginationHeader(instrumentosPaginados);
        return Ok(instrumentosPaginados);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync() => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int:min(1)}", Name = "InstrumentoPorId")]
    public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> CreateAsync(InstrumentoRequestDTO request)
    {
        var instrumento = await _service.CreateAsync(request);
        return CreatedAtRoute("InstrumentoPorId", new { id = instrumento.InstrumentoId }, instrumento);
    }

    [HttpPut("{id:int:min(1)}")]
    public async Task<IActionResult> UpdateAsync(int id, InstrumentoRequestDTO request) => Ok(await _service.UpdateAsync(id, request));

    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    private void AddPaginationHeader(IPagedList pagedList)
    {
        var metadata = new
        {
            pagedList.PageNumber,
            pagedList.PageSize,
            pagedList.PageCount,
            pagedList.TotalItemCount,
            pagedList.HasNextPage,
            pagedList.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));
    }
}
