using Roc.Build;
using Roc.Models;
using Single.Entities.Entity;

namespace Single.Services.IService;

/// <summary>
/// 描述： This is the class description
/// 作者：TP
/// 创建日期：2024/6/6 10:34:04
/// 版本：1.5
/// </summary>

public interface ISysMenuButtonServices : RocInterface
{
    /// <summary>
    /// 添加新的SysMenuButton
    /// </summary>
    Task<int> addAsync(SysMenuButton input, RocUser rocUser);
    /// <summary>
    /// 根据ID删除SysMenuButton
    /// </summary>
    Task<int> deleteAsync(SysMenuButton input, RocUser rocUser);
    /// <summary>
    /// 更新SysMenuButton
    /// </summary>
    Task<int> ModifyAsync(SysMenuButton input, RocUser rocUser);
    /// <summary>
    /// 分页获取所有SysMenuButton
    /// </summary>
    Task<RocPage<IEnumerable<SysMenuButton>>> getPagelist(RocPage<SysMenuButton> inputs);
    /// <summary>
    /// 获取所有SysMenuButton
    /// </summary>
    Task<IEnumerable<SysMenuButton>> getList(SysMenuButton input);
    /// <summary>
    /// 根据ID获取单个SysMenuButton
    /// </summary>
    Task<SysMenuButton> getEntity(SysMenuButton input);
}

