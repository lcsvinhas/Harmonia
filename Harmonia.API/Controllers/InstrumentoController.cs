using Asp.Versioning;
using Harmonia.API.DTOs;
using Harmonia.API.Paginations;
using Harmonia.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using X.PagedList;

namespace Harmonia.API.Controllers;

[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
public class InstrumentoController : ControllerBase
{
    private readonly IInstrumentoService _service;

    public InstrumentoController(IInstrumentoService service)
    {
        _service = service;
    }

    /// <summary>
    /// Filtra instrumentos por página.
    /// </summary>
    [HttpGet("Paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetPagedAsync([FromQuery] InstrumentoParameters param)
    {
        var instrumentosPaginados = await _service.GetPagedAsync(param);
        AddPaginationHeader(instrumentosPaginados);
        return Ok(instrumentosPaginados);
    }

    /// <summary>
    /// Filtra instrumentos por nome, marca, modelo ou cor com paginação.
    /// </summary>
    [HttpGet("Filtro")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetByFilterPagedAsync([FromQuery] InstrumentoFiltro filtro)
    {
        var instrumentosPaginados = await _service.GetByFilterPagedAsync(filtro);
        AddPaginationHeader(instrumentosPaginados);
        return Ok(instrumentosPaginados);
    }

    /// <summary>
    /// Obtém todos os instrumentos.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetAllAsync() => Ok(await _service.GetAllAsync());

    /// <summary>
    /// Obtém um instrumento pelo ID.
    /// </summary>
    /// <param name="id">ID do instrumento que deve retornar.</param>
    /// <returns></returns>
    [HttpGet("{id:int:min(1)}", Name = "InstrumentoPorId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _service.GetByIdAsync(id));

    /// <summary>
    /// Cria um novo instrumento. 
    /// </summary>
    [Authorize(Policy = "UserOnly")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateAsync(InstrumentoRequestDTO request)
    {
        var instrumento = await _service.CreateAsync(request);
        return CreatedAtRoute("InstrumentoPorId", new { id = instrumento.InstrumentoId }, instrumento);
    }

    /// <summary>
    /// Atualiza um instrumento existente.
    /// </summary>
    /// <param name="id">ID do instrumento que será atualizado.</param>
    [Authorize(Policy = "UserOnly")]
    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateAsync(int id, InstrumentoRequestDTO request) => Ok(await _service.UpdateAsync(id, request));

    /// <summary>
    /// Exclui um instrumento pelo ID.
    /// </summary>
    /// <param name="id">ID do instrumento que será excluído.</param>
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
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
