using Roc.Build;
using Roc.Models;
using Single.Entities.Entity;

namespace Single.Services.IService;

/// <summary>
/// 描述： This is the class description
/// 作者：TP
/// 创建日期：2024/6/11 14:40:47
/// 版本：1.6
/// </summary>

public interface ISysRoleServices : RocInterface
{
    /// <summary>
    /// 添加新的SysRole
    /// </summary>
    Task<int> addAsync(SysRole input, RocUser rocUser);
    /// <summary>
    /// 根据ID删除SysRole
    /// </summary>
    Task<int> deleteAsync(SysRole input, RocUser rocUser);
    /// <summary>
    /// 更新SysRole
    /// </summary>
    Task<int> ModifyAsync(SysRole input, RocUser rocUser);
    /// <summary>
    /// 分页获取所有SysRole
    /// </summary>
    Task<RocPage<IEnumerable<SysRole>>> getPagelist(RocPage<SysRole> inputs);
    /// <summary>
    /// 获取所有SysRole
    /// </summary>
    Task<IEnumerable<SysRole>> getList(SysRole input);
    /// <summary>
    /// 根据ID获取单个SysRole
    /// </summary>
    Task<SysRole> getEntity(SysRole input);
}

