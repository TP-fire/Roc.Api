using Roc.Models;
using SqlSugar;

namespace Single.Entities.Entity;

[SugarTable("sys_user")]
public class UserInfo : RocUser 
{
    /// <summary>
    /// 头像
    /// </summary>
    public string? HeadIcon { get; set; }

    /// <summary>
    /// 简拼
    /// </summary>
    public string? SimpleSpelling { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 角色ids
    /// </summary>
    public string? RoleIds { get; set; }

    /// <summary>
    /// 公司ids
    /// </summary>
    public string? CompanyIds { get; set; }

    /// <summary>
    /// 部门ids
    /// </summary>
    public string? DepartmentIds { get; set; }

    /// <summary>
    /// 系统标识
    /// </summary>
    public string? SystemFlag { get; set; }

    /// <summary>
    /// 管理员
    /// </summary>
    public int IsAdministrator { get; set; }

    /// <summary>
    /// 上次修改密码时间
    /// </summary>
    public DateTime? LastPasswordChangeTime { get; set; }

}
