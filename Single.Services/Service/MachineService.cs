using Microsoft.Extensions.Logging;
using Roc.Const;
using Roc.Models;
using Roc.utils;
using Single.Entities.Entity;
using Single.Services.IService;
using SqlSugar;

namespace Single.Services.Service;

using Single.Entities;

public class MachineService : IMachineService
{
    /// <summary>
    /// 仓储字段
    /// </summary>
    private SqlSugarScopeProvider repository;
    /// <summary>
    /// 日志模块
    /// </summary>
    private readonly ILogger<MachineService> logger;

    public MachineService(ISqlSugarClient _sqlSugarClient, ILogger<MachineService> logger)
    {
        this.repository = (_sqlSugarClient as SqlSugarScope).GetConnectionScope("single"); ;
        this.logger = logger;
    }

    public async Task<RocMessage> upExcel(RocFileInfo fileInfo)
    {
        var file = fileInfo.File;
        if(file == null)
        {
            return new RocMessage(RocCode.File_None);
        }
        ExcelUtil util = new ExcelUtil();
        List<MachineInfo> list = new List<MachineInfo>();
        var result = util.ReadExcel<MachineInfo>(file.OpenReadStream(), file.FileName, list,columnNum:10);
        if(result.Code != RocCode.Success)
            return result;

        // 此处代表读取正常，开始赋值默认工时
        List<MachineStandard> sts =  await repository.Queryable<MachineStandard>().ToListAsync();
        string[] arr = { "AAA",
                        "BBB",
                        "CCC"};
        foreach(MachineInfo info in list)
        {
            info.SheetName = result.Message;
            info.Id = System.Guid.NewGuid()+ "_"+ info.Id;
            if(!arr.Contains(info.Customer))
            {
                info.Arrivaldate = DateUtil.GetDateTimeAddition(info.Outdate, day: 1);
            }
        }
        var res = await repository.Insertable<MachineInfo>(list).ExecuteCommandAsync();
        if(res < 1)
        {
            return new RocMessage(RocCode.FailMessage, $"数据导入异常，少于1条");
        }

        return new RocMessage(RocCode.Success);
    }


    public async Task<RocPage<IEnumerable<MachineInfo>>> getPageList(RocPage<MachineInfo> info)
    {
        RefAsync<int> total = 0;
        var list = await repository.Queryable<MachineInfo>()
            .WhereIF(
                info.Data.MacName.IsNotNullOrEmpty(),
                x => x.MacName.Contains(info.Data.MacName))
            .OrderByIF(
                info.OrderField.IsNotNullOrEmpty(),
                t => info.OrderField,
                info.Ascending ? OrderByType.Asc : OrderByType.Desc)
            .ToPageListAsync(info.PageIndex, info.PageSize, total);

        return new RocPage<IEnumerable<MachineInfo>>
        {
            PageIndex = info.PageIndex,
            Ascending = info.Ascending,
            PageSize = info.PageSize,
            OrderField = info.OrderField,
            Total = (long)total,
            Data = list
        };
    }

    public async Task<int> Modify(MachineInfo info)
    {
        var code = await repository.Updateable<MachineInfo>(info).ExecuteCommandAsync();
        if(code >  0) 
            return RocCode.Success;
        else
            return RocCode.FailMessage;

    }
}
