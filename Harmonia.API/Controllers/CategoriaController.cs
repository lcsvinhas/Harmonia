using Harmonia.API.DTOs;
using Harmonia.API.Services;
using Microsoft.AspNetCore.Mvc;

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
    }
}
