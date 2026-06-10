# 20 - Alteração de Senha Segura via Keycloak

## 🎯 Objetivo
Redirecionar a alteração de senha de dentro do Flutter para o ambiente oficial de gerenciamento de conta do provedor de identidade (Keycloak). 

## 🛡️ Segurança e Motivação
- **Zero Captura de Senhas**: O Flutter foi desenvolvido para não coletar a senha atual nem a nova senha do usuário.
- **Ambiente Centralizado e Seguro**: A alteração acontece no ambiente nativo do Keycloak, que já possui suas regras de segurança consolidadas.
- **Nenhum Backend Intermediário**: Como não há campos no Flutter, a API ASP.NET Core não precisa expor nem rotear um endpoint sensível de alteração de senha na Fase 1.

## 🛣️ Rota e URL
- **Rota no App**: `/menu/alterar-senha` (Continua protegida, porém visualmente alterada).
- **URL da Conta Keycloak**: `AppConfig.keycloakAccountUrl`
- Esta URL é derivada dinamicamente através da variável `AppConfig.keycloakAuthority` (Ex: `https://auth.bravida.com.br/realms/bravito/account`).
- A configuração pode ser ajustada nos arquivos de ambiente, centralizados na classe `AppConfig`.

## ⚠️ Tratamento de Erros
Se o aplicativo não puder abrir a URL externa no navegador do dispositivo, será lançada uma `SnackBar` vermelha informando: *"Não foi possível abrir a página de alteração de senha. Tente novamente em alguns instantes."*

## ⏭️ Próximo Passo Recomendado
- O Keycloak pode futuramente ser configurado com políticas de senha mais robustas (MFA, força de senha, etc.), o que se refletirá automaticamente na tela web sem nenhuma mudança necessária no Flutter.
- O próximo passo no desenvolvimento focado em funcionalidades será implementar a persistência de fato do Histórico de Conversas.
