# Usa a imagem oficial do ASP.NET Core 8.0 como base para rodar o app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Usa a imagem do SDK para compilar o código
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia os arquivos de projeto (csproj) de cada camada da Clean Architecture
COPY ["src/Bravito.Api/Bravito.Api.csproj", "src/Bravito.Api/"]
COPY ["src/Bravito.Application/Bravito.Application.csproj", "src/Bravito.Application/"]
COPY ["src/Bravito.Domain/Bravito.Domain.csproj", "src/Bravito.Domain/"]
COPY ["src/Bravito.Infrastructure/Bravito.Infrastructure.csproj", "src/Bravito.Infrastructure/"]
COPY ["src/Bravito.Shared/Bravito.Shared.csproj", "src/Bravito.Shared/"]

# Restaura as dependências
RUN dotnet restore "./src/Bravito.Api/Bravito.Api.csproj"

# Copia todo o código-fonte
COPY . .

# Compila o projeto
WORKDIR "/src/src/Bravito.Api"
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
