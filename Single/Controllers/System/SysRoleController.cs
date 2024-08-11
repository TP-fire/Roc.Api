using Roc.Build;
using Roc.Const;
using Roc.Models;
using Microsoft.AspNetCore.Mvc;
using Single.Entities.Entity;
using Single.Services.IService;

namespace Single.Controllers.System;

/// <summary>
/// 描述： This is the class description
/// 作者：TP
/// 创建日期：2024/6/11 14:17:08
/// 版本：1.6
/// </summary>

[Route("[controller]/[action]")]
public class SysRoleController : RocController
{
    /// <summary>
    /// SysRole 服务接口
    /// </summary>
    private readonly ISysRoleServices service;

    /// <summary>
    /// SysRole 构造函数
    /// </summary>
    public SysRoleController(ISysRoleServices service)
    {
        this.service = service;
    }

    /// <summary>
    /// SysRole 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("insert")]
    public async Task<RocResult> insertAsync(SysRole input)
    {
        var code = await service.addAsync(input, this.rocUser);
        if (code == RocCode.Success)
            return Success("插入成功！");
        else
            return Error(code);
    }

    /// <summary>
    /// 删除 SysRole 信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("delete")]
    public async Task<RocResult> deleteAsync(SysRole input)
    {
        var code = await service.deleteAsync(input, this.rocUser);
        if (code == RocCode.Success)
            return Success("删除成功！");
        else
            return Error(code);
    }

    /// <summary>
    /// 更新 SysRole 信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("modify")]
    public async Task<RocResult> modifyAsync(SysRole input)
    {
        var code = await service.ModifyAsync(input,this.rocUser);
        if (code == RocCode.Success)
            return Success("修改成功！");
        else
            return Error(code);
    }

    /// <summary>
    /// SysRole  列表
    /// </summary>
    [HttpPost]
    [ActionName("getPagelist")]
    public async Task<RocResult<RocPage<IEnumerable<SysRole>>>> getPagelist(RocPage<SysRole> inputs)
    {
        return Success(await service.getPagelist(inputs));
    }

    /// <summary>
    /// SysRole  查询全部列表
    /// </summary>
    [HttpPost]
    [ActionName("getList")]
    public async Task<RocResult<IEnumerable<SysRole>>> getList(SysRole input)
    {
        return Success(await service.getList(input));
    }

    /// <summary>
    /// 获取 SysRole  单个
    /// </summary>
    [HttpPost]
    [ActionName("getEntity")]
    public async Task<RocResult<SysRole>> getEntity(SysRole input)
    {
        return Success(await service.getEntity(input));
    }
    
}

