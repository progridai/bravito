class EditarUsuarioRequestModel {
  final String nome;
  final String username;
  final String email;
  final bool ativo;
  final List<String> perfilIds;

  EditarUsuarioRequestModel({
    required this.nome,
    required this.username,
    required this.email,
    required this.ativo,
    required this.perfilIds,
  });

  Map<String, dynamic> toJson() {
    return {
      'nome': nome,
      'username': username,
      'email': email,
      'ativo': ativo,
      'perfilIds': perfilIds,
    };
  }
}
