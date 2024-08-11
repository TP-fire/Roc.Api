using Microsoft.Extensions.Logging;
using Roc.Models;
using Roc.utils;
using Single.Entities.Entity;
using Single.Services.IService;
using SqlSugar;

namespace Single.Services.Service;

public class FileManageService : IFileManageService
{
    /// <summary>
    /// 仓储字段
    /// </summary>
    private SqlSugarScopeProvider repository;
    /// <summary>
    /// 日志模块
    /// </summary>
    private readonly ILogger<FileManageService> logger;

    public FileManageService(ISqlSugarClient _sqlSugarClient, ILogger<FileManageService> logger)
    {
        this.repository = (_sqlSugarClient as SqlSugarScope).GetConnectionScope("single"); ;
        this.logger = logger;
    }

    public async Task<RocPage<IEnumerable<AlarmReportLogs>>> getPageList(RocPage<AlarmReportLogs> info)
    {
        RefAsync<int> total = 0;
        var list = await repository.Queryable<AlarmReportLogs>()
            .WhereIF(
                info.Data.BelongDate.IsNotNullOrEmpty(),
                x => x.BelongDate.Contains(info.Data.BelongDate))
            .WhereIF(
                info.Data.AlarmType.IsNotNullOrEmpty(),
                x => x.AlarmType.Contains(info.Data.AlarmType))
            .OrderByIF(
                info.OrderField.IsNotNullOrEmpty(),
                t => info.OrderField,
                info.Ascending ? OrderByType.Asc : OrderByType.Desc)
            .ToPageListAsync(info.PageIndex, info.PageSize, total);

        return new RocPage<IEnumerable<AlarmReportLogs>>
        {
            PageIndex = info.PageIndex,
            Ascending = info.Ascending,
            PageSize = info.PageSize,
            OrderField = info.OrderField,
            Total = (long)total,
            Data = list
        };
    }
}
