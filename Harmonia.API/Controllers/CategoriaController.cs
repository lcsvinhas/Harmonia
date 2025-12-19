using Harmonia.API.DTOs;
using Harmonia.API.Paginations;
using Harmonia.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using X.PagedList;

namespace Harmonia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpGet("Instrumentos")]
        public async Task<IActionResult> GetAllWithProductsAsync() => Ok(await _service.GetAllWithProductsAsync());

        [HttpGet("Paginacao")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] CategoriaParameters categoriaParameters)
        {
            var categoriasPaginadas = await _service.GetPagedAsync(categoriaParameters);
            AddPaginationHeader(categoriasPaginadas);
            return Ok(categoriasPaginadas);
        }

        [HttpGet("Filtro/Nome")]
        public async Task<IActionResult> GetByNamePagedAsync([FromQuery] CategoriaFiltroNome categoriaFiltroNome)
        {
            var categoriasPaginadas = await _service.GetByNamePagedAsync(categoriaFiltroNome);
            AddPaginationHeader(categoriasPaginadas);
            return Ok(categoriasPaginadas);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() => Ok(await _service.GetAllAsync());

        [HttpGet("{id:int:min(1)}", Name = "CategoriaPorId")]
        public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoriaRequestDTO request)
        {
            var categoria = await _service.CreateAsync(request);
            return CreatedAtRoute("CategoriaPorId", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> UpdateAsync(int id, CategoriaRequestDTO request) => Ok(await _service.UpdateAsync(id, request));

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
}
