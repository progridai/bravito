class EnviarMensagemChatRequestModel {
  final String? conversaId;
  final String mensagem;

  EnviarMensagemChatRequestModel({
    this.conversaId,
    required this.mensagem,
  });

  Map<String, dynamic> toJson() {
    return {
      'conversaId': conversaId,
      'mensagem': mensagem,
    };
  }
}
