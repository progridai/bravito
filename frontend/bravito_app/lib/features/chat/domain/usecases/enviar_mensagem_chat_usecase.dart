import '../../data/models/enviar_mensagem_chat_request_model.dart';
import '../../data/models/enviar_mensagem_chat_response_model.dart';
import '../repositories/chat_repository.dart';

class EnviarMensagemChatUseCase {
  final ChatRepository _repository;

  EnviarMensagemChatUseCase(this._repository);

  Future<EnviarMensagemChatResponseModel> call(EnviarMensagemChatRequestModel request) async {
    if (request.mensagem.trim().isEmpty) {
      throw Exception('A mensagem não pode estar vazia.');
    }
    return await _repository.enviarMensagem(request);
  }
}
