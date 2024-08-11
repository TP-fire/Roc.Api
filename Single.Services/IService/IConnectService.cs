using Roc.Build;
using Roc.Models;
using Single.Entities.Entity;

namespace Single.Services.IService;

public interface IConnectService : RocInterface
{
    Task<RocToken> getToken(UserInfo input);
}
