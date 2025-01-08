using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Abstract
{
    public interface ITasinmazService
    {
        Task<IEnumerable<Tasinmaz>> GetAllTasinmazlarAsync();
        Task<Tasinmaz> GetTasinmazByIdAsync(int id);
        Task<IEnumerable<Tasinmaz>> GetTasinmazlarByMahalleIdAsync(int mahalleId);
        Task AddTasinmazAsync(Tasinmaz tasinmaz);
        Task UpdateTasinmazAsync(Tasinmaz tasinmaz);
        Task DeleteTasinmazAsync(int id);
    }
}
