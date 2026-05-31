# 02 — Arquitetura do Projeto

---

## 🏗️ Desenho Arquitetural
O Projeto Bravito segue um fluxo linear e centralizado de comunicação para garantir a segurança dos dados e o isolamento de integrações externas:

```
      [ Frontend: Flutter Mobile/Web ]
                     │
                     ▼ (HTTPS / REST)
       ┌───────────────────────────┐
       │     API ASP.NET Core      │ ◄───► [ Auth: Keycloak ]
       └─────────────┬─────────────┘
                     │
        ┌────────────┴────────────┐
        ▼                         ▼
[ Banco: PostgreSQL ]    [ n8n Workflow Engine ]
```

---

## 🚨 Regra Principal (Fronteiras e Segurança)

> [!WARNING]
> **RESTRIÇÃO ABSOLUTA DE ACESSO DO FRONTEND:**
> O aplicativo Flutter **nunca** deve interagir diretamente com:
> 1. O banco de dados **PostgreSQL**.
> 2. Os webhooks de orquestração do **n8n**.
> 3. Quaisquer **credenciais internas** ou segredos de API.
> 4. Serviços de infraestrutura sensíveis.
> 5. Dados do **ERP** corporativo sem passar pela validação da API backend.
> 
> *Toda regra de segurança, validação de permissão, registro de auditoria e integração com serviços externos reside e é executada exclusivamente na API backend.*

---

## 👥 Responsabilidades dos Componentes

### 📱 1. Responsabilidade do Flutter (Apresentação)
O frontend em Flutter é responsável única e exclusivamente pela interface visual e experiência do usuário:
*   Renderização da interface gráfica (UI) baseada em componentes Material 3.
*   Tela e fluxos visuais de login e recuperação.
*   Controle de sessão local e estados de rotas (GoRouter).
*   Tela de chat reativa com estados de digitação e carregamento.
*   Consumo seguro e padronizado da API backend (via cliente HTTP Dio).
*   Armazenamento local criptografado de tokens de segurança (Flutter Secure Storage).

> [!CAUTION]
> **O Flutter não deve conter:**
> - Regras sensíveis de validação de permissão.
> - Credenciais, senhas ou tokens fixos em código.
> - URLs diretas de webhooks do n8n.
> - Conexões ADO/ORM com o PostgreSQL.
> - Lógicas críticas de regras de negócio.

---

### ⚙️ 2. Responsabilidade da API (Backend Gateway)
A API ASP.NET Core atua como o gatekeeper de toda a aplicação:
*   Validação estrita de tokens JWT (autenticação).
*   Validação de regras de acesso do usuário (autorização baseada em Roles/Claims).
*   Intermediação e integração segura com o **Keycloak**.
*   Persistência e manipulação segura de dados no **PostgreSQL** via EF Core.
*   Centralização e disparo autenticado de webhooks para o **n8n**.
*   Registro e formatação estruturada de logs técnicos e auditoria.
*   Configuração e execução de regras de negócio e controle multiempresa.

---

### 🔑 3. Responsabilidade do Keycloak (Identidade)
O Keycloak é o motor central de autenticação e governança de acesso:
*   Processamento visual e lógico de autenticação de usuários.
*   Emissão e assinatura de tokens JWT e Refresh Tokens de segurança.
*   Gerenciamento global de perfis, grupos, Roles e Claims de permissão.
*   Provisionamento centralizado e unificado de identidades.
*   Preparação técnica para suporte futuro a **SSO (Single Sign-On)** e **MFA/2FA** (Múltiplo Fator).

---

### 🗄️ 4. Responsabilidade do PostgreSQL (Armazenamento Relacional)
O PostgreSQL gerencia o armazenamento persistente e versionado dos dados proprietários do ecossistema:
*   Registros de empresas inquilinas (Empresas).
*   Perfis estendidos de usuários associados ao Keycloak.
*   Histórico estruturado de conversas (ConversasChat).
*   Histórico de mensagens de chat (MensagensChat) e metadados.
*   Trilhas de auditoria das operações executadas (LogsAuditoria).
*   Configurações parametrizadas de integrações da plataforma.

---

### 🤖 5. Responsabilidade do n8n (Motor de IA e Automações)
O n8n opera de maneira assíncrona nos bastidores como orquestrador do assistente inteligente:
*   Processamento lógico e enriquecimento das mensagens enviadas pelos usuários.
*   Orquestração do assistente de inteligência artificial (integrações com LLMs).
*   Fluxos de automação de disparo de notificações e conexões com APIs externas (ex: WhatsApp, e-mails).
*   *Nota: O n8n opera de forma interna, protegido pela rede privada da API, sem exposição pública direta ao frontend.*
