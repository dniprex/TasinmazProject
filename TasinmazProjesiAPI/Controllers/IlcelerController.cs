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
    public class IlcelerController : ControllerBase
    {
        private readonly IIlceService _ilceService;

        public IlcelerController(IIlceService ilceService)
        {
            _ilceService = ilceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ilce>>> GetAllIlceler()
        {
            var ilceler = await _ilceService.GetAllIlcelerAsync();
            if (ilceler == null || !ilceler.Any())
                return NotFound("No districts found.");
            return Ok(ilceler);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ilce>> GetIlceById(int id)
        {
            var ilce = await _ilceService.GetIlceByIdAsync(id);
            if (ilce == null)
                return NotFound($"District with ID {id} not found.");
            return Ok(ilce);
        }

        [HttpGet("by-il/{ilId?}")]
        public async Task<ActionResult<IEnumerable<Ilce>>> GetIlcelerByIlId(int? ilId)
        {
            if (!ilId.HasValue)
                return BadRequest(new { Message = "City ID is required." });

            var ilceler = await _ilceService.GetIlcelerByIlIdAsync(ilId.Value);
            if (ilceler == null || !ilceler.Any())
                return NotFound($"No districts found for city ID {ilId}.");

            return Ok(ilceler);
        }


        [HttpPost]
        public async Task<IActionResult> AddIlce(Ilce ilce)
        {
            if (ilce == null)
                return BadRequest("District data cannot be null.");
            await _ilceService.AddIlceAsync(ilce);
            return CreatedAtAction(nameof(GetIlceById), new { id = ilce.Id }, ilce);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIlce(int id, Ilce ilce)
        {
            if (ilce == null)
                return BadRequest("District data cannot be null.");
            if (id != ilce.Id)
                return BadRequest("ID mismatch.");
            var existingIlce = await _ilceService.GetIlceByIdAsync(id);
            if (existingIlce == null)
                return NotFound($"District with ID {id} not found.");
            await _ilceService.UpdateIlceAsync(ilce);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIlce(int id)
        {
            var existingIlce = await _ilceService.GetIlceByIdAsync(id);
            if (existingIlce == null)
                return NotFound($"District with ID {id} not found.");
            await _ilceService.DeleteIlceAsync(id);
            return NoContent();
        }
    }
}
