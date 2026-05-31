# Infraestrutura Local com Docker

Este diretório contém a configuração necessária para rodar a infraestrutura local de apoio do Projeto Bravito, incluindo **PostgreSQL** e **Keycloak**.

## 🛠 Pré-requisitos
- Docker
- Docker Compose

## 🚀 Como Executar

1. Crie o arquivo de configuração local:
   ```bash
   cp .env.example .env
   ```
2. Inicie os containers em segundo plano:
   ```bash
   docker compose up -d
   ```

## 🛑 Como Parar
```bash
docker compose down
```
Se desejar apagar o banco de dados (os volumes), use:
```bash
docker compose down -v
```

## 🌐 Acessos Locais

- **Keycloak**: [http://localhost:8080](http://localhost:8080)
- **PostgreSQL**: `localhost:5432`

> [!WARNING]
> **Aviso de Segurança**
> As credenciais padrão do `.env.example` são estritamente para **desenvolvimento local**. Nunca as utilize em produção ou faça commit do seu arquivo `.env` definitivo.
