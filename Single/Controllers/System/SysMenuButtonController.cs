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
/// 创建日期：2024/6/6 14:33:58
/// 版本：1.5
/// </summary>

[Route("[controller]/[action]")]
public class SysMenuButtonController : RocController
{
    /// <summary>
    /// SysMenuButton 服务接口
    /// </summary>
    private readonly ISysMenuButtonServices service;

    /// <summary>
    /// SysMenuButton 构造函数
    /// </summary>
    public SysMenuButtonController(ISysMenuButtonServices service)
    {
        this.service = service;
    }

    /// <summary>
    /// SysMenuButton 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("insert")]
    public async Task<RocResult> insertAsync(SysMenuButton input)
    {
        var code = await service.addAsync(input, this.rocUser);
        if (code == RocCode.Success)
            return Success("插入成功！");
        else
            return Error(code);
    }

    /// <summary>
    /// 删除 SysMenuButton 信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("delete")]
    public async Task<RocResult> deleteAsync(SysMenuButton input)
    {
        var code = await service.deleteAsync(input, this.rocUser);
        if (code == RocCode.Success)
            return Success("删除成功！");
        else
            return Error(code);
    }

    /// <summary>
    /// 更新 SysMenuButton 信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("modify")]
    public async Task<RocResult> modifyAsync(SysMenuButton input)
    {
        var code = await service.ModifyAsync(input,this.rocUser);
        if (code == RocCode.Success)
            return Success("修改成功！");
        else
            return Error(code);
    }

    /// <summary>
    /// SysMenuButton  列表
    /// </summary>
    [HttpPost]
    [ActionName("getPagelist")]
    public async Task<RocResult<RocPage<IEnumerable<SysMenuButton>>>> getPagelist(RocPage<SysMenuButton> inputs)
    {
        return Success(await service.getPagelist(inputs));
    }

    /// <summary>
    /// SysMenuButton  查询全部列表
    /// </summary>
    [HttpPost]
    [ActionName("getList")]
    public async Task<RocResult<IEnumerable<SysMenuButton>>> getList(SysMenuButton input)
    {
        return Success(await service.getList(input));
    }

    /// <summary>
    /// 获取 SysMenuButton  单个
    /// </summary>
    [HttpPost]
    [ActionName("getEntity")]
    public async Task<RocResult<SysMenuButton>> getEntity(SysMenuButton input)
    {
        return Success(await service.getEntity(input));
    }
    
}

