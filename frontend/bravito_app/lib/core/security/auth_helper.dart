import 'auth_helper_stub.dart'
  if (dart.library.io) 'auth_helper_io.dart'
  if (dart.library.html) 'auth_helper_web.dart';

abstract class AuthHelper {
  Future<Map<String, String>?> login();
  Future<void> logout();

  factory AuthHelper() => getAuthHelper();
}
