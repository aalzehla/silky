using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileHostDemo.File;

public class FileAppService : IFileAppService
{
    public Task PostFile(IFormFile file)
    {
        throw new System.NotImplementedException();
    }

    public Task PostFiles(IFormFileCollection files)
    {
        throw new System.NotImplementedException();
    }

    public Task PostFormWithFile(FormWithFile formWithFile)
    {
        throw new System.NotImplementedException();
    }

    public async Task<FileResult> Download()
    {
        var bytes = System.IO.File.ReadAllBytes("./Contents/test.xlsx").ToArray();
        return new FileContentResult(bytes, "application/octet-stream")
        {
            FileDownloadName = "test文件.xlsx"
        };
    }
}