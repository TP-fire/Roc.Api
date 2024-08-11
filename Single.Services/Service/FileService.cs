using Roc.Const;
using Roc.utils;
using Single.Entities.Entity;
using Single.Services.IService;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Single.Services.Service;

public class FileService : IFileService
{
    public async Task<RocMessage> upExcel(RocFileInfo fileInfo)
    {
        string filePath = PathUtil.GetAbsolutePath("excel", fileInfo.File.FileName);
        return await FileUtil.WriteToDisk(fileInfo.File, filePath);
    }

}
