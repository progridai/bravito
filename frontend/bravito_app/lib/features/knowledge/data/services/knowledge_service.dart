import 'dart:developer';
import 'dart:typed_data';
import 'package:dio/dio.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../../core/http/dio_client.dart';
import '../../domain/models/knowledge_document.dart';

final knowledgeServiceProvider = Provider<KnowledgeService>((ref) {
  final dioClient = ref.watch(dioClientProvider);
  return KnowledgeService(dioClient.dio);
});

class KnowledgeApiException implements Exception {
  final String message;
  final String? step;
  final String? details;
  final String? traceId;

  KnowledgeApiException({
    required this.message,
    this.step,
    this.details,
    this.traceId,
  });

  @override
  String toString() => message;
}

class KnowledgeService {
  final Dio _dio;

  KnowledgeService(this._dio);

  Future<List<KnowledgeDocument>> getKnowledgeDocuments() async {
    try {
      final response = await _dio.get('/api/knowledge/documents');
      if (response.data is List) {
        return (response.data as List)
            .map((e) => KnowledgeDocument.fromJson(e))
            .toList();
      }
      return [];
    } catch (e) {
      throw _handleError(e, 'Erro ao buscar documentos');
    }
  }

  Future<KnowledgeDocument> uploadKnowledgeDocument(
      Uint8List fileBytes, String fileName, {String? app}) async {
    try {
      final formData = FormData.fromMap({
        'file': MultipartFile.fromBytes(fileBytes, filename: fileName),
        if (app != null) 'app': app,
      });

      final response = await _dio.post(
        '/api/knowledge/documents/upload',
        data: formData,
      );

      return KnowledgeDocument.fromJson(response.data);
    } catch (e) {
      throw _handleError(e, 'Erro ao realizar upload do documento');
    }
  }

  Future<void> deleteKnowledgeDocument(String id) async {
    try {
      await _dio.delete('/api/knowledge/documents/$id');
    } catch (e) {
      throw _handleError(e, 'Erro ao excluir documento');
    }
  }

  Future<KnowledgeDocument> replaceKnowledgeDocument(
      String id, Uint8List fileBytes, String fileName) async {
    try {
      final formData = FormData.fromMap({
        'file': MultipartFile.fromBytes(fileBytes, filename: fileName),
      });

      final response = await _dio.post(
        '/api/knowledge/documents/$id/replace',
        data: formData,
      );

      return KnowledgeDocument.fromJson(response.data);
    } catch (e) {
      throw _handleError(e, 'Erro ao substituir documento');
    }
  }

  Future<void> reprocessKnowledgeDocument(String id) async {
    try {
      await _dio.post('/api/knowledge/documents/$id/reprocess');
    } catch (e) {
      throw _handleError(e, 'Erro ao reprocessar documento');
    }
  }

  Future<Map<String, dynamic>> getKnowledgeHealth() async {
    try {
      final response = await _dio.get('/api/knowledge/health');
      return response.data as Map<String, dynamic>;
    } catch (e) {
      throw _handleError(e, 'Erro ao verificar health');
    }
  }

  Exception _handleError(dynamic error, String defaultMessage) {
    if (error is DioException && error.response?.data != null) {
      final data = error.response!.data;
      if (data is Map<String, dynamic>) {
        final msg = data['message'] ?? defaultMessage;
        final step = data['step']?.toString();
        final details = data['details']?.toString();
        final traceId = data['traceId']?.toString();

        if (step != null || details != null || traceId != null) {
          log('Knowledge API Error -> Step: $step | Details: $details | TraceId: $traceId',
              error: error, name: 'KnowledgeService');
        }

        return KnowledgeApiException(
          message: msg,
          step: step,
          details: details,
          traceId: traceId,
        );
      }
    }
    log('Knowledge API Unexpected Error', error: error, name: 'KnowledgeService');
    return Exception(defaultMessage);
  }
}
