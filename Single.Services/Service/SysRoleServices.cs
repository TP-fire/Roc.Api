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
/// 创建日期：2024/6/11 14:40:47
/// 版本：1.6
/// </summary>

public class SysRoleServices : ISysRoleServices
{
    /// <summary>
    /// 日志模块
    /// </summary>
    private readonly ILogger<SysRoleServices> logger;
    /// <summary>
    /// 仓储字段
    /// </summary>
    private SqlSugarScopeProvider repository;

    public SysRoleServices(ISqlSugarClient _sqlSugarClient,
                                ILogger<SysRoleServices> logger)
    {
        this.logger = logger;
        repository = (_sqlSugarClient as SqlSugarScope).GetConnectionScope("single");
    }



    /// <summary>
    /// 添加新的SysRole
    /// </summary>
    public async Task<int> addAsync(SysRole input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        input.Create(rocUser);
        logger.LogInformation($"insert SysRole by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Insertable<SysRole>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    /// <summary>
    /// 根据ID删除SysRole
    /// </summary>
    public async Task<int> deleteAsync(SysRole input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        logger.LogInformation($"delete SysRole by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Deleteable<SysRole>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    /// <summary>
    /// 更新SysRole
    /// </summary>
    public async Task<int> ModifyAsync(SysRole input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        logger.LogInformation($"Modify SysRole by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Updateable<SysRole>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    /// <summary>
    /// 分页获取所有SysRole
    /// </summary>
    public async Task<RocPage<IEnumerable<SysRole>>> getPagelist(RocPage<SysRole> inputs)
    {
        //分页查询
        RefAsync<int> total = 0;
        var input = inputs.Data;
        var data = await repository.Queryable<SysRole>()

            .WhereIF(
                input.Id.IsNotNullOrEmpty() ,
                x => x.Id == input.Id )
            .WhereIF(
                input.RoleId.IsNotNullOrEmpty() ,
                x => x.RoleId == input.RoleId )
            .WhereIF(
                input.RoleName.IsNotNullOrEmpty() ,
                x => x.RoleName.Contains(input.RoleName))
            .WhereIF(
                input.MenubuttonId.IsNotNullOrEmpty() ,
                x => x.MenubuttonId == input.MenubuttonId )
            .OrderByPropertyName(inputs.OrderField, inputs.Ascending?OrderByType.Asc: OrderByType.Desc)
            .ToPageListAsync(inputs.PageIndex, inputs.PageSize, total);

        return new RocPage<IEnumerable<SysRole>>
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
    /// 获取所有SysRole
    /// </summary>
    public async Task<IEnumerable<SysRole>> getList(SysRole input)
    {
        //分页查询
        var data = await repository.Queryable<SysRole>()

            .WhereIF(
                input.Id.IsNotNullOrEmpty() ,
                x => x.Id == input.Id )
            .WhereIF(
                input.RoleId.IsNotNullOrEmpty() ,
                x => x.RoleId == input.RoleId )
            .WhereIF(
                input.RoleName.IsNotNullOrEmpty() ,
                x => x.RoleName.Contains(input.RoleName))
            .WhereIF(
                input.MenubuttonId.IsNotNullOrEmpty() ,
                x => x.MenubuttonId == input.MenubuttonId )
            .ToListAsync();

        return data;
    }

    public async Task<SysRole> getEntity(SysRole input)
    {
        var data = await repository.Queryable<SysRole>()

            .WhereIF(
                input.Id.IsNotNullOrEmpty() ,
                x => x.Id == input.Id )
            .WhereIF(
                input.RoleId.IsNotNullOrEmpty() ,
                x => x.RoleId == input.RoleId )
            .WhereIF(
                input.RoleName.IsNotNullOrEmpty() ,
                x => x.RoleName.Contains(input.RoleName))
            .WhereIF(
                input.MenubuttonId.IsNotNullOrEmpty() ,
                x => x.MenubuttonId == input.MenubuttonId )
            .Take(1)
            .ToListAsync();
        return data.FirstOrDefault();
    }

}

