using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasinmazProjesiAPI.DataAccess;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Concrete
{
    public class LogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Log>> GetLogsAsync(bool? durum)
        {
            var query = _context.Logs.AsQueryable();

            if (durum.HasValue)
            {
                query = query.Where(l => l.Durum == durum.Value.ToString());
            }

            return await query
                .OrderByDescending(l => l.TarihSaat) 
                .ToListAsync();
        }

        public async Task<Log> AddLogAsync(bool durum, string islemTip, string aciklama, string userMail, int? userId)
        {
            try
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

                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();
                return log;
            }
            catch (Exception ex)
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
                catch
                {
                    Console.WriteLine($"Log kaydedilirken kritik hata oluştu: {ex.Message}");
                }

                throw; //hata fırlatıyor ?
            }
        }
    }
}
