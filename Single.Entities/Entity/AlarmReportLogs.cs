using Roc.Models;

namespace Single.Entities.Entity;

public class AlarmReportLogs : RocEntity
{
    /// <summary>
    /// 归属日期
    /// </summary>
    public string? BelongDate { get; set; }
    /// <summary>
    /// 通知类型
    /// </summary>
    public string? AlarmType { get; set; }
    /// <summary>
    /// 通知内容
    /// </summary>
    public string? AlarmContent { get; set; }
    /// <summary>
    /// 状态 是否通知
    /// </summary>
    public int Status { get; set; }
}
