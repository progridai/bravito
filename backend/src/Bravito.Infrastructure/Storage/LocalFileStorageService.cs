using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Knowledge.Interfaces;
using Bravito.Infrastructure.Knowledge.Options;
using Microsoft.Extensions.Options;

namespace Bravito.Infrastructure.Storage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly KnowledgeOptions _options;

    public LocalFileStorageService(IOptions<KnowledgeOptions> options)
    {
        _options = options.Value;
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string documentId, CancellationToken cancellationToken = default)
    {
        var directory = _options.StoragePath;
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var sanitizedFileName = fileName.Replace(" ", "_").Replace("/", "").Replace("\\", "");
        var filePath = Path.Combine(directory, $"{documentId}_{sanitizedFileName}");

        using var fileStreamOut = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await fileStream.CopyToAsync(fileStreamOut, cancellationToken);

        return filePath;
    }

    public Task<bool> FileExistsAsync(string filePath)
    {
        return Task.FromResult(File.Exists(filePath));
    }

    public Task DeleteFileAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        return Task.CompletedTask;
    }

    public bool CheckStoragePathWritable()
    {
        try
        {
            var testFile = Path.Combine(_options.StoragePath, ".write_test");
            if (!Directory.Exists(_options.StoragePath))
            {
                Directory.CreateDirectory(_options.StoragePath);
            }
            File.WriteAllText(testFile, "test");
            File.Delete(testFile);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string GetStoragePath()
    {
        return _options.StoragePath;
    }
}
