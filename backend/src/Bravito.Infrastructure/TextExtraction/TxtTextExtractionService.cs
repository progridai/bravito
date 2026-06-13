using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Knowledge.Interfaces;

namespace Bravito.Infrastructure.TextExtraction;

public class TxtTextExtractionService : ITextExtractionService
{
    public async Task<string> ExtractTextAsync(string filePath, string mimeType, CancellationToken cancellationToken = default)
    {
        return await File.ReadAllTextAsync(filePath, cancellationToken);
    }
}
