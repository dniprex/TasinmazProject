using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.Business.Concrete;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IllerController : ControllerBase
    {
        private readonly IIlService _ilService;
        
        public IllerController(IIlService ilService)
        {
            _ilService = ilService;
        }
        
        // GET: api/Il
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Il>>> GetAllIller()
        {
            var iller = await _ilService.GetAllIllerAsync();
            return Ok(iller);
        }

        // GET: api/Il/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Il>> GetIlById(int id)
        {
            var il = await _ilService.GetIlByIdAsync(id);
            if (il == null)
            {
                return NotFound($"Il with ID {id} not found.");
            }
            return Ok(il);
        }
    }
}
