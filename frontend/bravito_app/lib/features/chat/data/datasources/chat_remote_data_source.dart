import 'package:dio/dio.dart';
import '../models/enviar_mensagem_chat_request_model.dart';
import '../models/enviar_mensagem_chat_response_model.dart';
import '../models/historico_chat_response_model.dart';
class ChatRemoteDataSource {
  final Dio _dio;

  ChatRemoteDataSource(this._dio);

  Future<EnviarMensagemChatResponseModel> enviarMensagem(EnviarMensagemChatRequestModel request) async {
    try {
      final response = await _dio.post(
        '/api/chat/enviar',
        data: request.toJson(),
      );

      return EnviarMensagemChatResponseModel.fromJson(response.data);
    } on DioException catch (e) {
      if (e.response?.statusCode == 401) {
        throw Exception('Não autorizado. Por favor, faça login novamente.');
      }
      
      final errorMessage = e.response?.data?['erro'] ?? 'Erro de comunicação com o servidor.';
      throw Exception(errorMessage);
    } catch (e) {
      throw Exception('Erro inesperado: $e');
    }
  }

  Future<HistoricoChatResponseModel> obterHistorico() async {
    try {
      final response = await _dio.get('/api/chat/historico');
      return HistoricoChatResponseModel.fromJson(response.data);
    } on DioException catch (e) {
      if (e.response?.statusCode == 401) {
        throw Exception('Não autorizado. Por favor, faça login novamente.');
      }
      
      final errorMessage = e.response?.data?['erro'] ?? 'Erro ao buscar histórico.';
      throw Exception(errorMessage);
    } catch (e) {
      throw Exception('Erro inesperado: $e');
    }
  }
}
