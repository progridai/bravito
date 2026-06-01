import 'package:flutter_appauth/flutter_appauth.dart';
import '../config/app_config.dart';
import 'auth_helper.dart';

class AuthHelperIO implements AuthHelper {
  final FlutterAppAuth _appAuth = const FlutterAppAuth();

  @override
  Future<Map<String, String>?> login() async {
    final AuthorizationTokenResponse? result = await _appAuth.authorizeAndExchangeCode(
      AuthorizationTokenRequest(
        AppConfig.clientId,
        AppConfig.redirectUrl,
        discoveryUrl: '${AppConfig.keycloakAuthority}/.well-known/openid-configuration',
        scopes: AppConfig.scopes,
      ),
    );

    if (result != null && result.accessToken != null && result.refreshToken != null) {
      return {
        'access_token': result.accessToken!,
        'refresh_token': result.refreshToken!,
      };
    }
    return null;
  }

  @override
  Future<void> logout() async {
    // Implementação de logout para mobile
  }

  @override
  Future<Map<String, String>?> handleRedirect() async {
    return null;
  }
}

AuthHelper getAuthHelper() => AuthHelperIO();
