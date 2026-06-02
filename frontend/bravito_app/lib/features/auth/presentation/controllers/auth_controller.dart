import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:local_auth/local_auth.dart';
import '../../../../core/storage/secure_storage_service.dart';
import '../../../../core/http/dio_client.dart';
import '../../data/datasources/auth_remote_data_source.dart';
import '../../data/repositories/auth_repository_impl.dart';
import '../../domain/usecases/login_usecase.dart';
import '../../domain/usecases/logout_usecase.dart';
import '../../domain/usecases/get_current_user_usecase.dart';
import '../../domain/entities/user_entity.dart';
import 'auth_state.dart';



final authRemoteDataSourceProvider = Provider((ref) {
  final storage = ref.watch(secureStorageProvider);
  final dio = ref.watch(dioClientProvider).dio;
  return AuthRemoteDataSource(storage, dio);
});

final authRepositoryProvider = Provider((ref) {
  final remoteData = ref.watch(authRemoteDataSourceProvider);
  return AuthRepositoryImpl(remoteData);
});

final loginUseCaseProvider = Provider((ref) => LoginUseCase(ref.watch(authRepositoryProvider)));
final logoutUseCaseProvider = Provider((ref) => LogoutUseCase(ref.watch(authRepositoryProvider)));
final getCurrentUserUseCaseProvider = Provider((ref) => GetCurrentUserUseCase(ref.watch(authRepositoryProvider)));

class AuthController extends Notifier<AuthState> {
  late LoginUseCase _loginUseCase;
  late LogoutUseCase _logoutUseCase;
  late GetCurrentUserUseCase _getCurrentUserUseCase;

  @override
  AuthState build() {
    _loginUseCase = ref.watch(loginUseCaseProvider);
    _logoutUseCase = ref.watch(logoutUseCaseProvider);
    _getCurrentUserUseCase = ref.watch(getCurrentUserUseCaseProvider);
    
    // We can't do async initialization directly in build if we want to return a synchronous state 
    // without returning AsyncValue. But for simplicity, we return AuthInitial and trigger check.
    Future.microtask(() => checkAuthStatus(requireBiometric: true));
    return AuthInitial();
  }

  Future<void> checkAuthStatus({bool requireBiometric = false}) async {
    state = AuthLoading();
    try {
      final user = await _getCurrentUserUseCase();
      if (user != null) {
        if (requireBiometric) {
          final LocalAuthentication auth = LocalAuthentication();
          final bool canAuthenticateWithBiometrics = await auth.canCheckBiometrics;
          final bool canAuthenticate = canAuthenticateWithBiometrics || await auth.isDeviceSupported();

          if (canAuthenticate) {
            try {
              final bool didAuthenticate = await auth.authenticate(
                localizedReason: 'Por favor, autentique-se para acessar o Bravito',
                options: const AuthenticationOptions(
                  stickyAuth: true,
                  biometricOnly: false,
                ),
              );
              
              if (!didAuthenticate) {
                state = AuthUnauthenticated();
                return;
              }
            } on PlatformException catch (e) {
              // Se houver qualquer erro com a biometria nativa, permitimos o acesso
              // como fallback ou logamos o erro e deixamos passar.
            }
          }
        }
        state = AuthAuthenticated(user);
      } else {
        state = AuthUnauthenticated();
      }
    } catch (e) {
      state = AuthUnauthenticated();
    }
  }

  Future<void> login() async {
    state = AuthLoading();
    try {
      await _loginUseCase();
      await checkAuthStatus(requireBiometric: false);
    } catch (e) {
      state = AuthError('Erro ao realizar login. Tente novamente.');
    }
  }

  Future<void> logout() async {
    state = AuthLoading();
    try {
      await _logoutUseCase();
      state = AuthUnauthenticated();
    } catch (e) {
      state = AuthError('Erro ao realizar logout.');
    }
  }
}

final authControllerProvider = NotifierProvider<AuthController, AuthState>(() {
  return AuthController();
});
