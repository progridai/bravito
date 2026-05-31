# 04 — Segurança do Projeto Bravito

---

## 🎯 Objetivo de Segurança
Como o ecossistema Bravito atuará como facilitador de processos operacionais e integração de dados corporativos (ERP, vendas, financeiro e estoque), **a segurança da informação é tratada como pilar obrigatório desde a primeira linha de código**, e nunca como uma etapa a ser resolvida posteriormente.

---

## 🚨 Regras Obrigatórias e Inegociáveis

> [!WARNING]
> **REGRAS ABSOLUTAS DE SEGURANÇA:**
> - **Isolamento de Banco**: O frontend **nunca** acessa o banco PostgreSQL diretamente.
> - **Isolamento de Integrações**: O frontend **nunca** chama webhooks do n8n diretamente.
> - **Segredos Protegidos**: Nenhum segredo, chave privada, client secret, senha ou URL sensível/privada deve ser colocada no código do frontend ou hardcoded no backend.
> - **Autenticação Centralizada**: Toda autenticação de usuários é intermediada exclusivamente pelo **Keycloak**.
> - **Validação no Backend**: O frontend pode apenas ocultar elementos visuais para fins de usabilidade (UX), mas **toda e qualquer autorização deve ser re-validada e bloqueada na API backend**.
> - **Tráfego Seguro**: Roteamento obrigatoriamente protegido por JWT com tempo de expiração curto nas rotas sensíveis.
> - **Auditoria e Logs**: Ações sensíveis e acessos devem ser logados, garantindo que senhas, segredos e tokens de acesso **nunca** apareçam nos arquivos de logs estruturados.

---

## 🔑 Autenticação e Autorização

### 1. Mecanismo de Autenticação (Keycloak)
*   Integração baseada em padrões de mercado: **OpenID Connect (OIDC)** e **OAuth 2.0**.
*   Validação no backend baseada em tokens **JWT (JSON Web Tokens)** assinados.
*   Suporte completo a fluxos de **Refresh Tokens** para renovação contínua e segura da sessão.
*   Uso de **Roles** e **Claims** dentro do payload do JWT para mapear o nível de acesso do usuário.

### 2. Validação de Tokens no Backend
*   O token recebido no header `Authorization: Bearer <JWT>` deve ser validado criptograficamente contra o servidor do Keycloak a cada requisição.
*   **Access Tokens** devem possuir curta duração para minimizar a janela de exposição de tokens interceptados.
*   Armazenamento local seguro do token:
    *   No Flutter Mobile/Web: Utilizar exclusivamente `Flutter Secure Storage`.
    *   *Proibido persistir tokens em plain text ou arquivos de configuração abertos.*

---

## 🌐 Configuração de CORS (Cross-Origin Resource Sharing)
A API backend deve impor políticas estritas de compartilhamento de origem em ambientes de homologação e produção:
*   **Proibido**: Usar `AllowAnyOrigin()`, `AllowAnyHeader()` ou `AllowAnyMethod()` sem restrições ou controle.
*   **Configuração Correta**: Declarar explicitamente as origens confiáveis (ex: URL exata do portal web do Flutter) permitindo apenas os verbos HTTP estritamente necessários para a operação (`GET`, `POST`, `OPTIONS`).

---

## 📝 Auditoria Mínima Obrigatória
A API backend deve interceptar e gravar no banco de dados relacional (via tabela `AuditLogs`) os seguintes eventos de segurança:
1.  Login efetuado com sucesso.
2.  Tentativas de login inválidas/com falha.
3.  Logouts executados.
4.  Mensagens enviadas no chat pelo usuário (com UUID da conversa).
5.  Respostas válidas recebidas do assistente de IA no n8n.
6.  Erros e timeouts na comunicação entre a API e o n8n.
7.  Acessos negados (`403 Forbidden`) em endpoints administrativos ou restritos.
8.  Alterações futuras de permissões ou configurações de Tenants.

---

## 🏢 Arquitetura Multiempresa (Multi-tenant)
Desde a primeira fase, a API backend deve garantir que nenhuma empresa acesse ou visualize dados de outra:
*   Cada requisição autenticada deve carregar a identificação da empresa inquilina (**TenantId**) e do usuário (**UserId**) no contexto.
*   Toda consulta, inserção ou atualização no PostgreSQL deve incluir a filtragem obrigatória pelo **TenantId**.
*   A estrutura deve conter validações em tempo de execução para verificar se as Claims do usuário combinam com a empresa e loja solicitadas na rota.

---

## 🤖 Segurança na Integração com o n8n
O n8n opera exclusivamente como um serviço de rede interno/privado.

*   **Ponto a Ponto**: A API backend é a única autorizada a realizar requisições HTTP POST para os webhooks de execução do n8n.
*   **Autenticação**: Uso obrigatório do cabeçalho `Authorization: Bearer <N8N_INTERNAL_TOKEN>` configurado no backend.
*   **Enriquecimento**: O payload enviado ao n8n deve ser formatado pelo backend, contendo o contexto limpo do usuário (`tenantId`, `userId`) sem trafegar dados brutos ou confidenciais desnecessários.
*   **Resiliência**: O backend deve aplicar timeouts rigorosos (ex: 15-30 segundos no HTTP Client) para evitar travamento de threads em caso de indisponibilidade do n8n.
