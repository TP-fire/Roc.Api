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
/// 创建日期：2024/5/29 8:11:06
/// 版本：1.4
/// </summary>

public class SysUserServices : ISysUserServices
{
    /// <summary>
    /// 日志工具
    /// </summary>
    private readonly ILogger<SysUserServices> logger;
    /// <summary>
    /// 仓储字段
    /// </summary>
    private SqlSugarScopeProvider repository;

    public SysUserServices(ISqlSugarClient _sqlSugarClient,
                            ILogger<SysUserServices> logger)
    {
        this.logger = logger;
        repository = (_sqlSugarClient as SqlSugarScope).GetConnectionScope("single");
    }



    /// <summary>
    /// 添加新的SysUser
    /// </summary>
    public async Task<int> addAsync(UserInfo input, RocUser rocUser)
    {
        if(rocUser == null)
            return RocCode.User_None;

        input.Create(rocUser);
        logger.LogInformation($"insert UserInfo by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Insertable(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    /// <summary>
    /// 根据ID删除SysUser
    /// </summary>
    public async Task<int> deleteAsync(UserInfo input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        logger.LogInformation($"delete UserInfo by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Deleteable<UserInfo>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    /// <summary>
    /// 更新SysUser
    /// </summary>
    public async Task<int> ModifyAsync(UserInfo input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        logger.LogInformation($"Modify UserInfo by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Updateable<UserInfo>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    /// <summary>
    /// 分页获取所有SysUser
    /// </summary>
    public async Task<RocPage<IEnumerable<UserInfo>>> getPagelist(RocPage<UserInfo> inputs)
    {
        //分页查询
        RefAsync<int> total = 0;
        var input = inputs.Data;
        var data = await repository.Queryable<UserInfo>()
            .WhereIF(
                !string.IsNullOrEmpty(input.Id),
                x => x.Id == input.Id)
            .WhereIF(
                !string.IsNullOrEmpty(input.Name),
                x => x.Name.Contains(input.Name))
            .WhereIF(
                !string.IsNullOrEmpty(input.Account),
                x => x.Account.Contains(input.Account))
            .WhereIF(
                !string.IsNullOrEmpty(input.Phone),
                x => x.Phone.Contains(input.Phone))
            .WhereIF(
                !string.IsNullOrEmpty(input.CompanyIds),
                x => x.CompanyIds.Contains(input.CompanyIds))
            .WhereIF(
                !string.IsNullOrEmpty(input.DepartmentIds),
                x => x.DepartmentIds.Contains(input.DepartmentIds))
            .OrderByPropertyName(inputs.OrderField, inputs.Ascending?OrderByType.Asc: OrderByType.Desc)
            .ToPageListAsync(inputs.PageIndex, inputs.PageSize, total);

        return new RocPage<IEnumerable<UserInfo>>
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
    /// 获取所有SysUser
    /// </summary>
    public async Task<IEnumerable<UserInfo>> getList(UserInfo input)
    {
        //分页查询
        var data = await repository.Queryable<UserInfo>()
            .WhereIF(
                !string.IsNullOrEmpty(input.Id),
                x => x.Id == input.Id)
            .WhereIF(
                !string.IsNullOrEmpty(input.Name),
                x => x.Name.Contains(input.Name))
            .WhereIF(
                !string.IsNullOrEmpty(input.Account),
                x => x.Account.Contains(input.Account))
            .WhereIF(
                !string.IsNullOrEmpty(input.Phone),
                x => x.Phone.Contains(input.Phone))
            .WhereIF(
                !string.IsNullOrEmpty(input.CompanyIds),
                x => x.CompanyIds.Contains(input.CompanyIds))
            .WhereIF(
                !string.IsNullOrEmpty(input.DepartmentIds),
                x => x.DepartmentIds.Contains(input.DepartmentIds))
            .ToListAsync();

        return data;
    }

    public async Task<UserInfo> getEntity(UserInfo input)
    {
        var data = await repository.Queryable<UserInfo>()
            .WhereIF(
                !string.IsNullOrEmpty(input.Id),
                x => x.Id == input.Id)
            .WhereIF(
                !string.IsNullOrEmpty(input.Name),
                x => x.Name.Contains(input.Name))
            .WhereIF(
                !string.IsNullOrEmpty(input.Account),
                x => x.Account.Contains(input.Account))
            .WhereIF(
                !string.IsNullOrEmpty(input.Phone),
                x => x.Phone.Contains(input.Phone))
            .WhereIF(
                !string.IsNullOrEmpty(input.CompanyIds),
                x => x.CompanyIds.Contains(input.CompanyIds))
            .WhereIF(
                !string.IsNullOrEmpty(input.DepartmentIds),
                x => x.DepartmentIds.Contains(input.DepartmentIds))
            .Take(1)
            .ToListAsync();
        return data.FirstOrDefault();
    }

}

