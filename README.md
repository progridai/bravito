# 🦉 Bravito

O **Bravito** é uma aplicação corporativa multiplataforma de alta performance, desenvolvida em **Flutter** e **ASP.NET Core**. O sistema é projetado sob rígidos padrões de segurança, autenticação centralizada, controle fino de acesso e integração nativa com assistentes de Inteligência Artificial via **n8n**, com visão de escalabilidade futura como extensão integrada de um sistema ERP..

---

## 🚀 Objetivo da Fase 1
O foco da primeira fase é a consolidação de uma fundação extremamente segura, performática e modular, englobando os seguintes pilares:

*   **🔒 Login com o Keycloak**: Autenticação centralizada e segura no padrão OpenID Connect (OIDC).
*   **📱 Aplicação Flutter (Mobile/Web)**: Interface responsiva desenvolvida em Flutter com arquitetura de ponta baseada em Material 3.
    *   **Autenticação:** Integrado via `flutter_appauth` e interceptores do `Dio`.
    *   **Roteamento:** Gerenciado via `go_router` com proteção de rotas baseada no AuthState.
    *   **Segurança da Conta:** Alterações sensíveis, como alteração de senha, são redirecionadas à tela oficial do Keycloak.
*   **⚙️ API Backend em ASP.NET Core**: Gateway seguro e centralizador com implementação de Clean Architecture.
    *   **n8n Assistant Webhook:** Integração segura (Server-to-Server) com o assistente n8n via API backend ASP.NET Core (`POST /api/chat/enviar`). A API atua como um proxy de segurança, populando e blindando o contexto do usuário autenticado no Keycloak antes de enviar ao Webhook, de modo que a URL do n8n e metadados confidenciais nunca cheguem ao Frontend.
*   **🗄️ Banco PostgreSQL**: Armazenamento relacional robusto e versionado via migrations.
*   **🛡️ Autenticação Segura API**: API ASP.NET Core configurada para validar JWT Bearer garantindo restrição severa de endpoints.
*   **🔐 Autorização por Recursos**: Sistema de controle de acesso granular armazenado no banco local. A API verifica permissões exclusivas da aplicação usando atributos como `[RequerRecurso("chat.acessar")]`.
*   **👥 Gestão Administrativa de Usuários**: Endpoints internos robustos com CRUD sincronizado automaticamente entre PostgreSQL e Keycloak Admin API, sem salvar senhas locais. Há também telas Flutter totalmente responsivas e seguras que integram com estes endpoints.
*   **📱 Proteção Visual e UX (Flutter)**: A interface ajusta o menu, oculta botões e redireciona rotas protegidas (Acesso Negado) lendo as permissões vindas do login.
*   **💬 Chat com Assistente no n8n**: Canal de chat integrado à inteligência artificial por meio de webhooks seguros.
*   **📝 Auditoria & Logs Estruturados**: Logs estruturados em tempo real (Serilog) e trilha básica de auditoria de dados.
*   **🌱 Estrutura Escalável**: Código-fonte desacoplado e modelagem preparada para expansão contínua.

---

## 🏗️ Arquitetura Principal

O fluxo de comunicação e as fronteiras do ecossistema Bravito seguem o modelo abaixo:

```
[ Flutter Mobile/Web ]
         │
         ▼ (HTTPS / REST)
[ ASP.NET Core API (Gateway) ]
         │
         ├───► [ Keycloak ] (Autenticação JWT)
         │
         ├───► [ PostgreSQL ] (Persistência)
         │
         └───► [ n8n ] (Webhook Interno da API)
```

---

## 📁 Estrutura de Pastas
O projeto está organizado da seguinte forma para garantir o desacoplamento e a consistência técnica:

