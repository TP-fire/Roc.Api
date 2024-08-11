using Roc.Models;
using Single.Entities.Entity;
using Single.Services.IService;
using SqlSugar;

namespace Single.Services.Service;

internal class UserService : IUserService
{
    /// <summary>
    /// 数据库链接
    /// </summary>
    private SqlSugarScopeProvider repository;

    public UserService(ISqlSugarClient _sqlSugarClient)
    {
        repository = (_sqlSugarClient as SqlSugarScope).GetConnectionScope("single");
    }

    public async Task<UserInfo> GetEntityAsync(RocUser rocUser)
    {
        UserInfo user = await repository.Queryable<UserInfo>().FirstAsync(x => x.Id == rocUser.Id);
        // 这里查询用户的菜单权限
        return user;
    }

    public async Task<(string fileName, byte[] datas)> GetPictureAsync(string fileName)
    {
        string rootPath = AppContext.BaseDirectory;
        rootPath = Path.Combine(rootPath, "picture", fileName);
        byte[] fileBytes = File.ReadAllBytes(rootPath);
        return (fileName, fileBytes);
    }
}
