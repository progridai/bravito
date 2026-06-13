using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Knowledge.Interfaces;
using Bravito.Infrastructure.Knowledge.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Bravito.Infrastructure.Data.Repositories;

public class PgVectorDocumentRepository : IVectorDocumentRepository
{
    private readonly KnowledgeDbContext _dbContext;
    private readonly KnowledgeOptions _options;

    public PgVectorDocumentRepository(KnowledgeDbContext dbContext, IOptions<KnowledgeOptions> options)
    {
        _dbContext = dbContext;
        _options = options.Value;
    }

    public async Task<bool> CheckDatabaseConnectionAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Database.CanConnectAsync(cancellationToken);
    }

    public async Task<bool> CheckTableExistsAsync(string tableName, CancellationToken cancellationToken = default)
    {
        var sql = @"
            SELECT EXISTS (
                SELECT FROM information_schema.tables 
                WHERE  table_schema = @schema
                AND    table_name   = @table
            );";

        var schemaParam = new NpgsqlParameter("@schema", _options.DbSchema);
        var tableParam = new NpgsqlParameter("@table", tableName);

        var connection = _dbContext.Database.GetDbConnection();
        var wasClosed = connection.State == System.Data.ConnectionState.Closed;
        if (wasClosed) await connection.OpenAsync(cancellationToken);

        try
        {
            using var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add(schemaParam);
            command.Parameters.Add(tableParam);

            var result = await command.ExecuteScalarAsync(cancellationToken);
            return result != null && (bool)result;
        }
        finally
        {
            if (wasClosed) await connection.CloseAsync();
        }
    }

    public async Task InsertChunkAsync(Guid id, string text, string metadataJson, float[] embedding, CancellationToken cancellationToken = default)
    {
        var embeddingString = "[" + string.Join(",", embedding) + "]";
        var tableName = $"{_options.DbSchema}.{_options.VectorTable}";

        var sql = $@"
            INSERT INTO {tableName} (id, text, metadata, embedding)
            VALUES (@id, @text, @metadata::jsonb, @embedding::vector)";

        var idParam = new NpgsqlParameter("@id", id);
        var textParam = new NpgsqlParameter("@text", text);
        var metadataParam = new NpgsqlParameter("@metadata", metadataJson);
        var embeddingParam = new NpgsqlParameter("@embedding", embeddingString);

        await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { idParam, textParam, metadataParam, embeddingParam }, cancellationToken);
    }

    public async Task<int> DeleteChunksByDocumentIdAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        var tableName = $"{_options.DbSchema}.{_options.VectorTable}";
        
        var sql = $@"
            DELETE FROM {tableName}
            WHERE metadata->>'document_id' = @documentId";

        var idParam = new NpgsqlParameter("@documentId", documentId.ToString());

        return await _dbContext.Database.ExecuteSqlRawAsync(sql, new[] { idParam }, cancellationToken);
    }
}