- **`backend/`**: Código-fonte do servidor ASP.NET Core (C#) em Clean Architecture.
- **`frontend/`**: Código-fonte do aplicativo Flutter (Dart) com gerência de estado em Riverpod.
- **`docker/`**: Arquivos de configuração de containers para deploys locais e em nuvem.
- **`docs/`**: Documentação detalhada dos subsistemas do projeto.
- **`AGENTS.md`**: Regras obrigatórias absolutas e diretrizes de desenvolvimento para IAs e desenvolvedores.

---

## 📚 Documentos de Apoio (/docs)
Para obter detalhes de implementação de cada área do projeto, consulte a documentação oficial interna:

1. **[Visão Geral do Projeto](docs/01-visao-geral.md)**: Detalhamento do negócio e regras gerais.
2. **[Arquitetura do Sistema](docs/02-arquitetura.md)**: Organização de projetos, injeção de dependência e fluxo de dados.
3. **[Padrão Visual e Design System](docs/03-padrao-visual.md)**: Guia completo sobre a paleta de cores oficial, tipografia e mascotaria.
4. **[Políticas de Segurança](docs/04-seguranca.md)**: Autenticação Keycloak, headers e proteção contra vazamento de credenciais.
5. **[Frontend (Flutter)](docs/05-frontend-flutter.md)**: Padrão Feature-First, Riverpod e GoRouter.
6. **[Backend (API)](docs/06-backend-api.md)**: Configurações de rotas, DTOs e middleware de erro no ASP.NET Core.
7. **[Banco de Dados](docs/07-banco-dados.md)**: Esquemas de tabelas e versionamento do PostgreSQL.
8. **[Integração n8n](docs/08-integracao-n8n.md)**: Modelagem e autenticação dos payloads de webhook.
9. **[Roadmap](docs/09-roadmap.md)**: Acompanhamento de entregas e fases futuras.
10. **[Prompts de Assistência](docs/10-prompts.md)**: Prompts padrão para aceleração de desenvolvimento.
11. **[Identidade Visual Oficial (Bravito)](docs/25-identidade-visual-oficial-bravito.md)**: Regras oficiais de UI e design system do mascote Bravito.

> **Nota:** A identidade visual oficial do Bravito foi definida. A aplicação utiliza o padrão corporativo da Bravida (Dourado, Branco, Cinza Claro, Preto Suave) substituindo paletas provisórias. Mais detalhes em `docs/03-padrao-visual.md` e `docs/25-identidade-visual-oficial-bravito.md`.

Stack
Frontend
Flutter
Dart
Riverpod
GoRouter
Dio
Material 3
Backend
ASP.NET Core
C#
Clean Architecture
PostgreSQL
Entity Framework Core
Keycloak
JWT Bearer
Serilog
Swagger/OpenAPI
Documentação

A documentação técnica está na pasta docs/.

Ordem recomendada de leitura:

docs/01-visao-geral.md
docs/02-arquitetura.md
docs/03-padrao-visual.md
docs/04-seguranca.md
docs/05-frontend-flutter.md
docs/06-backend-api.md
docs/07-banco-dados.md
docs/08-integracao-n8n.md
docs/09-roadmap.md
docs/10-prompts.md
Regra principal

Nenhuma funcionalidade deve ser implementada fora das regras definidas em AGENTS.md e nos documentos da pasta docs/.

---

## 🐳 Infraestrutura Local (Docker)

> O Keycloak é configurado automaticamente com o realm **bravito** contendo as roles e clients necessários para desenvolvimento local.

Para subir o banco de dados (PostgreSQL) e o servidor de autenticação (Keycloak) localmente para desenvolvimento:

1. Acesse o diretório docker:
   ```bash
   cd docker
   ```
2. Crie o seu arquivo de variáveis de ambiente com base no exemplo:
   ```bash
   cp .env.example .env
   ```
3. Inicie os containers:
   ```bash
   docker compose up -d
   ```

**Para parar a infraestrutura:**
```bash
docker compose down
```

**Acessos Locais:**
- **Keycloak**: [http://localhost:8080](http://localhost:8080)
- **PostgreSQL**: `localhost:5432`

> **Nota:** As credenciais contidas no `.env.example` são destinadas **apenas para desenvolvimento local**. Jamais versione um arquivo `.env` ou use senhas fracas em ambientes de produção.

---

## 📱 Executando o Frontend (Flutter)

O projeto Flutter está localizado na pasta `frontend/bravito_app`. 
Para executar o aplicativo em ambiente de desenvolvimento, siga os passos abaixo:

1. Acesse o diretório do frontend:
   ```bash
   cd frontend/bravito_app
   ```
2. Instale as dependências:
   ```bash
   flutter pub get
   ```
3. Execute o projeto (selecione o dispositivo, como Web, Android, iOS, ou Windows):
   ```bash
   flutter run
   ```

> **Nota:** O aplicativo agora possui integração real de autenticação com Keycloak usando **PKCE (Authorization Code Flow)**. A comunicação com o Backend ASP.NET Core (`/api/auth/me`) já está operacional e protegida por tokens JWT armazenados com segurança.

## 💬 Testando o Chat com o Assistente n8n

Para testar o fluxo completo de mensagens (Flutter -> Backend -> n8n -> Backend -> Flutter):

1. Certifique-se de que a **API ASP.NET Core** está rodando (`dotnet run` dentro de `backend/src/Bravito.Api`).
2. Abra o Flutter na Web (Google Chrome):
   ```bash
   cd frontend/bravito_app
   flutter run -d chrome
   ```
3. Faça Login usando as credenciais do Keycloak (`dev.admin` e `Admin@123456`).
4. Ao ser redirecionado para a tela inicial, clique em "Abrir Chat" e digite uma mensagem. A API encaminhará a mensagem autenticada para o Webhook do n8n de forma segura e invisível para o Client.
5. Em alguns instantes o balão do assistente deverá aparecer com a resposta do webhook!