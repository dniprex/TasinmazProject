using System.Collections.Generic;
using System.Threading.Tasks;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Business.Abstract
{
    public interface ILogService
    {
        Task<List<Log>> GetLogsAsync(bool? durum);
        Task<Log> AddLogAsync(bool durum, string islemTip, string aciklama, string userMail, int? userId);
    }
}
