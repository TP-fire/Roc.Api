using Roc.Build;
using Roc.Models;
using Single.Entities.Entity;

namespace Single.Services.IService;

public interface IFileManageService : RocInterface
{
    Task<RocPage<IEnumerable<AlarmReportLogs>>> getPageList(RocPage<AlarmReportLogs> info);
}
