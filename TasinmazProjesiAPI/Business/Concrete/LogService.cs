using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.DataAccess;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Concrete
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Logları filtreli veya filtresiz getirir
        public async Task<List<Log>> GetLogsAsync(bool? durum)
        {
            var query = _context.Logs.AsQueryable();

            if (durum.HasValue)
            {
                query = query.Where(l => l.Durum == durum.Value.ToString());
            }

            return await query
                .OrderByDescending(l => l.TarihSaat) // Logları en son tarihliden başlayarak sırala
                .ToListAsync();
        }

        // Yeni bir log kaydı ekler
        public async Task<Log> AddLogAsync(bool durum, string islemTip, string aciklama, string userMail, int? userId)
        {
            var log = new Log
            {
                Durum = durum.ToString(),
                IslemTip = islemTip,
                Aciklama = aciklama,
                UserMail = userMail,
                UserId = userId ?? 0,
                TarihSaat = DateTime.Now
            };

            try
            {
                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();
                return log;
            }
            catch (Exception ex)
            {
                // Log kaydedilirken hata oluşursa hata detayını ayrı bir log olarak kaydet
                await HandleLogErrorAsync(ex, userMail, userId);
                throw; // Hatanın yukarıya taşınmasına izin ver
            }
        }

        // Hata logunu kaydet
        private async Task HandleLogErrorAsync(Exception ex, string userMail, int? userId)
        {
            var errorLog = new Log
            {
                Durum = "false",
                IslemTip = "Error",
                Aciklama = $"Log kaydedilirken hata oluştu: {ex.Message}",
                UserMail = userMail,
                UserId = userId ?? 0,
                TarihSaat = DateTime.Now
            };

            try
            {
                await _context.Logs.AddAsync(errorLog);
                await _context.SaveChangesAsync();
            }
            catch (Exception innerEx)
            {
                Console.WriteLine($"Kritik hata: Hata logu kaydedilemedi. {innerEx.Message}");
            }
        }
    }
}
