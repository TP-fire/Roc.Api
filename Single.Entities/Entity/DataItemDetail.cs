using Roc.Models;
using SqlSugar;

namespace Single.Entities.Entity;

/// <summary>
/// 数据字典明细表
/// </summary>
[SugarTable("sys_dataitem_detail")]
public class DataItemDetail : RocSupplyEntity
{
    /// <summary>
    /// 数据字典主表Id
    /// </summary>
    public string? ItemId { get; set; }

    /// <summary>
    /// 明细编码
    /// </summary>
    public string? DetailCode { get; set; }

    /// <summary>
    /// 明细名称
    /// </summary>
    public string? DetailName { get; set; }

    /// <summary>
    /// 排序码
    /// </summary>
    public int SortCode { get; set; }

    /// <summary>
    /// 排序码
    /// </summary>
    public string? Remark { get; set; }
}
