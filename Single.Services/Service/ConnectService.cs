using Roc.Const;
using Roc.Framework;
using Roc.Models;
using Roc.utils;
using Single.Entities.Entity;
using Single.Services.IService;
using SqlSugar;
using System.Security.Claims;

namespace Single.Services.Service;

public class ConnectService : IConnectService
{
    /// <summary>
    /// 数据库链接
    /// </summary>
    private SqlSugarScope scope;

    public ConnectService(ISqlSugarClient _sqlSugarClient)
    {
        scope = _sqlSugarClient as SqlSugarScope;
    }

    public async Task<RocToken> getToken(UserInfo input)
    {
        RocJwt settings = new();
        var repository = scope.GetConnectionScope("authDb");

        var adminFlag = RocSupportConfig.GetConfigArrByKey("Administrators").Contains(input.Account);

        // 此处验证OA密码，如密码正确自动注册账号
        string password = input.Password.ToMd5();

        var oaUser = await repository.SqlQueryable<UserInfo>($@"select a.loginid account,
                                                                    a.password ,
                                                                    a.lastname name,
                                                                    'coin.png' headIcon ,
                                                                    a.pinyinlastname simpleSpelling,
                                                                    a.mobile phone,
                                                                    c.departmentname  departmentIds,
                                                                    b.subcompanyname  companyIds,
                                                                    REPLACE(a.pinyinlastname,'^','' ) + '@Hostar.com' email,
                                                                    'roc' systemFlag,
                                                                    1 source
                                                                    from HrmPinYinResource a 
                                                                    left join HrmSubCompany b on a.subcompanyid1 = b.id 
                                                                    left join HrmDepartment c on a.departmentid = c.id 
                                                                    where loginid ='{input.Account}'").FirstAsync();
        // 如果不是管理员判断OA密码是否正确
        if (!adminFlag && (oaUser == null || password != oaUser.Password))
        {
            return new()
            {
                Access_token = null,
                Refresh_token = null,
                Code = RocCode.FailMessage,
                Message = "用户名或密码错误"
            };
        }

        repository = scope.GetConnectionScope("honghu");

        var user = await repository.Queryable<UserInfo>().FirstAsync(x => x.Account == input.Account && x.Enabled == 1);

        // 判断管理员账号密码
        if (adminFlag && password != user.Password)
        {
            return new()
            {
                Access_token = null,
                Refresh_token = null,
                Code = RocCode.FailMessage,
                Message = "用户名或密码错误"
            };
        }
        // 如果用户不存在则创建新用户，此用户用于分配角色权限
        if (user == null)
        {
            // 获取默认用户权限
            var initUser = await repository.Queryable<UserInfo>().Where(x => x.Account == "init_user").FirstAsync();

            // 创建新用户
            oaUser.Id = StringUtil.Guid();
            oaUser.CreateDate = DateTime.Now;
            oaUser.CreateUserId = "sytuser";
            oaUser.CreateUserName = "sysuser";
            oaUser.Enabled = 1;
            oaUser.RoleIds = initUser.RoleIds;
            oaUser.LastPasswordChangeTime = DateTime.Now;
            var code = await repository.Insertable<UserInfo>(oaUser).ExecuteCommandAsync();
            if (code == 0)
                return new()
                {
                    Access_token = null,
                    Refresh_token = null,
                    Code = RocCode.FailMessage,
                    Message = "oa账户同步失败"
                };
            user = oaUser;
        }
        List<Claim> claims = new();
        claims.Add(new Claim(RocClaimTypes.Account, user.Account));
        claims.Add(new Claim(RocClaimTypes.UserId, user.Id));
        claims.Add(new Claim("Name", user.Name));
        claims.Add(new Claim("Email", user.Email));
        claims.Add(new Claim(RocClaimTypes.SystemFlag, user.SystemFlag));
        claims.Add(new Claim(RocClaimTypes.RoleId, user.RoleIds));
        claims.Add(new Claim(RocClaimTypes.CompanyId, user.CompanyIds));
        claims.Add(new Claim(RocClaimTypes.DepartmentId, user.DepartmentIds));
        claims.Add(new Claim(RocClaimTypes.HeadIcon, user.HeadIcon));

        var token = RocSupportToken.CreateToken(claims,
                                   DateTime.UtcNow.AddSeconds(settings.AccessTokenExpire),
                                   settings.Secret,
                                   settings.Issuer,
                                   settings.Audience,
                                   DateTime.UtcNow);

        return new()
        {
            Access_token = token,
            Refresh_token = token,
            Code = 200
        };
    }
}
