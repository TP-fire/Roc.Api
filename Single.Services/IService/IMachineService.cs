using Microsoft.Extensions.Logging;
using Roc.Build;
using Roc.Const;
using Roc.Models;
using Single.Entities;
using Single.Entities.Entity;
using Single.Services.Service;
using SqlSugar;

namespace Single.Services.IService;

public interface IMachineService : RocInterface
{
    /// <summary>
    /// 上传设备出厂信息
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <returns></returns>
    Task<RocMessage> upExcel(RocFileInfo fileInfo);
    /// <summary>
    /// 分页获取设备出场信息
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    Task<RocPage<IEnumerable<MachineInfo>>> getPageList(RocPage<MachineInfo> info);
    /// <summary>
    /// 修改设备信息
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    Task<int> Modify(MachineInfo info);
}
