import 'tipo_remetente.dart';

class MensagemChat {
  final String id;
  final String texto;
  final TipoRemetente tipoRemetente;
  final DateTime dataHora;
  final bool carregando;
  final String? erro;

  MensagemChat({
    required this.id,
    required this.texto,
    required this.tipoRemetente,
    required this.dataHora,
    this.carregando = false,
    this.erro,
  });

  MensagemChat copyWith({
    String? id,
    String? texto,
    TipoRemetente? tipoRemetente,
    DateTime? dataHora,
    bool? carregando,
    String? erro,
  }) {
    return MensagemChat(
      id: id ?? this.id,
      texto: texto ?? this.texto,
      tipoRemetente: tipoRemetente ?? this.tipoRemetente,
      dataHora: dataHora ?? this.dataHora,
      carregando: carregando ?? this.carregando,
      erro: erro ?? this.erro,
    );
  }
}
