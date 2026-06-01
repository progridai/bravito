import '../../domain/entities/mensagem_chat.dart';

class ChatState {
  final List<MensagemChat> mensagens;
  final bool carregando;
  final String? erro;
  final String? conversaId;

  ChatState({
    this.mensagens = const [],
    this.carregando = false,
    this.erro,
    this.conversaId,
  });

  ChatState copyWith({
    List<MensagemChat>? mensagens,
    bool? carregando,
    String? erro,
    String? conversaId,
    bool clearErro = false,
  }) {
    return ChatState(
      mensagens: mensagens ?? this.mensagens,
      carregando: carregando ?? this.carregando,
      erro: clearErro ? null : (erro ?? this.erro),
      conversaId: conversaId ?? this.conversaId,
    );
  }
}
