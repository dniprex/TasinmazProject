using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.DataAccess;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Concrete
{
    public class IlceService : IIlceService
    {
        private readonly ApplicationDbContext _context;

        public IlceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ilce>> GetAllIlcelerAsync()
        {
            return await _context.ilceler
                         .Include(ilce => ilce.Il)
                         .ToListAsync();
        }

        public async Task<Ilce> GetIlceByIdAsync(int id)
        {
            return await _context.ilceler.FindAsync(id);
        }

        public async Task<IEnumerable<Ilce>> GetIlcelerByIlIdAsync(int ilId)
        {
            return await _context.ilceler.Where(i => i.IlId == ilId).ToListAsync();
        }

        public async Task AddIlceAsync(Ilce ilce)
        {
            await _context.ilceler.AddAsync(ilce);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIlceAsync(Ilce ilce)
        {
            _context.ilceler.Update(ilce);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIlceAsync(int id)
        {
            var ilce = await _context.ilceler.FindAsync(id);
            if (ilce != null)
            {
                _context.ilceler.Remove(ilce);
                await _context.SaveChangesAsync();
            }
        }
    }


}
