using Microsoft.AspNetCore.Mvc;
using Roc.Build;
using Roc.Const;
using Roc.Models;
using Roc.utils;
using Single.Entities.Entity;
using Single.Services.IService;

namespace Single.Controllers.Machine;

/// <summary>
/// 示例代码
/// </summary>
[Route("[controller]/[action]")]
public class MachineController:RocController
{
    private readonly IMachineService machineService;

    public MachineController(IMachineService machineService)
    {
        this.machineService = machineService;
    }

    /// <summary>
    /// excel 模板下载
    /// </summary>
    /// <param name="fileId">文件名称</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{fileId}")]
    public IActionResult DownloadTemplate(string fileId)
    {
        var path = PathUtil.GetAbsolutePath("Template", fileId);
        var contentType = FileUtil.GetContentType(fileId);
        return PhysicalFile(path, contentType, fileId);
    }

    [HttpPost]
    [ActionName("upExcel")]
    public async Task<RocResult> UpExcel([FromForm] RocFileInfo fileInfo)
    {
        var result = await machineService.upExcel(fileInfo);
        if (result.Code == RocCode.Success)
        {
            return Success("excel导入成功");
        }
        else
        {
            return Error(result.Message);
        }
    }

    [HttpPost]
    [ActionName("getPageList")]
    public async Task<RocResult<RocPage<IEnumerable<MachineInfo>>>> GetPageList(RocPage<MachineInfo> info)
    {
        return Success(await machineService.getPageList(info));
    }

    [HttpPost]
    [ActionName("Modify")]
    public async Task<RocResult> Modify(MachineInfo info)
    {
        var code = await machineService.Modify(info);
        if (code == RocCode.Success)
        {
            return Success("修改成功");
        }
        else
        {
            return Error(code);
        }
    }

}
