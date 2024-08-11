using Roc.Const;
using Roc.Models;
using Roc.utils;
using SqlSugar;
using Single.Entities.Entity;
using Single.Services.IService;
using Microsoft.Extensions.Logging;

namespace Single.Services.Service;

/// <summary>
/// 描述： This is the class description
/// 作者：TP
/// 创建日期：2024/6/6 11:11:19
/// 版本：1.5
/// </summary>

public class SysMenuButtonServices : ISysMenuButtonServices
{
    /// <summary>
    /// 日志模块
    /// </summary>
    private readonly ILogger<SysMenuButtonServices> logger;
    /// <summary>
    /// 仓储字段
    /// </summary>
    private SqlSugarScopeProvider repository;

    public SysMenuButtonServices(ISqlSugarClient _sqlSugarClient,
                                ILogger<SysMenuButtonServices> logger)
    {
        this.logger = logger;
        repository = (_sqlSugarClient as SqlSugarScope).GetConnectionScope("single");
    }



    /// <summary>
    /// 添加新的SysMenuButton
    /// </summary>
    public async Task<int> addAsync(SysMenuButton input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        input.Create(rocUser);
        logger.LogInformation($"insert SysMenuButton by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Insertable<SysMenuButton>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    /// <summary>
    /// 根据ID删除SysMenuButton
    /// </summary>
    public async Task<int> deleteAsync(SysMenuButton input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        logger.LogInformation($"delete SysMenuButton by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Deleteable<SysMenuButton>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    /// <summary>
    /// 更新SysMenuButton
    /// </summary>
    public async Task<int> ModifyAsync(SysMenuButton input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        logger.LogInformation($"Modify SysMenuButton by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Updateable<SysMenuButton>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    /// <summary>
    /// 分页获取所有SysMenuButton
    /// </summary>
    public async Task<RocPage<IEnumerable<SysMenuButton>>> getPagelist(RocPage<SysMenuButton> inputs)
    {
        //分页查询
        RefAsync<int> total = 0;
        var input = inputs.Data;
        var data = await repository.Queryable<SysMenuButton>()

            .WhereIF(
                input.Id.IsNotNullOrEmpty() ,
                x => x.Id == input.Id )
            .WhereIF(
                input.ParentId.IsNotNullOrEmpty() ,
                x => x.ParentId == input.ParentId )
            .WhereIF(
                input.Category != null ,
                x => x.Category == input.Category )
            .WhereIF(
                input.Name.IsNotNullOrEmpty() ,
                x => x.Name.Contains(input.Name))
            .WhereIF(
                input.Title.IsNotNullOrEmpty() ,
                x => x.Title.Contains(input.Title))
            .WhereIF(
                input.Path.IsNotNullOrEmpty() ,
                x => x.Path == input.Path )
            .WhereIF(
                input.Component.IsNotNullOrEmpty() ,
                x => x.Component == input.Component )
            .WhereIF(
                input.Icon.IsNotNullOrEmpty() ,
                x => x.Icon == input.Icon )
            .WhereIF(
                input.SortCode != null ,
                x => x.SortCode == input.SortCode )
            .OrderByPropertyName(inputs.OrderField, inputs.Ascending?OrderByType.Asc: OrderByType.Desc)
            .ToPageListAsync(inputs.PageIndex, inputs.PageSize, total);

        return new RocPage<IEnumerable<SysMenuButton>>
        {
            PageIndex = inputs.PageIndex,
            Ascending = inputs.Ascending,
            PageSize = inputs.PageSize,
            OrderField = inputs.OrderField,
            Total = (long)total,

            Data = data
        };
    }

    /// <summary>
    /// 获取所有SysMenuButton
    /// </summary>
    public async Task<IEnumerable<SysMenuButton>> getList(SysMenuButton input)
    {
        //分页查询
        var data = await repository.Queryable<SysMenuButton>()

            .WhereIF(
                input.Id.IsNotNullOrEmpty() ,
                x => x.Id == input.Id )
            .WhereIF(
                input.ParentId.IsNotNullOrEmpty() ,
                x => x.ParentId == input.ParentId )
            .WhereIF(
                input.Category != null ,
                x => x.Category == input.Category )
            .WhereIF(
                input.Name.IsNotNullOrEmpty() ,
                x => x.Name.Contains(input.Name))
            .WhereIF(
                input.Title.IsNotNullOrEmpty() ,
                x => x.Title == input.Title )
            .WhereIF(
                input.Path.IsNotNullOrEmpty() ,
                x => x.Path == input.Path )
            .WhereIF(
                input.Component.IsNotNullOrEmpty() ,
                x => x.Component == input.Component )
            .WhereIF(
                input.Icon.IsNotNullOrEmpty() ,
                x => x.Icon == input.Icon )
            .WhereIF(
                input.SortCode != null ,
                x => x.SortCode == input.SortCode )
            .ToListAsync();

        return data;
    }

    public async Task<SysMenuButton> getEntity(SysMenuButton input)
    {
        var data = await repository.Queryable<SysMenuButton>()

            .WhereIF(
                input.Id.IsNotNullOrEmpty() ,
                x => x.Id == input.Id )
            .WhereIF(
                input.ParentId.IsNotNullOrEmpty() ,
                x => x.ParentId == input.ParentId )
            .WhereIF(
                input.Category != null ,
                x => x.Category == input.Category )
            .WhereIF(
                input.Name.IsNotNullOrEmpty() ,
                x => x.Name.Contains(input.Name))
            .WhereIF(
                input.Title.IsNotNullOrEmpty() ,
                x => x.Title == input.Title )
            .WhereIF(
                input.Path.IsNotNullOrEmpty() ,
                x => x.Path == input.Path )
            .WhereIF(
                input.Component.IsNotNullOrEmpty() ,
                x => x.Component == input.Component )
            .WhereIF(
                input.Icon.IsNotNullOrEmpty() ,
                x => x.Icon == input.Icon )
            .WhereIF(
                input.SortCode != null ,
                x => x.SortCode == input.SortCode )
            .Take(1)
            .ToListAsync();
        return data.FirstOrDefault();
    }

}

