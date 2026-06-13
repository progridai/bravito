using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Bravito.Application.Knowledge.Interfaces;

namespace Bravito.Infrastructure.Chunking;

public class SimpleTextChunkingService : ITextChunkingService
{
    private const int ChunkSize = 1000;
    private const int Overlap = 200;

    public List<string> ChunkText(string text)
    {
        var chunks = new List<string>();
        if (string.IsNullOrWhiteSpace(text)) return chunks;

        // Limpeza básica
        text = Regex.Replace(text, @"\s+", " ").Trim();

        if (text.Length <= ChunkSize)
        {
            chunks.Add(text);
            return chunks;
        }

        int currentIndex = 0;
        while (currentIndex < text.Length)
        {
            int remaining = text.Length - currentIndex;
            int take = Math.Min(ChunkSize, remaining);

            if (currentIndex + take < text.Length)
            {
                // Tenta encontrar um espaço para não quebrar a palavra
                int lastSpace = text.LastIndexOf(' ', currentIndex + take - 1, take);
                if (lastSpace > currentIndex)
                {
                    take = lastSpace - currentIndex;
                }
            }

            var chunk = text.Substring(currentIndex, take).Trim();
            if (chunk.Length > 50 || (currentIndex == 0 && text.Length <= 50)) // ignora chunks muito pequenos, exceto se for o único
            {
                chunks.Add(chunk);
            }

            currentIndex += take - Overlap;
            
            // Garantir progresso
            if (take - Overlap <= 0)
            {
                currentIndex += ChunkSize;
            }
        }

        return chunks;
    }
}
