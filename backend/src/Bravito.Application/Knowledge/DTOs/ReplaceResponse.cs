using System;

namespace Bravito.Application.Knowledge.DTOs;

public class ReplaceResponse
{
    public bool Success { get; set; }
    public Guid Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public int DeletedOldChunks { get; set; }
    public int NewChunks { get; set; }
}
