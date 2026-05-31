# 🤖 Regras Obrigatórias do Projeto Bravito (AGENTS.md)

> [!IMPORTANT]
> **DIRETRIZ DE SISTEMA PARA ASSISTENTES DE IA E AGENTES DE CÓDIGO:**
> Este arquivo contém a verdade absoluta sobre o projeto **Bravito**. Qualquer geração de código, sugestão ou modificação arquitetural deve obrigatoriamente validar e seguir as diretrizes abaixo. A violação de qualquer regra absoluta listada aqui resultará em rejeição imediata do código.

---

## 📖 Instrução de Leitura Obrigatória
Antes de implementar qualquer funcionalidade, consulte e leia detalhadamente os arquivos da pasta `docs/`, principalmente:
- [01 - Visão Geral do Projeto](file:///d:/Potter/Rsul%20Automa%C3%A7oes/bravito/docs/01-visao-geral.md)
- [02 - Arquitetura do Sistema](file:///d:/Potter/Rsul%20Automa%C3%A7oes/bravito/docs/02-arquitetura.md)
- [03 - Padrão Visual e Design System](file:///d:/Potter/Rsul%20Automa%C3%A7oes/bravito/docs/03-padrao-visual.md)
- [04 - Políticas de Segurança](file:///d:/Potter/Rsul%20Automa%C3%A7oes/bravito/docs/04-seguranca.md)

---

## 🚨 Regras Absolutas (Sem Exceção)

> [!WARNING]
> **ISOLAMENTO COMPLETO DE DADOS E INTEGRAÇÃO NO FRONTEND:**
> - O frontend Flutter **NUNCA** deve acessar o banco de dados PostgreSQL diretamente.
> - O frontend Flutter **NUNCA** deve chamar webhooks do n8n diretamente.
> - Toda e qualquer comunicação sensível deve passar obrigatoriamente pela **API backend**.

- **Gerenciamento de Identidade**: A autenticação do sistema deve ser feita exclusivamente com **Keycloak**.
- **Padrão de Código do Backend**: O backend deve seguir rigorosamente os princípios de **Clean Architecture** (Domain, Application, Infrastructure, Web API).
- **Modelo de Negócio**: O projeto deve ser planejado e preparado para **multiempresa (multi-tenant)** desde o início.
- **Proteção contra Vazamento de Segredos**: Nenhum segredo, senha, token, client secret ou URL sensível/privada deve ser colocado diretamente no código-fonte. Use variáveis de ambiente (`.env` ou `appsettings.json` parametrizados).
- **Regras de Negócio e Permissões**: Nenhuma regra de negócio crítica, validação sensível ou permissão de acesso deve residir exclusivamente no frontend. O backend é o gatekeeper da validação.
- **Fidelidade Visual**: Toda e qualquer tela/UI construída deve seguir estritamente a identidade visual oficial do Bravito.
- **Consistência de Escopo**: Não implementar módulos, classes ou funcionalidades que estejam fora do escopo solicitado para a fase atual.
- **Governança**: Antes de alterar qualquer detalhe da arquitetura acordada, explique claramente a decisão técnica e aguarde aprovação expressa do usuário.

---

## 🛠️ Checklist de Auto-Verificação para Agentes (Rode antes de gerar código)
Antes de responder ao usuário ou criar um novo arquivo, faça a si mesmo as seguintes perguntas:
1. *Estou fazendo o Flutter se conectar diretamente ao PostgreSQL ou n8n?* **(Deve ser NÃO)**
2. *Essa funcionalidade/serviço está na lista do Escopo da Fase 1?* **(Deve ser SIM)**
3. *Estou criando arquivos na pasta correta (`frontend/` para Flutter/Dart, `backend/` para C#/ASP.NET Core)?* **(Deve ser SIM)**
4. *Estou expondo senhas ou tokens sensíveis no código?* **(Deve ser NÃO)**
5. *O backend que estou gerando respeita a separação de Clean Architecture?* **(Deve ser SIM)**

---

## 📦 Stack Oficial do Projeto

### Frontend
- **Framework**: `Flutter` (Dart)
- **Gerência de Estado**: `Riverpod`
- **Roteamento**: `GoRouter`
- **Cliente HTTP**: `Dio`
- **Armazenamento Seguro**: `Flutter Secure Storage`
- **Design System**: `Material 3`

### Backend
- **Framework Principal**: `ASP.NET Core` (C#)
- **Padrão Arquitetural**: `Clean Architecture`
- **Banco de Dados**: `PostgreSQL`
- **Mapeador ORM**: `Entity Framework Core`
- **Identidade e Acesso**: `Keycloak` com `JWT Bearer Authentication`
- **Logs estruturados**: `Serilog`
- **Documentação de API**: `Swagger / OpenAPI`
- **Containerização**: `Docker`

---

## 🎨 Identidade Visual Obrigatória

Ao gerar estilos, interfaces ou sugerir elementos de UI, use exclusivamente a paleta de cores oficial:

- 🟦 **Azul principal**: `#1E3A8A`
- 🔵 **Azul secundário**: `#2563EB`
- 🟡 **Dourado**: `#D4AF37`
- ⚪ **Cinza claro / fundo**: `#F2F4F7`
- ⚫ **Cinza escuro / texto**: `#334155`

> [!TIP]
> **Aparência & Mascote:**
> A aplicação deve ter uma aparência corporativa, moderna, segura e amigável.
> O mascote do Bravito é uma **coruja tecnológica**. Ela pode ser usada como apoio visual, mas a interface deve permanecer profissional e madura, nunca parecendo infantil.

## Padrão obrigatório de nomenclatura do banco de dados

A estrutura de banco de dados do Projeto Bravito deve usar nomes em português.

Essa regra vale para:

* Tabelas.
* Colunas.
* Índices.
* Constraints.
* Migrations.
* Entidades persistidas.
* Configurações de mapeamento do Entity Framework.

Não criar tabelas ou campos em inglês.

Exemplos corretos:

* `Empresas`
* `Usuarios`
* `Perfis`
* `ConversasChat`
* `MensagensChat`
* `LogsAuditoria`
* `ConfiguracoesIntegracao`

Exemplos incorretos:

* `Tenants`
* `Users`
* `UserProfiles`
* `ChatConversations`
* `ChatMessages`
* `AuditLogs`
* `IntegrationSettings`

Exceções permitidas:

* Nomes técnicos de bibliotecas, pacotes e frameworks.
* Claims padrão do Keycloak/JWT, como `sub`, `email`, `preferred_username`, `realm_access`.
* Client IDs já definidos no Keycloak, como `bravito-flutter` e `bravito-api`.
* Variáveis técnicas de ambiente quando forem padrão da ferramenta.

Sempre que for criar uma nova tabela, entidade ou migration, usar nomenclatura em português, clara e consistente.


---

## 🚀 Escopo Atual (Fase 1)

### ✅ Implementar APENAS:
- Estrutura base do projeto (pastas e infra inicial).
- Fluxo de login integrado com o Keycloak.
- Tela de chat dinâmica e amigável.
- API backend segura em ASP.NET Core.
- Integração da API backend com os fluxos do n8n.
- Conexão e persistência com o PostgreSQL.
- Auditoria básica de dados.
- Logs estruturados e endpoints de health checks.

### ❌ NÃO implementar ainda (Fora de Escopo):
- Módulos de ERP.
- Dashboards e gráficos gerenciais.
- Relatórios estatísticos ou PDFs.
- Módulo Financeiro ou faturamento.
- Controle de Estoque.
- Fluxo ou telas de Vendas.
- Gestão completa e avançada de usuários.
- Arquitetura baseada em Microsserviços.
- Orquestração de containers com Kubernetes.

