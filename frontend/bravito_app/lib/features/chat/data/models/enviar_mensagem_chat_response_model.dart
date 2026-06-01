class EnviarMensagemChatResponseModel {
  final bool sucesso;
  final String? conversaId;
  final String resposta;
  final String? mensagemErro;
  final dynamic metadados;

  EnviarMensagemChatResponseModel({
    required this.sucesso,
    this.conversaId,
    required this.resposta,
    this.mensagemErro,
    this.metadados,
  });

  factory EnviarMensagemChatResponseModel.fromJson(Map<String, dynamic> json) {
    return EnviarMensagemChatResponseModel(
      sucesso: json['sucesso'] ?? false,
      conversaId: json['conversaId'],
      resposta: json['resposta'] ?? '',
      mensagemErro: json['mensagemErro'],
      metadados: json['metadados'],
    );
  }
}
