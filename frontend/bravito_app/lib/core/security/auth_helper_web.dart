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

  @override
  Future<Map<String, String>?> handleRedirect() async {
    return await handleWebRedirect();
  }

  // Método que deve ser chamado logo após inicialização para capturar o redirecionamento
  static Future<Map<String, String>?> handleWebRedirect() async {
    try {
      print('DEBUG: [OIDC] handleWebRedirect() iniciado');
      final issuer = await Issuer.discover(Uri.parse(AppConfig.keycloakAuthority));
      final client = Client(issuer, AppConfig.clientId);
      final authenticator = Authenticator(client, scopes: AppConfig.scopes);
      
      final credential = await authenticator.credential;
      if (credential != null) {
        print('DEBUG: [OIDC] credential encontrado, solicitando token...');
        final token = await credential.getTokenResponse();
        print('DEBUG: [OIDC] token retornado. Access token presente: ${token.accessToken != null}');
        if (token.accessToken != null) {
           return {
             'access_token': token.accessToken!,
             'refresh_token': token.refreshToken ?? '',
           };
        }
      } else {
        print('DEBUG: [OIDC] Nenhuma credential encontrada na URL.');
      }
    } catch (e) {
      print('DEBUG: [OIDC] Exceção durante handleWebRedirect(): $e');
    }
    return null;
  }
}

AuthHelper getAuthHelper() => AuthHelperWeb();
