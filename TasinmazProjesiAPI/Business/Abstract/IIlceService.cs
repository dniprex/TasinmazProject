using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Abstract
{
    public interface IIlceService
    {
        Task<IEnumerable<Ilce>> GetAllIlcelerAsync();
        Task<Ilce> GetIlceByIdAsync(int id);
        Task<IEnumerable<Ilce>> GetIlcelerByIlIdAsync(int ilId);
        Task AddIlceAsync(Ilce ilce);
        Task UpdateIlceAsync(Ilce ilce);
        Task DeleteIlceAsync(int id);

    }
}
