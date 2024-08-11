using Bi.Services.IService;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using Roc.Build;
using Roc.Const;
using Roc.Extensions;
using Roc.Models;
using Roc.Work.Model;
using Single.Entities.Entity;

namespace Single.Controllers.System;

[Route("[controller]/[action]")]
public class DataItemDetailController : RocController
{
    /// <summary>
    /// 字段
    /// </summary>
    private readonly IDataItemDetailService dataItemDetailService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dataItemDetailService"></param>
    public DataItemDetailController(IDataItemDetailService dataItemDetailService)
    {
        this.dataItemDetailService = dataItemDetailService;
    }
    /// <summary>
    /// 新增数据字典 分支
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ActionName("insertTree")]
    public async Task<RocResult> insertTree(DataItem input)
    {
        var code = await dataItemDetailService.insertTree(input, this.rocUser);
        if (code == RocCode.Success)
            return Success();
        else
            return Error(code);
    }/// <summary>
     /// 删除数据字典 分支
     /// </summary>
     /// <returns></returns>
    [HttpPost]
    [ActionName("deleteTree")]
    public async Task<RocResult> deleteTree(DataItem input)
    {
        var code = await dataItemDetailService.deleteTree(input,this.rocUser);
        if (code == RocCode.Success)
            return Success("删除成功");
        else
            return Error(code);
    }/// <summary>
     /// 修改数据字典 分支
     /// </summary>
     /// <returns></returns>
    [HttpPost]
    [ActionName("modifyTree")]
    public async Task<RocResult> modifyTree(DataItem input)
    {
        int code = await dataItemDetailService.modifyTree(input, this.rocUser);
        if (code == RocCode.Success)
            return Success();
        else
            return Error(code);
    }
    /// <summary>
    /// 新增数据字典明细
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ActionName("insert")]
    public async Task<RocResult> insert(DataItemDetail input)
    {
        var code = await dataItemDetailService.insert(input, this.rocUser);
        if (code == RocCode.Success)
            return Success();
        else
            return Error(code);
    }/// <summary>
     /// 删除数据字典明细
     /// </summary>
     /// <returns></returns>
    [HttpPost]
    [ActionName("delete")]
    public async Task<RocResult> delete(DataItemDetail input)
    {
        var code = await dataItemDetailService.delete(input, this.rocUser);
        if (code == RocCode.Success)
            return Success("删除成功");
        else
            return Error(code);
    }/// <summary>
     /// 修改数据字典明细
     /// </summary>
     /// <returns></returns>
    [HttpPost]
    [ActionName("modify")]
    public async Task<RocResult> modify(DataItemDetail input)
    {
        int code = await dataItemDetailService.modify(input, this.rocUser);
        if (code == RocCode.Success)
            return Success();
        else
            return Error(code);
    }  


    /// <summary>
    /// 分页获取数据字典明细列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ActionName("getPagelist")]
    public async Task<RocResult<RocPage<IEnumerable<DataItemDetail>>>> getPagelist(RocPage<DataItemDetail> inputs)
    {
        var data = await dataItemDetailService.getPagelist(inputs);
        return Success(data);
    }

    /// <summary>
    /// 获取数据字典明细列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ActionName("getlist")]
    public async Task<RocResult<IEnumerable<DataItemDetail>>> GetListAsync(DataItemDetail input)
    {
        var data = await dataItemDetailService.GetListAsync(input);
        return Success(data);
    }

    /// <summary>
    /// 获取数据字典树状结构
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    [HttpGet]
    [ActionName("getDataDictTree")]
    public async Task<RocResult<IEnumerable<DataItemTree>>> getDataDictTree()
    {
        var res = await dataItemDetailService.getDataDictTree();
        if (res.Count() > 0)
        {
            res = res
                    .TreeToJson("Id", new[] { "0" }, childName: "children")
                    .ToObject<IEnumerable<DataItemTree>>();

        }
        return Success(res);
    }

}
