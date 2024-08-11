using Bi.Services.IService;
using Microsoft.Extensions.Logging;
using Roc.Const;
using Roc.Models;
using Roc.utils;
using Single.Entities.Entity;
using Single.Services.Service;
using SqlSugar;

namespace Bi.Services.Service;

internal class DataItemDetailService : IDataItemDetailService
{
    /// <summary>
    /// 日志工具
    /// </summary>
    private readonly ILogger<SysUserServices> logger;
    /// <summary>
    /// 仓储字段
    /// </summary>
    private SqlSugarScopeProvider repository;

    public DataItemDetailService(ISqlSugarClient _sqlSugarClient,
                            ILogger<SysUserServices> logger)
    {
        this.logger = logger;
        repository = (_sqlSugarClient as SqlSugarScope).GetConnectionScope("single");
    }


    public async Task<int> insertTree(DataItem input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        input.Create(rocUser);
        logger.LogInformation($"insert DataItem by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Insertable(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    public async Task<int> deleteTree(DataItem input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        logger.LogInformation($"delete DataItem by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Deleteable<DataItem>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    public async Task<int> modifyTree(DataItem input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        logger.LogInformation($"Modify DataItem by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Updateable<DataItem>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    public async Task<int> insert(DataItemDetail input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        input.Create(rocUser);
        logger.LogInformation($"insert DataItemDetail by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Insertable(input).ExecuteCommandAsync();
        return RocCode.Success;
    }
    public async Task<int> delete(DataItemDetail input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        logger.LogInformation($"delete DataItemDetail by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Deleteable<DataItemDetail>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }
    public async Task<int> modify(DataItemDetail input, RocUser rocUser)
    {
        if (rocUser == null)
            return RocCode.User_None;

        logger.LogInformation($"Modify DataItemDetail by {rocUser.Account} param {JsonUtil.ObjectToJson(input)}");
        await repository.Updateable<DataItemDetail>(input).ExecuteCommandAsync();
        return RocCode.Success;
    }

    public async Task<IEnumerable<DataItemTree>> getDataDictTree()
    {
        List<DataItemTree> tree = new();
        var dicts = await repository.Queryable<DataItem>()
            .OrderBy(x => x.SortCode).ToListAsync();
        dicts.ForEach(x =>
        {
            tree.Add(new DataItemTree
            {
                Id = x.Id,
                ParentId = x.ParentId,
                Title = x.ItemName,
                Code = x.ItemCode,
                SortCode = x.SortCode,
                Expand = true,
                contextmenu = true
            });
        });
        return tree;
    }

    public async Task<IEnumerable<DataItemDetail>> GetListAsync(DataItemDetail input)
    {
        //var dt = repository.Ado.GetDataTable("select * from  sys_dataitem");

        var expable = Expressionable.Create<DataItemDetail>();
        //主键Id
        if (input.Id.IsNotNullOrEmpty())
            expable = expable.And(x =>x.Id == input.Id);

        //数据字典主表Id
        if (!input.ItemId.IsNullOrEmpty())
            expable = expable.And(x => x.ItemId == input.ItemId);

        //明细编码
        if (!input.DetailCode.IsNullOrEmpty())
            expable = expable.And(x => x.DetailCode == input.DetailCode);

        //明细名称
        if (!input.DetailName.IsNullOrEmpty())
            expable = expable.And(x => x.DetailName.Contains(input.DetailName));

        //是否有效
        if (input.Enabled != -1)
            expable = expable.And(x => x.Enabled == input.Enabled);

        return await repository.Queryable<DataItemDetail>().Where(expable.ToExpression()).OrderBy(x=>x.SortCode).ToListAsync();

    }

    public async Task<RocPage<IEnumerable<DataItemDetail>>> getPagelist(RocPage<DataItemDetail> inputs)
    {
        RefAsync<int> total = 0;
        var list = await repository.Queryable<DataItemDetail>()
            .Where(x => x.ItemId == inputs.Data.ItemId)
            .OrderBy(inputs.OrderField)
            .ToPageListAsync(inputs.PageIndex, inputs.PageSize, total);
        return new RocPage<IEnumerable<DataItemDetail>>
        {
            PageIndex = inputs.PageIndex,
            Ascending = inputs.Ascending,
            PageSize = inputs.PageSize,
            OrderField = inputs.OrderField,
            Total = total,
            Data = list
        };
    }

}
