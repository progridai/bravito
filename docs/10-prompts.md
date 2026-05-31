# 10 — Prompts de Desenvolvimento

Este arquivo registra os prompts usados para orientar o desenvolvimento do Projeto Bravito com auxílio de IA.

A regra principal é nunca pedir para implementar muitas partes ao mesmo tempo.

O desenvolvimento deve ser dividido em etapas pequenas, validadas uma por uma.

## Ordem dos prompts

1. Criar estrutura inicial do repositório.
2. Configurar Docker Compose com PostgreSQL e Keycloak.
3. Configurar autenticação Keycloak no backend.
4. Criar tema e layout base do Flutter.
5. Criar tela de login integrada ao Keycloak.
6. Criar estrutura de chat no backend.
7. Criar integração segura backend com n8n.
8. Criar tela de chat no Flutter.
9. Criar auditoria e logs.
10. Revisar segurança e arquitetura.

## Prompt 1 — Estrutura inicial do projeto

Você será responsável por iniciar o desenvolvimento do Projeto Bravito.

Antes de escrever código, leia e siga integralmente:

- `AGENTS.md`
- `docs/01-visao-geral.md`
- `docs/02-arquitetura.md`
- `docs/03-padrao-visual.md`
- `docs/04-seguranca.md`

Nesta primeira etapa, NÃO implemente login funcional, NÃO implemente chat, NÃO implemente integração com n8n e NÃO implemente banco de dados ainda.

Seu objetivo agora é criar somente a estrutura inicial profissional do repositório, preparada para crescer com segurança e arquitetura limpa.

Crie a seguinte estrutura:

```text
bravito/
  backend/
    src/
      Bravito.Api/
      Bravito.Application/
      Bravito.Domain/
      Bravito.Infrastructure/
      Bravito.Shared/
    tests/
      Bravito.UnitTests/
      Bravito.IntegrationTests/

  frontend/
    bravito_app/

  docs/
    01-visao-geral.md
    02-arquitetura.md
    03-padrao-visual.md
    04-seguranca.md
    05-frontend-flutter.md
    06-backend-api.md
    07-banco-dados.md
    08-integracao-n8n.md
    09-roadmap.md
    10-prompts.md

  docker/
    docker-compose.yml

  AGENTS.md
  README.md

Requisitos da etapa:

Criar solução backend em ASP.NET Core com Clean Architecture.
Criar os projetos:
Bravito.Api
Bravito.Application
Bravito.Domain
Bravito.Infrastructure
Bravito.Shared
Configurar referências corretas entre os projetos.
Criar projeto Flutter dentro de frontend/bravito_app.
Criar estrutura inicial modular no Flutter.
Criar tema inicial com as cores oficiais do Bravito.
Garantir que nenhum segredo, senha, token ou webhook seja colocado no código.

Importante:

Não implemente autenticação agora.
Não implemente tela de login agora.
Não implemente chat agora.
Não implemente integração com n8n agora.
Não implemente conexão com banco agora.
Não invente módulos fora da fase 1.
Não use arquitetura simplificada.
Não coloque regra de negócio no frontend.
Não coloque URL de webhook no frontend.

Ao final, entregue:

Estrutura de pastas criada.
Lista dos arquivos principais criados.
Explicação curta das decisões tomadas.
Comandos necessários para compilar o backend.
Comandos necessários para rodar o Flutter.
Próximo passo recomendado, sem implementar ainda.