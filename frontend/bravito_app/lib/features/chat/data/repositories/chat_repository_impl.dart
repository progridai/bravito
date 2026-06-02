import '../../domain/repositories/chat_repository.dart';
import '../datasources/chat_remote_data_source.dart';
import '../models/enviar_mensagem_chat_request_model.dart';
import '../models/enviar_mensagem_chat_response_model.dart';
import '../models/historico_chat_response_model.dart';

class ChatRepositoryImpl implements ChatRepository {
  final ChatRemoteDataSource _remoteDataSource;

  ChatRepositoryImpl(this._remoteDataSource);

  @override
  Future<EnviarMensagemChatResponseModel> enviarMensagem(EnviarMensagemChatRequestModel request) async {
    return await _remoteDataSource.enviarMensagem(request);
  }

  @override
  Future<HistoricoChatResponseModel> obterHistorico() async {
    return await _remoteDataSource.obterHistorico();
  }
}
