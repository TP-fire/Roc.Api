using Microsoft.AspNetCore.Mvc;
using Roc.Build;
using Roc.Framework;
using Roc.Models;
using Roc.utils;
using Single.Entities.Entity;
using Single.Services.IService;

namespace Single.Controllers.System;

/// <summary>
/// 用户管理中心
/// </summary>
[Route("[controller]/[action]")]
public class UserController : RocController
{

    /// <summary>
    /// 首页 服务接口
    /// </summary>
    private readonly IUserService userService;
    /// <summary>
    /// 登陆接口获取Token
    /// </summary>
    private readonly IConnectService connectService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public UserController(IUserService userService,
                            IConnectService connectService)
    {
        this.userService = userService;
        this.connectService = connectService;
    }

    /// <summary>
    /// 登陆接口
    /// </summary>
    [HttpPost]
    [ActionName("login")]
    public async Task<RocToken> login(UserInfo input)
    {
        return await connectService.getToken(input);
    }

    /// <summary>
    /// 登陆接口
    /// </summary>
    [HttpPost]
    [ActionName("logout")]
    public RocResult<string> logout()
    {
        return Success("登出成功");
    }

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ActionName("info")]
    public async Task<RocResult<UserInfo>> getCurrentUserAsync()
    {
        string Authorization = this.HttpContext.Request.Headers["roc-token"];
        RocUser rocUser2 = RocSupportToken.GetUserFromToken(Authorization);


        var rocUser = this.rocUser;
        var user = await userService.GetEntityAsync(rocUser);

        return this.Success(user);
    }

    /// <summary>
    /// 获取用户头像
    /// </summary>
    /// <param name="fileName">图片名称</param>
    /// <returns></returns>
    [HttpGet]
    [ActionName("getheadicon")]
    public async Task<IActionResult> getHeadIconAsync(string fileName)
    {
        //获取图片字节码
        var (data, pictureBytes) = await userService.GetPictureAsync(fileName);
        if (pictureBytes == null || pictureBytes.Length == 0)
            return NotFound();

        //获取文件ContentType
        var contentType = fileName.GetContentType();
        if (contentType.IsNullOrEmpty())
            return BadRequest();

        return File(pictureBytes, contentType, data);
    }
}
