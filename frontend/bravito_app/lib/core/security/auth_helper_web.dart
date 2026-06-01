import 'package:openid_client/openid_client_browser.dart';
import '../config/app_config.dart';
import 'auth_helper.dart';

class AuthHelperWeb implements AuthHelper {
  @override
  Future<Map<String, String>?> login() async {
    // Descobrir endpoints OIDC
    final issuer = await Issuer.discover(Uri.parse(AppConfig.keycloakAuthority));
    
    // Configurar cliente Web
    // No Web, geralmente a porta muda, então pegamos a origem atual do navegador para redirect
    final clientId = AppConfig.clientId;
    final client = Client(issuer, clientId);

    final authenticator = Authenticator(client, scopes: AppConfig.scopes);
    authenticator.authorize(); // Isso vai redirecionar a aba atual para o Keycloak
    
    // A página será recarregada após o login e o código entrará em getCredential
    return null;
  }

  @override
  Future<void> logout() async {
    // Implementação para Web
  }

  // Método que deve ser chamado logo após inicialização para capturar o redirecionamento
  static Future<Map<String, String>?> handleWebRedirect() async {
    try {
      final issuer = await Issuer.discover(Uri.parse(AppConfig.keycloakAuthority));
      final client = Client(issuer, AppConfig.clientId);
      final authenticator = Authenticator(client, scopes: AppConfig.scopes);
      
      final credential = await authenticator.credential;
      if (credential != null) {
        final token = await credential.getTokenResponse();
        if (token.accessToken != null) {
           return {
             'access_token': token.accessToken!,
             'refresh_token': token.refreshToken ?? '',
           };
        }
      }
    } catch (e) {
      // Falha ao processar redirect
    }
    return null;
  }
}

AuthHelper getAuthHelper() => AuthHelperWeb();
