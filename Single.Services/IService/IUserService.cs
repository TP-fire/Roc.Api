using Roc.Build;
using Roc.Models;
using Single.Entities.Entity;

namespace Single.Services.IService;

public interface IUserService : RocInterface
{
    /// <summary>
    ///  根据Token 获取UserInfo实体类
    /// </summary>
    /// <param name="rocUser"></param>
    /// <returns></returns>
    Task<UserInfo> GetEntityAsync(RocUser rocUser);
    /// <summary>
    /// 根据名称获取图片
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    Task<(string fileName, byte[] datas)> GetPictureAsync(string fileName);
}
