using Roc.Models;
using SqlSugar;

namespace Single.Entities.Entity;

/// <summary>
/// 数据字典主表
/// </summary>
[SugarTable("sys_dataitem")]
public class DataItem : RocSupplyEntity
{
    /// <summary>
    /// 父级数据字典Id
    /// </summary>
    public string ParentId { get; set; }

    /// <summary>
    /// 字典编码
    /// </summary>
    public string ItemCode { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// 排序码
    /// </summary>
    public int SortCode { get; set; }
}