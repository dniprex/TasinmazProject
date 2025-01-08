using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.DataAccess;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Concrete
{
    public class MahalleService : IMahalleService
    {
        private readonly ApplicationDbContext _context;

        public MahalleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mahalle>> GetAllMahallelerAsync()
        {
            return await _context.mahalleler.Include(mahalle => mahalle.Ilce).ThenInclude(ilce => ilce.Il)
                .ToListAsync();
        }

        public async Task<Mahalle> GetMahalleByIdAsync(int id)
        {
            return await _context.mahalleler.FindAsync(id);
        }

        public async Task<IEnumerable<Mahalle>> GetMahallelerByIlceIdAsync(int ilceId)
        {
            return await _context.mahalleler.Where(m => m.IlceId == ilceId).ToListAsync();
        }

        public async Task AddMahalleAsync(Mahalle mahalle)
        {
            await _context.mahalleler.AddAsync(mahalle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMahalleAsync(Mahalle mahalle)
        {
            _context.mahalleler.Update(mahalle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMahalleAsync(int id)
        {
            var mahalle = await _context.mahalleler.FindAsync(id);
            if (mahalle != null)
            {
                _context.mahalleler.Remove(mahalle);
                await _context.SaveChangesAsync();
            }
        }
    }

}
