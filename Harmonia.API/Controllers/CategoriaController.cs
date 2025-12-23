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
[Route("api/v{version:ApiVersion}/[controller]")]
[ApiController]
[Produces("application/json")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _service;

    public CategoriaController(ICategoriaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Obtém todas as categorias com seus respectivos instrumentos.
    /// </summary>
    [HttpGet("Instrumentos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetAllWithProductsAsync() => Ok(await _service.GetAllWithProductsAsync());

    /// <summary>
    /// Filtra categorias por página.
    /// </summary>
    [HttpGet("Paginacao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetPagedAsync([FromQuery] CategoriaParameters param)
    {
        var categoriasPaginadas = await _service.GetPagedAsync(param);
        AddPaginationHeader(categoriasPaginadas);
        return Ok(categoriasPaginadas);
    }

    /// <summary>
    /// Filtra categorias por nome com paginação.
    /// </summary>
    [HttpGet("Filtro")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetByFilterPagedAsync([FromQuery] CategoriaFiltro filtro)
    {
        var categoriasPaginadas = await _service.GetByFilterPagedAsync(filtro);
        AddPaginationHeader(categoriasPaginadas);
        return Ok(categoriasPaginadas);
    }

    /// <summary>
    /// Obtém todas as categorias.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetAllAsync() => Ok(await _service.GetAllAsync());

    /// <summary>
    /// Obtém uma categoria pelo ID.
    /// </summary>
    /// <param name="id">ID da categoria que deve retornar.</param>
    [HttpGet("{id:int:min(1)}", Name = "CategoriaPorId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _service.GetByIdAsync(id));

    /// <summary>
    /// Cria uma nova categoria.
    /// </summary>
    [Authorize(Policy = "UserOnly")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateAsync(CategoriaRequestDTO request)
    {
        var categoria = await _service.CreateAsync(request);
        return CreatedAtRoute("CategoriaPorId", new { id = categoria.CategoriaId }, categoria);
    }

    /// <summary>
    /// Atualiza uma categoria existente.
    /// </summary>
    /// <param name="id">ID da categoria que será atualizada.</param>
    [Authorize(Policy = "UserOnly")]
    [HttpPut("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateAsync(int id, CategoriaRequestDTO request) => Ok(await _service.UpdateAsync(id, request));

    /// <summary>
    /// Exclui uma categoria pelo ID.
    /// </summary>
    /// <param name="id">ID da categoria que será excluída.</param>
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
