# 16 - Implementação de Login Flutter com Keycloak (PKCE)

Este documento detalha a implementação da autenticação real no aplicativo Flutter do **Bravito**, conectando-se ao Keycloak através do fluxo seguro **Authorization Code Flow with PKCE** e validando a sessão no Backend ASP.NET Core.

## 🔄 Fluxo de Login Implementado

O fluxo implementado é o padrão ouro para aplicações mobile (SPA/Mobile):
1. O usuário clica em "Entrar com Keycloak" na tela de login.
2. O Flutter utiliza a biblioteca `flutter_appauth` para abrir uma aba de navegador segura isolada (Chrome Custom Tabs no Android / ASWebAuthenticationSession no iOS).
3. O Keycloak exibe a sua tela de login real.
4. Após autenticação bem-sucedida, o Keycloak redireciona o usuário de volta ao aplicativo através de um Deep Link Custom Scheme (`bravito://login-callback`).
5. O `flutter_appauth` intercepta o código e faz a troca (exchange) pelo `access_token` e `refresh_token` diretamente com o Keycloak, usando o verificador PKCE (gerado automaticamente).
6. Os tokens são armazenados com criptografia no dispositivo usando `flutter_secure_storage`.
7. O aplicativo consome o endpoint `GET /api/auth/me` do nosso backend (ASP.NET Core) enviando o Token JWT via cabeçalho `Authorization: Bearer`.
8. Ao receber o 200 OK do backend com os dados do usuário, o estado global é atualizado e o usuário é redirecionado para a tela de Chat.

## ⛔ Por que não usar `grant_type=password`?

O fluxo Resource Owner Password Credentials (`grant_type=password`) exige que a aplicação capture a senha do usuário em campos de texto locais e a trafegue até o Auth Server. Isso:
- Quebra a delegação de confiança (o app nunca deve conhecer a senha do usuário).
- Dificulta ou impede a implementação de MFA (Múltiplo Fator de Autenticação).
- É formalmente desencorajado pelas especificações OAuth 2.1 e melhores práticas de segurança atuais.

Portanto, a tela de login local do Flutter foi adaptada para conter apenas o botão de redirecionamento, removendo os campos estáticos de usuário e senha.

## ⚙️ Configurações do Keycloak Utilizadas

O Client do aplicativo no Keycloak (`bravito-flutter`) está devidamente configurado no realm `bravito`:
- **Client ID**: `bravito-flutter`
- **Access Type**: `public` (Client Authentication desabilitado / sem client secret)
- **Standard Flow Enabled**: `true` (Habilita Authorization Code)
- **PKCE**: Requerido (`S256`)
- **Valid Redirect URIs**: `bravito://*`, `http://localhost:*`

**Nota:** Nenhuma modificação no `bravito-realm.json` foi necessária, pois ele já estava preparado para este esquema na concepção inicial.

## 🚀 Como Testar

### 1. Iniciar a Infraestrutura
Na pasta raiz do projeto:
```bash
cd docker
docker compose up -d
```
Aguarde o Keycloak (porta 8080) e PostgreSQL subirem.

### 2. Iniciar a API Backend
Abra um terminal, vá para a raiz e inicie a API (necessário para o endpoint `/api/auth/me`):
```bash
cd backend/Bravito.Api
dotnet run
```
A API rodará em `http://localhost:5000`.

### 3. Iniciar o Flutter
Em outro terminal:
```bash
cd frontend/bravito_app
flutter run
```

### 4. Realizando o Login
1. Ao abrir o App, você verá a tela inicial.
2. Clique no botão "Entrar com Keycloak".
3. O navegador será aberto.
4. Faça login com o usuário de testes:
   - **Usuário**: `dev.admin`
   - **Senha**: `Admin@123456`
5. Você será redirecionado de volta ao App, e em seguida levado à tela do Chat exibindo "Olá, Dev".

### 5. Testando Logout
- Na tela de Chat, clique no ícone de "Sair" na barra superior (AppBar).
- Os tokens locais serão apagados do Storage, o estado voltará para Não-Autenticado e você retornará à tela de Login.

## 🚧 Limitações Atuais e Adaptação para Web
A biblioteca `flutter_appauth` é a solução padrão oficial para integrações mobile complexas de OAuth/PKCE devido ao uso rigoroso de in-app browsers seguros. No entanto, ela **não suporta o Flutter Web** nativamente.

Para permitir testes imediatos na Web (Chrome), incluímos um fallback utilizando a biblioteca `openid_client`.
**Atenção:** Na Web, o `openid_client` utiliza nativamente o **Implicit Flow** (ao invés do Authorization Code Flow). 
Por isso, a configuração do Keycloak no arquivo `docker/keycloak/bravito-realm.json` foi ajustada (`"implicitFlowEnabled": true`) para permitir que o cliente Web consiga logar.

## ⏭️ Próximo Passo Recomendado
- O próximo passo natural é a **Integração Real do Chat com o n8n via Webhooks Backend**. 
- Substituir o placeholder do ChatPage pela UI final de envio de mensagens e gerenciar os fluxos de streaming/SSE (Server-Sent Events) conectando o Flutter -> API -> n8n.
