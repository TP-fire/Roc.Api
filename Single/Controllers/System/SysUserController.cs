using Microsoft.AspNetCore.Mvc;
using Roc.Build;
using Roc.Const;
using Roc.Models;
using Single.Entities.Entity;
using Single.Services.IService;

namespace Single.Controllers.System;

/// <summary>
/// 描述： This is the class description
/// 作者：TP
/// 创建日期：2024/5/29 8:11:06
/// 版本：1.4
/// </summary>

[Route("[controller]/[action]")]
public class SysUserController:RocController
{
    /// <summary>
    /// SysUser 服务接口
    /// </summary>
    private readonly ISysUserServices service;

    /// <summary>
    /// SysUser 构造函数
    /// </summary>
    public SysUserController(ISysUserServices service)
    {
        this.service = service;
    }

    /// <summary>
    /// SysUser 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("insert")]
    public async Task<RocResult> insertAsync(UserInfo input)
    {
        var code = await service.addAsync(input, this.rocUser);
        if (code == RocCode.Success)
            return Success();
        else
            return Error(code);
    }

    /// <summary>
    /// 删除 SysUser 信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("delete")]
    public async Task<RocResult> deleteAsync(UserInfo input)
    {
        var code = await service.deleteAsync(input, this.rocUser);
        if (code == RocCode.Success)
            return Success("删除成功！");
        else
            return Error(code);
    }

    /// <summary>
    /// 更新 SysUser 信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    [ActionName("modify")]
    public async Task<RocResult> modifyAsync(UserInfo input)
    {
        var code = await service.ModifyAsync(input,this.rocUser);
        if (code == RocCode.Success)
            return Success();
        else
            return Error(code);
    }

    /// <summary>
    /// SysUser  列表
    /// </summary>
    [HttpPost]
    [ActionName("getPagelist")]
    public async Task<RocResult<RocPage<IEnumerable<UserInfo>>>> getPagelist(RocPage<UserInfo> inputs)
    {
        return Success(await service.getPagelist(inputs));
    }

    /// <summary>
    /// SysUser  查询全部列表
    /// </summary>
    [HttpPost]
    [ActionName("getList")]
    public async Task<RocResult<IEnumerable<UserInfo>>> getList(UserInfo input)
    {
        return Success(await service.getList(input));
    }

    /// <summary>
    /// 获取 SysUser  单个
    /// </summary>
    [HttpPost]
    [ActionName("getEntity")]
    public async Task<RocResult<UserInfo>> getEntity(UserInfo input)
    {
        return Success(await service.getEntity(input));
    }
    
}

