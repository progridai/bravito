import 'package:dio/dio.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../config/app_config.dart';
import 'auth_interceptor.dart';
import '../storage/secure_storage_service.dart';

final secureStorageProvider = Provider((ref) => SecureStorageService());

final dioClientProvider = Provider((ref) {
  final storage = ref.watch(secureStorageProvider);
  return DioClient(storage);
});

class DioClient {
  late final Dio _dio;

  DioClient(SecureStorageService storageService) {
    _dio = Dio(
      BaseOptions(
        baseUrl: AppConfig.apiBaseUrl,
        connectTimeout: const Duration(seconds: 10),
        receiveTimeout: const Duration(seconds: 10),
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
        },
      ),
    );

    _dio.interceptors.add(AuthInterceptor(storageService));
  }

  Dio get dio => _dio;
}
