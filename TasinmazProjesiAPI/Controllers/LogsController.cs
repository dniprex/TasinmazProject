using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasinmazProjesiAPI.DataAccess;
using TasinmazProjesiAPI.Dtos;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs([FromQuery] string filter)
        {
            var logs = await _context.Logs
                .Where(log => string.IsNullOrEmpty(filter) || log.Aciklama.Contains(filter))
                .Select(log => new
                {
                    log.UserId,
                    log.UserMail,
                    log.Durum,
                    log.IslemTip,
                    log.Aciklama,
                    log.TarihSaat
                }) 
                .ToListAsync();

            return Ok(logs);
        }
        [HttpPost]
        public async Task<IActionResult> AddLog([FromBody] LogDto log)
        {
            Console.WriteLine($"Gelen Log Verisi: {System.Text.Json.JsonSerializer.Serialize(log)}");

            if (log == null)
                return BadRequest(new { Message = "Log verisi boş olamaz." });

            if (log.UserId <= 0)
                return BadRequest(new { Message = "Geçerli bir UserId sağlanmalıdır." });

            if (string.IsNullOrEmpty(log.UserMail))
                return BadRequest(new { Message = "UserMail alanı zorunludur." });

            if (string.IsNullOrEmpty(log.Durum))
                return BadRequest(new { Message = "Durum alanı zorunludur." });

            if (string.IsNullOrEmpty(log.IslemTip))
                return BadRequest(new { Message = "İşlem Tipi alanı zorunludur." });

            if (string.IsNullOrEmpty(log.Aciklama))
                return BadRequest(new { Message = "Açıklama alanı zorunludur." });

            var newLog = new Log
            {
                UserId = log.UserId,
                UserMail = log.UserMail,
                Durum = log.Durum,
                IslemTip = log.IslemTip,
                Aciklama = log.Aciklama,
                TarihSaat = DateTime.Now
            };

            try
            {
                _context.Logs.Add(newLog);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Log kaydı başarıyla eklendi." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"İç Hata: {ex.InnerException.Message}");
                }

                return StatusCode(500, new
                {
                    Message = "Log kaydedilirken hata oluştu.",
                    Detail = ex.Message,
                    InnerException = ex.InnerException?.Message
                });
            }
        }

    }
}
