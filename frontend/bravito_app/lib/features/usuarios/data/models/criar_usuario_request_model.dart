class CriarUsuarioRequestModel {
  final String nome;
  final String email;
  final String senhaTemporaria;
  final bool ativo;
  final List<String> perfilIds;

  CriarUsuarioRequestModel({
    required this.nome,
    required this.email,
    required this.senhaTemporaria,
    required this.ativo,
    required this.perfilIds,
  });

  Map<String, dynamic> toJson() {
    return {
      'nome': nome,
      'email': email,
      'senhaTemporaria': senhaTemporaria,
      'ativo': ativo,
      'perfilIds': perfilIds,
    };
  }
}
