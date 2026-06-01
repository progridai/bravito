import 'package:dio/dio.dart';
import '../storage/secure_storage_service.dart';

class AuthInterceptor extends Interceptor {
  final SecureStorageService _storageService;

  AuthInterceptor(this._storageService);

  @override
  void onRequest(RequestOptions options, RequestInterceptorHandler handler) async {
    final token = await _storageService.getAccessToken();
    if (token != null) {
      options.headers['Authorization'] = 'Bearer $token';
    }
    super.onRequest(options, handler);
  }

  @override
  void onError(DioException err, ErrorInterceptorHandler handler) async {
    // Aqui futuramente pode-se implementar o refresh token
    if (err.response?.statusCode == 401) {
      // Token expirado ou inválido
      await _storageService.clearTokens();
    }
    super.onError(err, handler);
  }
}
