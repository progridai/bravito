import '../entities/user_entity.dart';

abstract class AuthRepository {
  Future<void> login();
  Future<void> logout();
  Future<UserEntity?> getCurrentUser();
  Future<bool> isAuthenticated();
}
