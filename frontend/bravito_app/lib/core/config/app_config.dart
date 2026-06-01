class AppConfig {
  AppConfig._();

  static const String keycloakAuthority = 'http://localhost:8080/realms/bravito';
  static const String clientId = 'bravito-flutter';
  static const String apiBaseUrl = 'http://localhost:5132';
  
  // Custom scheme para redirect URI no mobile (bravito://login-callback)
  static const String redirectUrlScheme = 'bravito';
  static const String redirectUrl = '$redirectUrlScheme://login-callback';
  
  // Scopes OIDC
  static const List<String> scopes = ['openid', 'profile', 'email', 'offline_access'];
}
