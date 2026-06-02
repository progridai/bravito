import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:uuid/uuid.dart';

import '../../../../core/http/dio_client.dart';
import '../../data/datasources/chat_remote_data_source.dart';
import '../../data/models/enviar_mensagem_chat_request_model.dart';
import '../../data/repositories/chat_repository_impl.dart';
import '../../domain/entities/mensagem_chat.dart';
import '../../domain/entities/tipo_remetente.dart';
import '../../domain/repositories/chat_repository.dart';
import '../../domain/usecases/enviar_mensagem_chat_usecase.dart';
import '../../domain/usecases/obter_historico_chat_usecase.dart';
import 'chat_state.dart';

final chatRemoteDataSourceProvider = Provider((ref) {
  final dio = ref.watch(dioClientProvider).dio;
  return ChatRemoteDataSource(dio);
});

final chatRepositoryProvider = Provider<ChatRepository>((ref) {
  return ChatRepositoryImpl(ref.watch(chatRemoteDataSourceProvider));
});

final enviarMensagemChatUseCaseProvider = Provider((ref) {
  return EnviarMensagemChatUseCase(ref.watch(chatRepositoryProvider));
});

final obterHistoricoChatUseCaseProvider = Provider((ref) {
  return ObterHistoricoChatUseCase(ref.watch(chatRepositoryProvider));
});

class ChatController extends Notifier<ChatState> {
  late EnviarMensagemChatUseCase _enviarMensagemUseCase;
  late ObterHistoricoChatUseCase _obterHistoricoUseCase;
  final _uuid = const Uuid();

  @override
  ChatState build() {
    _enviarMensagemUseCase = ref.watch(enviarMensagemChatUseCaseProvider);
    _obterHistoricoUseCase = ref.watch(obterHistoricoChatUseCaseProvider);
    return ChatState();
  }

  Future<void> carregarHistorico() async {
    try {
      state = state.copyWith(carregando: true, clearErro: true);
      
      final response = await _obterHistoricoUseCase();
      if (response.sucesso) {
        final mensagensHistorico = response.mensagens.map((e) => e.toEntity()).toList();
        
        state = state.copyWith(
          mensagens: mensagensHistorico,
          conversaId: response.conversaId ?? state.conversaId,
          carregando: false,
        );
      } else {
        state = state.copyWith(carregando: false);
      }
    } catch (e) {
      state = state.copyWith(
        carregando: false,
        erro: _formatarErro(e),
      );
    }
  }

  Future<void> enviarMensagem(String texto) async {
    if (texto.trim().isEmpty) return;

    final mensagemUsuarioId = _uuid.v4();
    final novaMensagem = MensagemChat(
      id: mensagemUsuarioId,
      texto: texto,
      tipoRemetente: TipoRemetente.usuario,
      dataHora: DateTime.now(),
    );

    // Adiciona a mensagem do usuário à lista e inicia o loading
    state = state.copyWith(
      mensagens: [...state.mensagens, novaMensagem],
      carregando: true,
      clearErro: true,
    );

    try {
      final request = EnviarMensagemChatRequestModel(
        conversaId: state.conversaId,
        mensagem: texto,
      );

      final response = await _enviarMensagemUseCase(request);

      if (response.sucesso) {
        final mensagemAssistente = MensagemChat(
          id: _uuid.v4(),
          texto: response.resposta,
          tipoRemetente: TipoRemetente.assistente,
          dataHora: DateTime.now(),
        );

        state = state.copyWith(
          mensagens: [...state.mensagens, mensagemAssistente],
          carregando: false,
          conversaId: response.conversaId ?? state.conversaId,
        );
      } else {
        _marcarMensagemComErro(mensagemUsuarioId, response.mensagemErro ?? 'Erro ao processar mensagem.');
      }
    } catch (e) {
      _marcarMensagemComErro(mensagemUsuarioId, _formatarErro(e));
    }
  }

  void _marcarMensagemComErro(String id, String erro) {
    final novasMensagens = state.mensagens.map((msg) {
      if (msg.id == id) {
        return msg.copyWith(erro: erro);
      }
      return msg;
    }).toList();

    // Também podemos adicionar uma mensagem de sistema para avisar do erro
    final msgSistema = MensagemChat(
      id: _uuid.v4(),
      texto: erro,
      tipoRemetente: TipoRemetente.sistema,
      dataHora: DateTime.now(),
    );

    state = state.copyWith(
      mensagens: [...novasMensagens, msgSistema],
      carregando: false,
    );
  }

  String _formatarErro(dynamic e) {
    String msg = e.toString();
    if (msg.startsWith('Exception: ')) {
      return msg.replaceFirst('Exception: ', '');
    }
    return 'Ocorreu um erro inesperado de comunicação.';
  }
}

final chatControllerProvider = NotifierProvider<ChatController, ChatState>(() {
  return ChatController();
});
