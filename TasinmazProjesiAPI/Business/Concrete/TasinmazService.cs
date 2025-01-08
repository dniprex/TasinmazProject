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
            _context = context;
        }

        public async Task<IEnumerable<Tasinmaz>> GetAllTasinmazlarAsync()
        {
            return await _context.tasinmazlar.Include(tasinmaz => tasinmaz.Mahalle).ThenInclude(mahalle => mahalle.Ilce).ThenInclude(ilce => ilce.Il)
                .ToListAsync();
        }

        public async Task<Tasinmaz> GetTasinmazByIdAsync(int id)
        {
            return await _context.tasinmazlar.FindAsync(id);
        }

        public async Task<IEnumerable<Tasinmaz>> GetTasinmazlarByMahalleIdAsync(int mahalleId)
        {
            return await _context.tasinmazlar.Where(t => t.MahalleId == mahalleId).ToListAsync();
        }

        public async Task AddTasinmazAsync(Tasinmaz tasinmaz)
        {
            await _context.tasinmazlar.AddAsync(tasinmaz);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTasinmazAsync(Tasinmaz tasinmaz)
        {
            _context.tasinmazlar.Update(tasinmaz);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTasinmazAsync(int id)
        {
            var tasinmaz = await _context.tasinmazlar.FindAsync(id);
            if (tasinmaz != null)
            {
                _context.tasinmazlar.Remove(tasinmaz);
                await _context.SaveChangesAsync();
            }
        }
    }

}
