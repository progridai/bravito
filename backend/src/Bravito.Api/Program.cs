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
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Repositories
builder.Services.AddScoped<Bravito.Application.Chat.Interfaces.IConversaRepository, Bravito.Infrastructure.Data.Repositories.ConversaRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = keycloakOptions?.Authority;
        options.Audience = keycloakOptions?.Audience;
        options.RequireHttpsMetadata = keycloakOptions?.RequireHttpsMetadata ?? false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false, // Desativado para tolerar instâncias do Keycloak sem o Audience Mapper configurado na VPS
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

// app.UseHttpsRedirection(); // Removido pois o Easypanel (Traefik) já cuida do HTTPS e redirecionamentos

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
