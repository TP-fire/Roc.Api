using Roc.Attributes;
using Roc.Models;
using SqlSugar;

namespace Single.Entities.Entity;

[SugarTable("AlarmMacInfo")]
public class MachineInfo : RocEntity
{
    // 项目号是ID

    /// <summary>
    ///  
    /// </summary>
    [ExcelColumn("项目号")]
    public string? ProjectCode { get; set; }
    /// <summary>
    /// 客户（地址）
    /// </summary>
    [ExcelColumn("客户")]
    public string? Customer { get; set; }
    /// <summary>
    /// 设备名称 
    /// </summary>
    [ExcelColumn("货物")]
    public string? MacName { get; set; }
    /// <summary>
    /// 存货编码
    /// </summary>
    [ExcelColumn("存货编码")]
    public string? StockCode { get; set; }
    /// <summary>
    /// 型号
    /// </summary>
    [ExcelColumn("型号")]
    public string? TypeCode { get; set; }
    /// <summary>
    /// 出货日期
    /// </summary>
    [ExcelColumn("出货日期")]
    public DateTime? Outdate { get; set; }
    /// <summary>
    /// 出货数量
    /// </summary>
    [ExcelColumn(" 出货数量")]
    public int OutNum { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    [ExcelColumn("备注")]
    public string? Remark { get; set; }
    /// <summary>
    /// 标准工时
    /// </summary>
    [ExcelColumn("标准工时")]
    public int Workhours { get; set; }
    /// <summary>
    /// 到货日期
    /// </summary>
    [ExcelColumn("到货日期")]
    public DateTime? Arrivaldate { get; set; }
    /// <summary>
    /// 额外工时
    /// </summary>
    [ExcelColumn("额外工时")]
    public int MoreWorkhours { get; set; }
    /// <summary>
    /// 所属分类
    /// </summary>
    [ExcelColumn("所属分类")]
    public string? TypeId { get; set; }
    /// <summary>
    /// sheet名称
    /// </summary>
    [ExcelColumn("sheet名称")]
    public string? SheetName { get; set; }
}
