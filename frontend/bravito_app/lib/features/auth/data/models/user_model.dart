import '../../domain/entities/user_entity.dart';

class UserModel extends UserEntity {
  UserModel({
    required super.id,
    required super.username,
    required super.email,
    required super.firstName,
    required super.lastName,
    super.perfis = const [],
    super.recursos = const [],
  });

  factory UserModel.fromJson(Map<String, dynamic> json) {
    return UserModel(
      id: json['usuarioId'] ?? json['id'] ?? json['UsuarioId'] ?? '',
      username: json['username'] ?? json['preferred_username'] ?? '',
      email: json['email'] ?? json['Email'] ?? '',
      firstName: json['nome'] ?? json['Nome'] ?? json['firstName'] ?? json['given_name'] ?? json['name'] ?? '',
      lastName: json['lastName'] ?? json['family_name'] ?? '',
      perfis: (json['perfis'] as List<dynamic>?)?.map((e) => e.toString()).toList() ?? [],
      recursos: (json['recursos'] as List<dynamic>?)?.map((e) => e.toString()).toList() ?? [],
    );
  }
}
