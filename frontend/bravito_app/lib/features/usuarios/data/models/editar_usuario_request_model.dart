class EditarUsuarioRequestModel {
  final String nome;
  final String email;
  final bool ativo;
  final List<String> perfilIds;

  EditarUsuarioRequestModel({
    required this.nome,
    required this.email,
    required this.ativo,
    required this.perfilIds,
  });

  Map<String, dynamic> toJson() {
    return {
      'nome': nome,
      'email': email,
      'ativo': ativo,
      'perfilIds': perfilIds,
    };
  }
}
