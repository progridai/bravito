import 'package:dio/dio.dart';
import '../../../../core/storage/secure_storage_service.dart';
import '../../../../core/security/auth_helper.dart';
import '../../../../core/security/auth_helper_web.dart' if (dart.library.io) '../../../../core/security/auth_helper_stub.dart'; // import condicional só pra usar a classe na compilação da web, na verdade podemos usar kIsWeb
import 'package:flutter/foundation.dart';
import '../models/user_model.dart';

class AuthRemoteDataSource {
  final SecureStorageService _storageService;
  final Dio _dio;
  late final AuthHelper _authHelper;

  AuthRemoteDataSource(this._storageService, this._dio) {
    _authHelper = AuthHelper();
  }

  Future<void> processWebRedirect() async {
    if (kIsWeb) {
      // Tratamento especial para o redirect da Web
      try {
        final AuthHelperWeb helperWeb = _authHelper as AuthHelperWeb;
        final tokens = await AuthHelperWeb.handleWebRedirect();
        if (tokens != null) {
          await _storageService.saveTokens(
            accessToken: tokens['access_token']!,
            refreshToken: tokens['refresh_token']!,
          );
        }
      } catch (_) {}
    }
  }

  Future<void> login() async {
    try {
      final tokens = await _authHelper.login();
      
      if (tokens != null) {
        await _storageService.saveTokens(
          accessToken: tokens['access_token']!,
          refreshToken: tokens['refresh_token']!,
        );
      }
    } catch (e) {
      throw Exception('Erro durante a autenticação: $e');
    }
  }

  Future<void> logout() async {
    try {
      await _authHelper.logout();
      await _storageService.clearTokens();
    } catch (e) {
      throw Exception('Erro durante o logout: $e');
    }
  }

  Future<UserModel?> getCurrentUser() async {
    try {
      await processWebRedirect(); // Verifica e salva se houver token na URL (Web)

      final token = await _storageService.getAccessToken();
      if (token == null) return null;

      final response = await _dio.get('/api/auth/me');
      
      if (response.statusCode == 200) {
        return UserModel.fromJson(response.data);
      }
      return null;
    } catch (e) {
      if (e is DioException && e.response?.statusCode == 401) {
        await _storageService.clearTokens();
      }
      throw Exception('Erro ao buscar dados do usuário');
    }
  }

  Future<bool> isAuthenticated() async {
    final token = await _storageService.getAccessToken();
    return token != null;
  }
}
