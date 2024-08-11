using Microsoft.AspNetCore.Mvc;
using Roc.Build;
using Roc.Const;
using Roc.Models;
using Roc.utils;
using Single.Entities.Entity;
using Single.Services.IService;

namespace Single.Controllers.FIle;

[Route("[controller]/[action]")]
public class FileController : RocController
{
    private readonly IFileService fileService;

    public FileController(IFileService fileService)
    {
        this.fileService = fileService;
    }

    /// <summary>
    /// excel 模板下载
    /// </summary>
    /// <param name="fileId">文件名称</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{fileId}")]
    public IActionResult DownloadExcel(string fileId)
    {
        var path = PathUtil.GetAbsolutePath("excel", fileId);
        var contentType = FileUtil.GetContentType(fileId);
        // 设置响应头，指示这是一个需要下载的文件
        var fileStream = new FileStream(path, FileMode.Open);

        return File(fileStream, contentType, fileId);
    }

    [HttpPost]
    [ActionName("upExcel")]
    public async Task<RocResult> UpExcel([FromForm] RocFileInfo fileInfo)
    {
        var rocMessage = await fileService.upExcel(fileInfo);
        if (rocMessage.Code == RocCode.Success)
        {
            return Success(rocMessage.Message);
        }
        else
        {
            return Error(rocMessage.Code);
        }
    }
}
