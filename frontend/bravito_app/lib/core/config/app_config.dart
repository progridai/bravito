class AppConfig {
  AppConfig._();

  static const String keycloakAuthority = 'https://bravito-keycloak.lchoyg.easypanel.host/realms/bravito';
  static const String clientId = 'bravito-flutter';
  static const String apiBaseUrl = 'https://bravito-api.lchoyg.easypanel.host';
  
  // Custom scheme para redirect URI no mobile (bravito://login-callback)
  static const String redirectUrlScheme = 'bravito';
  static const String redirectUrl = '$redirectUrlScheme://login-callback';
  
  // Scopes OIDC
  static const List<String> scopes = ['openid', 'profile', 'email', 'offline_access'];
}
