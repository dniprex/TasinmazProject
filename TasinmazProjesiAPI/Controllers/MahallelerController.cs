using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahallelerController : ControllerBase
    {
        private readonly IMahalleService _mahalleService;

        public MahallelerController(IMahalleService mahalleService)
        {
            _mahalleService = mahalleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mahalle>>> GetAllMahalleler()
        {
            var mahalleler = await _mahalleService.GetAllMahallelerAsync();
            return Ok(mahalleler);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mahalle>> GetMahalleById(int id)
        {
            var mahalle = await _mahalleService.GetMahalleByIdAsync(id);
            if (mahalle == null) return NotFound($"Mahalle with ID {id} not found.");
            return Ok(mahalle);
        }

        [HttpGet("by-ilce/{ilceId}")]
        public async Task<ActionResult<IEnumerable<Mahalle>>> GetMahallelerByIlceId(int ilceId)
        {
            var mahalleler = await _mahalleService.GetMahallelerByIlceIdAsync(ilceId);
            return Ok(mahalleler);
        }

        [HttpPost]
        public async Task<IActionResult> AddMahalle(Mahalle mahalle)
        {
            await _mahalleService.AddMahalleAsync(mahalle);
            return CreatedAtAction(nameof(GetMahalleById), new { id = mahalle.Id }, mahalle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMahalle(int id, Mahalle mahalle)
        {
            if (id != mahalle.Id) return BadRequest("ID mismatch.");
            await _mahalleService.UpdateMahalleAsync(mahalle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMahalle(int id)
        {
            await _mahalleService.DeleteMahalleAsync(id);
            return NoContent();
        }
    }
}
