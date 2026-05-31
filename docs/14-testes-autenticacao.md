# 14 — Testes de Autenticação JWT

## 🛡️ Objetivo
Este documento serve como um guia rápido para validar a infraestrutura de segurança da API ASP.NET Core (`Bravito.Api`) juntamente com o servidor de identidade Keycloak. Os passos abaixo certificam que a comunicação, a proteção de endpoints e a leitura de Claims (Audience, Roles, Email, etc.) estão funcionando corretamente.

---

## 🛑 Testes sem Token (Anônimo)

Antes de gerar um token, você pode validar o comportamento da API bloqueando acessos indevidos.
 *(Certifique-se de que a API esteja rodando localmente, por exemplo, na porta 5000).*

1. **Endpoint Público:**
   ```bash
   curl -s http://localhost:5000/api/public/ping
   ```
   **Resultado Esperado:** HTTP 200 e a mensagem `{"message": "Bravito API online"}`.

2. **Endpoint Privado (Ping):**
   ```bash
   curl -s -w "\n%{http_code}" http://localhost:5000/api/private/ping
   ```
   **Resultado Esperado:** HTTP 401 (Unauthorized).

3. **Endpoint Privado (Claims):**
   ```bash
   curl -s -w "\n%{http_code}" http://localhost:5000/api/auth/me
   ```
   **Resultado Esperado:** HTTP 401 (Unauthorized).

---

## 🔑 Como Obter um Token Localmente

O realm `bravito` está configurado com o usuário de teste `dev.admin`. Como o client `bravito-flutter` possui a propriedade *Direct Access Grants* ativa para desenvolvimento local, você pode obter um token JWT diretamente via cURL:

```bash
curl -s -X POST http://localhost:8080/realms/bravito/protocol/openid-connect/token \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "client_id=bravito-flutter" \
  -d "username=dev.admin" \
  -d "password=Admin@123456" \
  -d "grant_type=password"
```

O comando acima retornará um JSON contendo a propriedade `access_token`. 

> [!NOTE]
> **Validação de Audience (aud):** 
> O client `bravito-flutter` no arquivo `bravito-realm.json` possui um *Protocol Mapper* (`oidc-audience-mapper`) configurado. Ele injeta forçadamente a Audience `bravito-api` dentro do `access_token`. Sem isso, o ASP.NET Core rejeitaria o JWT.

---

## ✅ Testes com Token (Autenticado)

Com o `access_token` em mãos (substitua `<SEU_TOKEN>` nos comandos abaixo pelo valor real longo), execute os testes finais:

1. **Testando o Ping Protegido:**
   ```bash
   curl -s -H "Authorization: Bearer <SEU_TOKEN>" http://localhost:5000/api/private/ping
   ```
   **Resultado Esperado:** `{"message": "Acesso autenticado com sucesso"}`

2. **Extraindo as Claims do Usuário:**
   ```bash
   curl -s -H "Authorization: Bearer <SEU_TOKEN>" http://localhost:5000/api/auth/me
   ```
   **Resultado Esperado:** 
   O ASP.NET Core lerá e confirmará o token validando: `Issuer`, `Audience`, `Subject (id)`, `preferred_username`, `email`, e `roles`.
   
   ```json
   {
       "id": "9f83e8d7-5c16-4530-a860-7fdeeca11fd4",
       "username": "dev.admin",
       "name": "Dev Admin",
       "email": "dev.admin@bravito.local",
       "roles": [ "{\"roles\":[\"admin\"]}" ]
   }
   ```
   *(Nota: O mapeamento de roles pode vir compactado pelo Realm Access do Keycloak, mas a API é capaz de extrair a claim sem problemas).*

---

## 🔁 Sobre o Arquivo de Configuração (bravito-realm.json)
Qualquer alteração em Mappers de Audience ou propriedades de Clients que exija testes seguros (como a liberação de Direct Grants) foi salva no arquivo versionado `docker/keycloak/bravito-realm.json`.

Caso seja necessário atualizar a base de testes local com modificações novas que sua equipe subir nesse arquivo, rode:

```bash
cd docker
docker compose down -v
docker compose up -d
```
Isso destruirá as tabelas atuais e importará o JSON limpo no banco. Nunca inclua credenciais reais de produção neste arquivo.
