import '../../domain/entities/usuario.dart';

class UsuarioAdminModel extends Usuario {
  UsuarioAdminModel({
    required super.id,
    required super.nome,
    required super.email,
    required super.ativo,
    super.perfis = const [],
  });

  factory UsuarioAdminModel.fromJson(Map<String, dynamic> json) {
    return UsuarioAdminModel(
      id: json['id'] ?? '',
      nome: json['nome'] ?? '',
      email: json['email'] ?? '',
      ativo: json['ativo'] ?? false,
      perfis: (json['perfis'] as List<dynamic>?)?.map((e) => e.toString()).toList() ?? [],
    );
  }
}
