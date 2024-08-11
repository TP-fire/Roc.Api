using Microsoft.AspNetCore.Mvc;
using Roc.Build;
using Roc.Const;
using Roc.Models;
using Roc.utils;
using Single.Entities.Entity;
using Single.Services.IService;

namespace Single.Controllers.FIle;

[Route("[controller]/[action]")]
public class FileManageController :RocController
{
    private readonly IFileManageService fileManageService;

    public FileManageController(IFileManageService fileManageService)
    {
        this.fileManageService = fileManageService;
    }

    [HttpPost]
    [ActionName("getPageList")]
    public async Task<RocResult<RocPage<IEnumerable<AlarmReportLogs>>>> GetPageList(RocPage<AlarmReportLogs> info)
    {
        return Success(await fileManageService.getPageList(info));
    }
}
