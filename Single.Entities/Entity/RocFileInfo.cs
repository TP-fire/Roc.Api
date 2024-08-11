using Microsoft.AspNetCore.Http;

namespace Single.Entities.Entity;

/// <summary>
/// 接受文件实体类
/// </summary>
public class RocFileInfo
{
    /// <summary>
    /// excel文件上传
    /// </summary>
    public IFormFile? File { get; set; }
}
