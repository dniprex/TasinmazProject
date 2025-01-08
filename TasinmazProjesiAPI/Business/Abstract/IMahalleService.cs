using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Abstract
{
    public interface IMahalleService
    {
        Task<IEnumerable<Mahalle>> GetAllMahallelerAsync();
        Task<Mahalle> GetMahalleByIdAsync(int id);
        Task<IEnumerable<Mahalle>> GetMahallelerByIlceIdAsync(int ilceId);
        Task AddMahalleAsync(Mahalle mahalle);
        Task UpdateMahalleAsync(Mahalle mahalle);
        Task DeleteMahalleAsync(int id);
    }
}
