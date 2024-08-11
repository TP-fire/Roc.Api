using Roc.Models;
using SqlSugar;

namespace Single.Entities.Entity;

[SugarTable("AlarmMacStandard")]
public class MachineStandard : RocEntity
{
    // 工时标准ID 自动生成

    /// <summary>
    /// 名称 
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    public string? Code { get; set; }
    /// <summary>
    /// 标准工时
    /// </summary>
    public int Standard { get; set; } = 0;
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateDate { get; set; }
}
