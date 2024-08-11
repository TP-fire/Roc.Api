
using Microsoft.AspNetCore.Components.Routing;
using Roc.Build;
using Roc.Models;
using Single.Entities.Entity;

namespace Bi.Services.IService;

public interface IDataItemDetailService : RocInterface
{
    Task<int> insertTree(DataItem input ,RocUser rocUser);
    Task<int> deleteTree(DataItem input, RocUser rocUser);
    Task<int> modifyTree(DataItem input, RocUser rocUser);
    Task<int> insert(DataItemDetail input, RocUser rocUser);
    Task<int> delete(DataItemDetail input, RocUser rocUser);
    Task<int> modify(DataItemDetail input, RocUser rocUser);
    Task<IEnumerable<DataItemTree>> getDataDictTree();
    Task<IEnumerable<DataItemDetail>> GetListAsync(DataItemDetail input);
    Task<RocPage<IEnumerable<DataItemDetail>>> getPagelist(RocPage<DataItemDetail> inputs);
    
}
