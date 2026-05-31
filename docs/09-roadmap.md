# 09 — Roadmap do Projeto Bravito

## Fase 1 — Fundação

Objetivo:

Criar a base segura e escalável do projeto.

Itens:

- Estrutura do repositório.
- Backend com Clean Architecture.
- Frontend Flutter modular.
- Tema visual do Bravito.
- Docker Compose inicial.
- PostgreSQL.
- Keycloak.
- Autenticação no backend.
- Login no Flutter.
- Chat no backend.
- Integração backend com n8n.
- Chat no Flutter.
- Auditoria básica.
- Logs.
- Health checks.

## Fase 2 — Governança

Objetivo:

Criar recursos administrativos e controle de acesso.

Itens:

- Gestão de usuários.
- Gestão de perfis.
- Gestão de permissões.
- Gestão de empresas.
- Auditoria avançada.
- Painel administrativo.
- Controle por empresa/unidade.

## Fase 3 — Integração ERP

Objetivo:

Começar a consumir dados do ERP com segurança.

Itens:

- APIs para consulta de dados do ERP.
- Permissões por módulo.
- Controle por loja/unidade.
- Cache.
- Logs por operação.
- Relatórios iniciais.
- Controle de acesso por tipo de informação.

## Fase 4 — Escala

Objetivo:

Preparar o sistema para maior volume e operação crítica.

Itens:

- Redis.
- Observabilidade.
- Métricas.
- Tracing.
- Rate limit avançado.
- Deploy automatizado.
- Monitoramento.
- Alertas.
- Estratégia de backup.
- Estratégia de alta disponibilidade.

## Escopo proibido na fase 1

Não implementar agora:

- ERP.
- Dashboard.
- Relatórios.
- Financeiro.
- Estoque.
- Vendas.
- Gestão completa de usuários.
- Microserviços.
- Kubernetes.
- Mensageria complexa.
- Aplicação web separada em React.