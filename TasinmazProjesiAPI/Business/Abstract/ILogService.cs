using System.Threading.Tasks;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Abstract
{
    public interface ILogService
    {
        Task AddLog(Log log);
    }
}
