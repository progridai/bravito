import '../../data/models/enviar_mensagem_chat_request_model.dart';
import '../../data/models/enviar_mensagem_chat_response_model.dart';

abstract class ChatRepository {
  Future<EnviarMensagemChatResponseModel> enviarMensagem(EnviarMensagemChatRequestModel request);
}
