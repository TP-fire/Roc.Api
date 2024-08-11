using Roc.Build;
using Roc.Const;
using Single.Entities.Entity;

namespace Single.Services.IService;

public interface IFileService : RocInterface
{
    Task<RocMessage> upExcel(RocFileInfo fileInfo);
}
