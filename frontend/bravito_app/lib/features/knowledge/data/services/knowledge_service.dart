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
    if (error is DioException) {
      if (error.response?.data != null) {
        final data = error.response!.data;
        if (data is Map<String, dynamic>) {
          // Extrai campos padronizados do ASP.NET Core e do nosso padrao customizado
          final msg = data['message'] ?? data['title'] ?? defaultMessage;
          final step = data['step']?.toString();
          final details = data['details']?.toString() ?? data['detail']?.toString();
          final traceId = data['traceId']?.toString();
          final errors = data['errors']; // ASP.NET validation errors

          String extraInfo = '';
          if (details != null) extraInfo += ' - $details';
          if (errors != null) extraInfo += ' | Erros: $errors';

          // Se nao tivermos nem message customizada nem title do ASP.NET,
          // significa que e um JSON desconhecido. Vamos jogar o JSON todo na tela.
          if (data['message'] == null && data['title'] == null) {
            extraInfo += ' | Raw JSON: $data';
          }

          final fullMessage = '$msg$extraInfo (Status: ${error.response?.statusCode})';

          if (step != null || details != null || traceId != null) {
            print('Knowledge API Error -> Step: $step | Details: $details | TraceId: $traceId | Error: $error');
          }

          return KnowledgeApiException(
            message: fullMessage,
            step: step,
            details: details,
            traceId: traceId,
          );
        } else {
          // O backend retornou algo, mas não é o JSON esperado (ex: string simples)
          return Exception('$defaultMessage: Resposta não-JSON: "${error.response?.data}" (Status: ${error.response?.statusCode})');
        }
      }
      
      // Sem dados na resposta (ex: CORS, timeout, servidor fora do ar)
      return Exception('$defaultMessage: ${error.message} (Status: ${error.response?.statusCode})');
    }
    
    print('Knowledge API Unexpected Error: $error');
    String errStr = error?.toString() ?? 'null';
    if (errStr.trim().isEmpty) errStr = '<Erro vazio>';
    return Exception('$defaultMessage: [$errStr]');
  }
}
