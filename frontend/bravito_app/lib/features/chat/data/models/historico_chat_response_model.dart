import '../../domain/entities/mensagem_chat.dart';
import '../../domain/entities/tipo_remetente.dart';

class MensagemChatDtoModel {
  final String id;
  final String tipoRemetente;
  final String conteudo;
  final DateTime dataCriacao;

  MensagemChatDtoModel({
    required this.id,
    required this.tipoRemetente,
    required this.conteudo,
    required this.dataCriacao,
  });

  factory MensagemChatDtoModel.fromJson(Map<String, dynamic> json) {
    return MensagemChatDtoModel(
      id: json['id'] ?? '',
      tipoRemetente: json['tipoRemetente'] ?? '',
      conteudo: json['conteudo'] ?? '',
      dataCriacao: json['dataCriacao'] != null 
          ? DateTime.parse(json['dataCriacao']) 
          : DateTime.now(),
    );
  }

  MensagemChat toEntity() {
    TipoRemetente remetente;
    switch (tipoRemetente.toLowerCase()) {
      case 'assistente':
        remetente = TipoRemetente.assistente;
        break;
      case 'sistema':
        remetente = TipoRemetente.sistema;
        break;
      case 'usuario':
      default:
        remetente = TipoRemetente.usuario;
        break;
    }

    return MensagemChat(
      id: id,
      texto: conteudo,
      tipoRemetente: remetente,
      dataHora: dataCriacao,
    );
  }
}

class HistoricoChatResponseModel {
  final bool sucesso;
  final String? conversaId;
  final List<MensagemChatDtoModel> mensagens;

  HistoricoChatResponseModel({
    required this.sucesso,
    this.conversaId,
    required this.mensagens,
  });

  factory HistoricoChatResponseModel.fromJson(Map<String, dynamic> json) {
    return HistoricoChatResponseModel(
      sucesso: json['sucesso'] ?? false,
      conversaId: json['conversaId'],
      mensagens: (json['mensagens'] as List<dynamic>?)
              ?.map((e) => MensagemChatDtoModel.fromJson(e as Map<String, dynamic>))
              .toList() ??
          [],
    );
  }
}
