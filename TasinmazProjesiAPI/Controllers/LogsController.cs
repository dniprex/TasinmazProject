using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasinmazProjesiAPI.Dtos;

namespace TasinmazProjesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController //: ControllerBase
    {
        //private readonly ILogService _logService;

//public LogController(ILogService logService)
//{
//    _logService = logService;
//}

//[HttpGet]
//public async Task<IActionResult> GetLogs([FromQuery] LogFilterDto filterDto)
//{
//    var logs = await _logService.GetFilteredLogsAsync(filterDto);
//    return Ok(logs);
//}

//[HttpGet("export")]
//public async Task<IActionResult> ExportLogs([FromQuery] LogFilterDto filterDto)
//{
//    var fileContent = await _logService.ExportLogsToExcel(filterDto);
//    return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Logs.xlsx");
//}

/*AuthService
Kullanıcı giriş, şifreleme ve JWT oluşturma işlemleri bu servis ile yapılır.

TasinmazService
Taşınmazların CRUD işlemlerini gerçekleştirir.

LogService
Log kayıtlarını tutar, filtreleme ve Excel'e aktarım sağlar.*/
}
}
