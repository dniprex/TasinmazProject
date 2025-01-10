using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasinmazlarController : ControllerBase
    {
        private readonly ITasinmazService _tasinmazService;

        public TasinmazlarController(ITasinmazService tasinmazService)
        {
            _tasinmazService = tasinmazService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasinmaz>>> GetAllTasinmazlar()
        {
            var tasinmazlar = await _tasinmazService.GetAllTasinmazlarAsync();
            return Ok(tasinmazlar);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tasinmaz>> GetTasinmazById(int id)
        {
            var tasinmaz = await _tasinmazService.GetTasinmazByIdAsync(id);
            if (tasinmaz == null) return NotFound($"Tasinmaz with ID {id} not found.");
            return Ok(tasinmaz);
        }

        [HttpGet("by-mahalle/{mahalleId}")]
        public async Task<ActionResult<IEnumerable<Tasinmaz>>> GetTasinmazlarByMahalleId(int mahalleId)
        {
            var tasinmazlar = await _tasinmazService.GetTasinmazlarByMahalleIdAsync(mahalleId);
            return Ok(tasinmazlar);
        }

        [HttpPost]
        public async Task<IActionResult> AddTasinmaz(Tasinmaz tasinmaz)
        {
            await _tasinmazService.AddTasinmazAsync(tasinmaz);
            return CreatedAtAction(nameof(GetTasinmazById), new { id = tasinmaz.Id }, tasinmaz);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasinmaz(int id, Tasinmaz tasinmaz)
        {
            await _tasinmazService.UpdateTasinmazAsync(tasinmaz);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasinmaz(int id)
        {
            await _tasinmazService.DeleteTasinmazAsync(id);
            return NoContent();
        }
    }
}
