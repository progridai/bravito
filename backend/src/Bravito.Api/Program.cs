using Bravito.Api.Configuration;
using Bravito.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Keycloak Auth
var keycloakOptions = builder.Configuration.GetSection("Keycloak").Get<KeycloakOptions>();
builder.Services.Configure<KeycloakOptions>(builder.Configuration.GetSection("Keycloak"));

// Configure N8n integration
builder.Services.Configure<Bravito.Infrastructure.Integrations.N8n.Options.N8nOptions>(builder.Configuration.GetSection("N8n"));
builder.Services.AddHttpClient<Bravito.Application.Chat.Interfaces.IAssistenteChatService, Bravito.Infrastructure.Integrations.N8n.N8nAssistenteChatService>();

// Configure PostgreSQL Database
builder.Services.AddDbContext<Bravito.Infrastructure.Data.BravitoDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
});

// Configure Repositories
builder.Services.AddScoped<Bravito.Application.Chat.Interfaces.IConversaRepository, Bravito.Infrastructure.Data.Repositories.ConversaRepository>();

// Configure Access Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Bravito.Application.Acesso.Interfaces.IUsuarioAtualService, Bravito.Infrastructure.Acesso.Services.UsuarioAtualService>();
builder.Services.AddScoped<Bravito.Application.Acesso.Interfaces.IUsuarioAplicacaoService, Bravito.Infrastructure.Acesso.Services.UsuarioAplicacaoService>();
builder.Services.AddScoped<Bravito.Application.Acesso.Interfaces.IAutorizacaoAplicacaoService, Bravito.Infrastructure.Acesso.Services.AutorizacaoAplicacaoService>();

builder.Services.Configure<Bravito.Infrastructure.Integrations.Keycloak.Options.KeycloakAdminOptions>(builder.Configuration.GetSection("KeycloakAdmin"));
builder.Services.AddHttpClient<Bravito.Application.Acesso.Interfaces.IKeycloakAdminService, Bravito.Infrastructure.Integrations.Keycloak.KeycloakAdminService>();
builder.Services.AddScoped<Bravito.Application.Acesso.Interfaces.IUsuariosAdminService, Bravito.Infrastructure.Acesso.Services.UsuariosAdminService>();

// Configure Knowledge Base Options
builder.Services.Configure<Bravito.Infrastructure.Knowledge.Options.KnowledgeOptions>(options => 
{
    builder.Configuration.GetSection("KnowledgeBase").Bind(options);
    builder.Configuration.Bind(options); // Fallback to root for backward compatibility
});

builder.Services.Configure<Bravito.Infrastructure.Knowledge.Options.GeminiOptions>(options =>
{
    builder.Configuration.GetSection("Gemini").Bind(options);
    builder.Configuration.Bind(options); // Fallback to root for backward compatibility
    
    var envKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
    if (!string.IsNullOrEmpty(envKey))
    {
        options.ApiKey = envKey;
    }

    var envModel = Environment.GetEnvironmentVariable("KNOWLEDGE_EMBEDDING_MODEL");
    if (!string.IsNullOrEmpty(envModel))
    {
        options.EmbeddingModel = envModel;
    }
});

// Configure Knowledge Base Database
builder.Services.AddDbContext<Bravito.Infrastructure.Data.KnowledgeDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("KnowledgeDb"));
});

// Configure Knowledge Base Services
builder.Services.AddScoped<Bravito.Application.Knowledge.Interfaces.IKnowledgeDocumentRepository, Bravito.Infrastructure.Data.Repositories.KnowledgeDocumentRepository>();
builder.Services.AddScoped<Bravito.Application.Knowledge.Interfaces.IVectorDocumentRepository, Bravito.Infrastructure.Data.Repositories.PgVectorDocumentRepository>();
builder.Services.AddScoped<Bravito.Application.Knowledge.Interfaces.IFileStorageService, Bravito.Infrastructure.Storage.LocalFileStorageService>();
builder.Services.AddScoped<Bravito.Application.Knowledge.Interfaces.ITextChunkingService, Bravito.Infrastructure.Chunking.SimpleTextChunkingService>();
builder.Services.AddScoped<Bravito.Application.Knowledge.Interfaces.IKnowledgeDocumentService, Bravito.Application.Knowledge.Services.KnowledgeDocumentService>();

builder.Services.AddHttpClient<Bravito.Application.Knowledge.Interfaces.IEmbeddingService, Bravito.Infrastructure.Embeddings.GeminiEmbeddingService>();

builder.Services.AddScoped<Bravito.Application.Knowledge.Interfaces.ITextExtractionService, Bravito.Infrastructure.TextExtraction.PdfTextExtractionService>();
builder.Services.AddScoped<Bravito.Application.Knowledge.Interfaces.ITextExtractionService, Bravito.Infrastructure.TextExtraction.DocxTextExtractionService>();
builder.Services.AddScoped<Bravito.Application.Knowledge.Interfaces.ITextExtractionService, Bravito.Infrastructure.TextExtraction.TxtTextExtractionService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = keycloakOptions?.Authority;
        options.Audience = keycloakOptions?.Audience;
        options.RequireHttpsMetadata = false; // Como é tráfego interno no Docker, não exige HTTPS para o metadado
        
        // Bypass do bloqueio de DNS/NAT Loopback do Easypanel usando a rede interna do Docker
        options.MetadataAddress = "http://keycloak:8080/realms/bravito/.well-known/openid-configuration";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = keycloakOptions?.Authority, // Exige que o token venha do domínio oficial (auth.bravida.com.br)
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = keycloakOptions?.Audience
        };
    });

builder.Services.AddAuthorization();

// Configure Swagger with JWT Bearer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bravito API", Version = "v1" });
    
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Insira 'Bearer {seu_token_jwt}'",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    
    c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Apply database migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Bravito.Infrastructure.Data.BravitoDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalExceptionHandler>();

// Enable Swagger in all environments
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAll");

// app.UseHttpsRedirection(); // Removido pois o Easypanel (Traefik) já cuida do HTTPS e redirecionamentos

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
