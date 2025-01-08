using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.DataAccess;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Concrete
{
    public class IlService : IIlService
    {
        private readonly ApplicationDbContext _context;

        public IlService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Il>> GetAllIllerAsync()
        {
            return await _context.iller.ToListAsync();
        }

        public async Task<Il> GetIlByIdAsync(int id)
        {
            return await _context.iller.FindAsync(id);
        }
    }
}
