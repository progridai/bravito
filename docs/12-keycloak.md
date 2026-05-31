# 12 — Autenticação e Keycloak

## 🔐 Visão Geral
A autenticação e autorização do ecossistema Bravito são centralizadas no **Keycloak**. Nenhuma senha real ou lógica de hash é feita no backend da aplicação; tudo é delegado ao servidor de identidade.

## 🏗️ Configuração do Realm (`bravito`)
O realm principal do sistema chama-se **bravito**. Para facilitar o desenvolvimento local, um arquivo de pré-configuração (`bravito-realm.json`) é importado automaticamente quando o container do Keycloak sobe.

## 📱 Clients Existentes

1. **`bravito-flutter`**
   - **Tipo:** Public Client (Sem Client Secret).
   - **Fluxo:** Authorization Code Flow com PKCE (Proof Key for Code Exchange).
   - **Uso:** Autenticação no aplicativo móvel e portal web. Ele redireciona o usuário para o login do Keycloak e troca o código temporário por tokens JWT.

2. **`bravito-api`**
   - **Tipo:** Bearer-only (Representa um recurso protegido).
   - **Uso:** Servirá como "Audience" na API ASP.NET Core para validar a assinatura e a expiração dos tokens enviados pelo Flutter.

## 👥 Roles Iniciais (Perfis de Acesso)
- `admin`: Acesso total ao sistema e integrações.
- `gestor`: Gerencia equipes e relatórios.
- `operador`: Utiliza as funcionalidades do dia a dia do assistente.
- `auditor`: Acesso apenas leitura a logs e históricos.

## 🧪 Usuário de Teste Local
Um usuário pré-criado já existe para facilitar o desenvolvimento:
- **Username:** `dev.admin`
- **Email:** `dev.admin@bravito.local`
- **Senha:** `Admin@123456`
- **Role:** `admin`

## ⚙️ Painel Administrativo
- **Acesso:** `http://localhost:8080`
- **Login Admin Geral:** `admin` / `admin` (Definido no `.env`).
- **Validação:** Após logar, clique na lista de realms no canto superior esquerdo e verifique se o realm **bravito** existe. Na aba "Users", busque por "dev.admin" para conferir se o perfil foi carregado corretamente.
