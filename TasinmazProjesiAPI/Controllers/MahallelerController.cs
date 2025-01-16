using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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
            if (mahalleler == null || !mahalleler.Any())
                return NotFound("No neighborhoods found.");
            return Ok(mahalleler);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mahalle>> GetMahalleById(int id)
        {
            var mahalle = await _mahalleService.GetMahalleByIdAsync(id);
            if (mahalle == null)
                return NotFound($"Neighborhood with ID {id} not found.");
            return Ok(mahalle);
        }

        [HttpGet("by-ilce/{ilceId?}")]
        public async Task<ActionResult<IEnumerable<Mahalle>>> GetMahallelerByIlceId(int? ilceId)
        {
            if (!ilceId.HasValue)
                return BadRequest(new { Message = "District ID is required." });

            var mahalleler = await _mahalleService.GetMahallelerByIlceIdAsync(ilceId.Value);
            if (mahalleler == null || !mahalleler.Any())
                return NotFound($"No neighborhoods found for district ID {ilceId}.");

            return Ok(mahalleler);
        }


        [HttpPost]
        public async Task<IActionResult> AddMahalle(Mahalle mahalle)
        {
            if (mahalle == null)
                return BadRequest("Neighborhood data cannot be null.");
            await _mahalleService.AddMahalleAsync(mahalle);
            return CreatedAtAction(nameof(GetMahalleById), new { id = mahalle.Id }, mahalle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMahalle(int id, Mahalle mahalle)
        {
            if (mahalle == null)
                return BadRequest("Neighborhood data cannot be null.");
            if (id != mahalle.Id)
                return BadRequest("ID mismatch.");
            var existingMahalle = await _mahalleService.GetMahalleByIdAsync(id);
            if (existingMahalle == null)
                return NotFound($"Neighborhood with ID {id} not found.");
            await _mahalleService.UpdateMahalleAsync(mahalle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMahalle(int id)
        {
            var existingMahalle = await _mahalleService.GetMahalleByIdAsync(id);
            if (existingMahalle == null)
                return NotFound($"Neighborhood with ID {id} not found.");
            await _mahalleService.DeleteMahalleAsync(id);
            return NoContent();
        }
    }
}
