import '../../data/models/historico_chat_response_model.dart';
import '../repositories/chat_repository.dart';

class ObterHistoricoChatUseCase {
  final ChatRepository _repository;

  ObterHistoricoChatUseCase(this._repository);

  Future<HistoricoChatResponseModel> call() async {
    return await _repository.obterHistorico();
  }
}
