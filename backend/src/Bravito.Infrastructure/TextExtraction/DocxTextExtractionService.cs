using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Knowledge.Interfaces;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Bravito.Infrastructure.TextExtraction;

public class DocxTextExtractionService : ITextExtractionService
{
    public Task<string> ExtractTextAsync(string filePath, string mimeType, CancellationToken cancellationToken = default)
    {
        var sb = new StringBuilder();

        using (var wordDocument = WordprocessingDocument.Open(filePath, false))
        {
            var body = wordDocument.MainDocumentPart?.Document.Body;
            if (body != null)
            {
                foreach (var text in body.Descendants<Text>())
                {
                    sb.Append(text.Text).Append(" ");
                }
            }
        }

        return Task.FromResult(sb.ToString());
    }
}
