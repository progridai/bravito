import '../../domain/entities/perfil_acesso.dart';

class PerfilAcessoModel extends PerfilAcesso {
  PerfilAcessoModel({
    required super.id,
    required super.nome,
    super.descricao,
  });

  factory PerfilAcessoModel.fromJson(Map<String, dynamic> json) {
    return PerfilAcessoModel(
      id: json['id'] ?? '',
      nome: json['nome'] ?? '',
      descricao: json['descricao'],
    );
  }
}
