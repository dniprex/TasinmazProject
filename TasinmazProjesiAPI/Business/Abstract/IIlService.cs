using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Abstract
{
    public interface IIlService
    {
        Task<IEnumerable<Il>> GetAllIllerAsync();
        Task<Il> GetIlByIdAsync(int id);
    }
}
