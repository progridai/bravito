class Usuario {
  final String id;
  final String nome;
  final String username;
  final String email;
  final bool ativo;
  final List<String> perfis;

  Usuario({
    required this.id,
    required this.nome,
    required this.username,
    required this.email,
    required this.ativo,
    this.perfis = const [],
  });
}
