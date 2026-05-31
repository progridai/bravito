# 11 — Infraestrutura Local (Docker)

## 🐳 Visão Geral
A infraestrutura local de desenvolvimento do Projeto Bravito é provisionada usando Docker Compose para garantir que todos os desenvolvedores tenham um ambiente isolado, limpo e reproduzível.

Na **Fase 2**, os serviços implementados são o banco de dados e a camada de identidade.

## 📦 Serviços Criados

1. **postgres**: Banco de dados relacional oficial (`postgres:15`).
   - A imagem gerencia tanto o banco da aplicação (`bravito_app`) quanto o banco do Keycloak (`bravito_keycloak`), criados durante a inicialização via `init-user-db.sh`.
   - **Volume Local:** `postgres_data` mapeado para garantir persistência.
   - **Porta:** Mapeada para `5432` localmente.

2. **keycloak**: Gestão de identidade e acessos (`quay.io/keycloak/keycloak:24.0`).
   - Iniciado no modo `start-dev` (próprio para desenvolvimento).
   - Comunica-se com o serviço `postgres` usando a rede `bravito_net`.
   - **Porta:** Mapeada para `8080` localmente.

## ⚙️ Variáveis de Ambiente e `.env`
O diretório `docker/` inclui um arquivo `.env.example` que contém:
- Credenciais e configuração do PostgreSQL.
- Credenciais e configuração do Keycloak.

> [!CAUTION]
> O arquivo `.env` nunca deve ser adicionado ao controle de versão. Há uma regra estrita no `.gitignore` (`.env` e `*.env`) para bloquear isso. Você deve duplicar o `.env.example` localmente.

## 🚀 Guia Rápido

1. Entre na pasta `docker/` do projeto:
   ```bash
   cd docker/
   ```
2. Crie seu `.env`:
   ```bash
   cp .env.example .env
   ```
3. Suba o ambiente:
   ```bash
   docker compose up -d
   ```
4. Verifique os logs se necessário:
   ```bash
   docker compose logs -f
   ```

## 📍 Acessos Locais

- **Keycloak Admin Console**: `http://localhost:8080`
  - Usuário padrão: `admin` (conforme `.env.example`)
  - Senha padrão: `admin`
- **Conexão PostgreSQL**: `localhost:5432`
  - Usuário: `bravito_admin`
  - Senha: `bravito_local_pass`
  - DB Aplicação: `bravito_app`
  - DB Keycloak: `bravito_keycloak`

---

## 🔐 Configuração Inicial Automática do Keycloak
Na inicialização do container (Fase 3), o Keycloak é instruído a importar o arquivo `docker/keycloak/bravito-realm.json`.

**O que é criado automaticamente?**
- **Realm:** `bravito`
- **Clients:** `bravito-flutter` (público para o app) e `bravito-api` (para validação na API).
- **Roles:** `admin`, `gestor`, `operador`, `auditor`.
- **Usuário de Teste:** `dev.admin` / `Admin@123456`

> [!TIP]
> **Como reimportar o realm?**
> Caso precise limpar as alterações e resetar o Keycloak para o estado inicial, pare os containers, remova os volumes (`docker compose down -v`) e suba novamente. O Keycloak fará a importação limpa no banco vazio.
>
> **Cuidados com Dados Locais:**
> O arquivo `bravito-realm.json` não deve conter segredos reais de produção ou chaves de provedores de identidade sensíveis. Ele é voltado exclusivamente para facilitar o Setup de novos desenvolvedores.
