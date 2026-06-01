# Integração do Chat Flutter com Backend e n8n

## Objetivo
Este documento detalha a implementação da tela de chat no aplicativo Flutter, sua arquitetura baseada em Clean Architecture, o gerenciamento de estado via Riverpod e o fluxo seguro de comunicação com a API Backend (que por sua vez repassa as informações para o assistente no n8n).

## Fluxo de Comunicação
A arquitetura foi projetada para que o Flutter nunca possua as chaves ou URLs do assistente (n8n). Todo o fluxo de mensagens trafega de forma autenticada pela API C#.

1. **Usuário Digita e Envia:** Pelo App, o usuário insere texto. O App não permite o envio de strings vazias. O balão da mensagem do usuário aparece instantaneamente e o status entra em "carregando" (spinner).
2. **Requisição HTTP (Dio):** O `ChatRemoteDataSource` envia um payload simples (`{"mensagem": "..."}`) para a rota `POST /api/chat/enviar`.
3. **Injeção de Segurança (AuthInterceptor):** O `Dio` automaticamente anexa o header `Authorization: Bearer <TOKEN>` extraído do `SecureStorage` no momento do envio. Não é necessário injetar lógica de token no Chat Controller.
4. **Backend -> n8n -> Backend:** A API C# valida o JWT, extrai os metadados confidenciais (IDs, Email) e envia de forma invisível para o n8n.
5. **Retorno e Renderização:** O App recebe a resposta em JSON (`sucesso`, `resposta`, `mensagemErro`), remove o "loading" e plota o balão do assistente ou, em caso de falha, um balão de erro (sistema) avermelhado para alertar o usuário.

## Estrutura da Feature (`lib/features/chat`)
A arquitetura baseia-se fortemente em Clean Architecture para promover reuso, testes fáceis e independência de UI:

*   `domain/entities/`:
    *   `mensagem_chat.dart`: Representa o objeto de domínio central para renderização da UI (tipoRemetente, texto, dataHora, erro).
    *   `tipo_remetente.dart`: Enum com os tipos (`usuario`, `assistente`, `sistema`).
*   `data/models/`:
    *   `enviar_mensagem_chat_request_model.dart`: DTO de requisição do backend.
    *   `enviar_mensagem_chat_response_model.dart`: DTO de resposta serializado do backend.
*   `domain/usecases/`:
    *   `enviar_mensagem_chat_usecase.dart`: Validações de regra de negócio, garantindo por exemplo que a string não seja nula nem composta por espaços vazios, repassando ao repositório.
*   `data/datasources/`:
    *   `chat_remote_data_source.dart`: Interação com a API via pacote Dio. Captura de Response Errors (HTTP 401, 502, etc).
*   `presentation/controllers/`:
    *   `chat_state.dart` e `chat_controller.dart`: State Management limpo usando Riverpod `Notifier`. Mantém o histórico virtual efêmero (em memória).
*   `presentation/pages/` & `presentation/widgets/`:
    *   `chat_page.dart`: Interface UI. Consome os providers, renderiza o scroll, capta envios pelo teclado (`onSubmitted`).
    *   `mensagem_chat_bubble.dart`: Componente que desenha balões alinhados baseado no tipo de rementente (Azul = Usuário / Cinza = Assistente / Vermelho = Falha).

## Tratamento de Erros

**Falhas de Rede/API:**
Se ocorrer timeout ou erro 500/502 no n8n/Backend, o App exibe uma bolha extra de `TipoRemetente.sistema` indicando a falha "Ocorreu um erro de comunicação" de forma amigável e preserva a interface de digitação limpa.

**Token Expirado (HTTP 401):**
O `ChatRemoteDataSource` levanta uma Exception específica que o `ChatController` formata. Como a proteção das páginas do App é global, a falha do Dio fará o interceptor principal limpar o Token do Vault. O Riverpod vai observar que o token expirou no AuthController e varrer a árvore de rotas devolvendo o usuário bruscamente à `/login`.

## Pendências Não Implementadas
- Não foi implementado armazenamento ou busca de Histórico Paginado via Banco de Dados (as conversas morrem ao fechar o App).
- Não há lógica avançada de Refresh Token. Se expirar, o usuário é deslogado.
- Não foi feito persistência do `conversaId` nos repositórios para retomar o contexto após reabrir a tela.

## Próximo Passo Recomendado
O próximo passo deve ser consolidar a persistência do Histórico das Conversas de Chat dentro do PostgreSQL no lado Backend, criando a modelagem (Tabelas e Migrations). Assim as conversas enviadas para o N8N poderão ser consultadas de forma duradoura.
