import 'package:dio/dio.dart';
import '../../../../core/storage/secure_storage_service.dart';
import '../../../../core/security/auth_helper.dart';
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
      try {
        final tokens = await _authHelper.handleRedirect();
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
      print('DEBUG: Iniciando getCurrentUser()');

      final token = await _storageService.getAccessToken();
      print('DEBUG: Token obtido do storage: ${token != null ? "SIM (Tamanho: ${token.length})" : "NÃO"}');
      if (token == null) return null;

      print('DEBUG: Chamando a API local /api/auth/me...');
      final response = await _dio.get('/api/auth/me');
      print('DEBUG: Resposta da API recebida com status: ${response.statusCode}');
      
      if (response.statusCode == 200) {
        return UserModel.fromJson(response.data);
      }
      return null;
    } catch (e) {
      print('DEBUG: Exceção capturada em getCurrentUser(): $e');
      if (e is DioException) {
        print('DEBUG: DioException Status Code: ${e.response?.statusCode}');
        print('DEBUG: DioException Data: ${e.response?.data}');
        if (e.response?.statusCode == 401) {
          await _storageService.clearTokens();
        }
      }
      throw Exception('Erro ao buscar dados do usuário: $e');
    }
  }

  Future<bool> isAuthenticated() async {
    final token = await _storageService.getAccessToken();
    return token != null;
  }
}
