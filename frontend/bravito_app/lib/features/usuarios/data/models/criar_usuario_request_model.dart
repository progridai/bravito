class CriarUsuarioRequestModel {
  final String nome;
  final String username;
  final String email;
  final String senhaTemporaria;
  final bool ativo;
  final List<String> perfilIds;

  CriarUsuarioRequestModel({
    required this.nome,
    required this.username,
    required this.email,
    required this.senhaTemporaria,
    required this.ativo,
    required this.perfilIds,
  });

  Map<String, dynamic> toJson() {
    return {
      'nome': nome,
      'username': username,
      'email': email,
      'senhaTemporaria': senhaTemporaria,
      'ativo': ativo,
      'perfilIds': perfilIds,
    };
  }
}
