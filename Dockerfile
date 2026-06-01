# Usa a imagem oficial do ASP.NET Core 8.0 como base para rodar o app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Usa a imagem do SDK para compilar o código
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia os arquivos de projeto (csproj) ajustando para a pasta backend/
COPY ["backend/src/Bravito.Api/Bravito.Api.csproj", "backend/src/Bravito.Api/"]
COPY ["backend/src/Bravito.Application/Bravito.Application.csproj", "backend/src/Bravito.Application/"]
COPY ["backend/src/Bravito.Domain/Bravito.Domain.csproj", "backend/src/Bravito.Domain/"]
COPY ["backend/src/Bravito.Infrastructure/Bravito.Infrastructure.csproj", "backend/src/Bravito.Infrastructure/"]
COPY ["backend/src/Bravito.Shared/Bravito.Shared.csproj", "backend/src/Bravito.Shared/"]

# Restaura as dependências
RUN dotnet restore "./backend/src/Bravito.Api/Bravito.Api.csproj"

# Copia todo o restante do código da pasta backend/
COPY backend/ backend/

# Compila o projeto
WORKDIR "/src/backend/src/Bravito.Api"
RUN dotnet build "./Bravito.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publica a aplicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Bravito.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Gera a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bravito.Api.dll"]
