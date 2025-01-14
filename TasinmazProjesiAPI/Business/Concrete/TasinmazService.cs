using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.DataAccess;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Concrete
{
    public class TasinmazService : ITasinmazService
    {
        private readonly ApplicationDbContext _context;

        public TasinmazService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Tasinmaz>> GetAllTasinmazlarAsync()
        {
            return await _context.tasinmazlar
                .Include(t => t.Mahalle)
                .ThenInclude(m => m.Ilce)
                .ThenInclude(ilce => ilce.Il)
                .ToListAsync();
        }

        public async Task<Tasinmaz> GetTasinmazByIdAsync(int id)
        {
            return await _context.tasinmazlar
                .Include(t => t.Mahalle)
                .ThenInclude(m => m.Ilce)
                .ThenInclude(ilce => ilce.Il)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tasinmaz>> GetTasinmazlarByMahalleIdAsync(int mahalleId)
        {
            return await _context.tasinmazlar
                .Where(t => t.MahalleId == mahalleId)
                .Include(t => t.Mahalle)
                .ThenInclude(m => m.Ilce)
                .ThenInclude(ilce => ilce.Il)
                .ToListAsync();
        }

        public async Task AddTasinmazAsync(Tasinmaz tasinmaz)
        {
            if (tasinmaz == null)
            {
                throw new ArgumentNullException(nameof(tasinmaz), "Eklenecek taşınmaz boş olamaz.");
            }

            await _context.tasinmazlar.AddAsync(tasinmaz);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTasinmazAsync(Tasinmaz tasinmaz)
        {
            if (tasinmaz == null)
            {
                throw new ArgumentNullException(nameof(tasinmaz), "Güncellenecek taşınmaz boş olamaz.");
            }

            var existingTasinmaz = await _context.tasinmazlar.FindAsync(tasinmaz.Id);
            if (existingTasinmaz == null)
            {
                throw new KeyNotFoundException($"Taşınmaz ID {tasinmaz.Id} bulunamadı.");
            }

            // Yalnızca değişen alanları güncelle
            existingTasinmaz.TasinmazIsim = tasinmaz.TasinmazIsim ?? existingTasinmaz.TasinmazIsim;
            existingTasinmaz.TasinmazParsel = tasinmaz.TasinmazParsel != 0 ? tasinmaz.TasinmazParsel : existingTasinmaz.TasinmazParsel;
            existingTasinmaz.TasinmazNitelik = tasinmaz.TasinmazNitelik ?? existingTasinmaz.TasinmazNitelik;
            existingTasinmaz.TasinmazAdres = tasinmaz.TasinmazAdres ?? existingTasinmaz.TasinmazAdres;
            existingTasinmaz.Ada = tasinmaz.Ada ?? existingTasinmaz.Ada;
            existingTasinmaz.KoordinatBilgisi = tasinmaz.KoordinatBilgisi ?? existingTasinmaz.KoordinatBilgisi;
            existingTasinmaz.MahalleId = tasinmaz.MahalleId != 0 ? tasinmaz.MahalleId : existingTasinmaz.MahalleId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTasinmazAsync(int id)
        {
            var tasinmaz = await _context.tasinmazlar.FindAsync(id);
            if (tasinmaz == null)
            {
                throw new KeyNotFoundException($"Taşınmaz ID {id} bulunamadı.");
            }

            _context.tasinmazlar.Remove(tasinmaz);
            await _context.SaveChangesAsync();
        }
    }
}
