import '../../domain/entities/user_entity.dart';
import '../../domain/repositories/auth_repository.dart';
import '../datasources/auth_remote_data_source.dart';

class AuthRepositoryImpl implements AuthRepository {
  final AuthRemoteDataSource _remoteDataSource;

  AuthRepositoryImpl(this._remoteDataSource);

  @override
  Future<void> login() async {
    await _remoteDataSource.login();
  }

  @override
  Future<void> logout() async {
    await _remoteDataSource.logout();
  }

  @override
  Future<UserEntity?> getCurrentUser() async {
    return await _remoteDataSource.getCurrentUser();
  }

  @override
  Future<bool> isAuthenticated() async {
    return await _remoteDataSource.isAuthenticated();
  }
}
