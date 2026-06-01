using Bravito.Api.Configuration;
using Bravito.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Keycloak Auth
var keycloakOptions = builder.Configuration.GetSection("Keycloak").Get<KeycloakOptions>();
builder.Services.Configure<KeycloakOptions>(builder.Configuration.GetSection("Keycloak"));

// Configure N8n integration
builder.Services.Configure<Bravito.Infrastructure.Integrations.N8n.Options.N8nOptions>(builder.Configuration.GetSection("N8n"));
builder.Services.AddHttpClient<Bravito.Application.Chat.Interfaces.IAssistenteChatService, Bravito.Infrastructure.Integrations.N8n.N8nAssistenteChatService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = keycloakOptions?.Authority;
        options.Audience = keycloakOptions?.Audience;
        options.RequireHttpsMetadata = keycloakOptions?.RequireHttpsMetadata ?? false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
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
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins(
                "http://localhost",
                "http://localhost:3000",
                "http://localhost:5000",
                "http://localhost:8080",
                "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowLocalhost");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
