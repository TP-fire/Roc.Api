using Roc.Build;
using Roc.Models;
using Single.Entities.Entity;

namespace Single.Services.IService;

/// <summary>
/// 描述： This is the class description
/// 作者：TP
/// 创建日期：2024/5/29 8:11:06
/// 版本：1.4
/// </summary>

public interface ISysUserServices : RocInterface
{
    /// <summary>
    /// 添加新的SysUser
    /// </summary>
    Task<int> addAsync(UserInfo input, RocUser rocUser);
    /// <summary>
    /// 根据ID删除SysUser
    /// </summary>
    Task<int> deleteAsync(UserInfo input, RocUser rocUser);
    /// <summary>
    /// 更新SysUser
    /// </summary>
    Task<int> ModifyAsync(UserInfo entity, RocUser rocUser);
    /// <summary>
    /// 分页获取所有SysUser
    /// </summary>
    Task<RocPage<IEnumerable<UserInfo>>> getPagelist(RocPage<UserInfo> inputs);
    /// <summary>
    /// 获取所有SysUser
    /// </summary>
    Task<IEnumerable<UserInfo>> getList(UserInfo input);
    /// <summary>
    /// 根据ID获取单个SysUser
    /// </summary>
    Task<UserInfo> getEntity(UserInfo input);
}

