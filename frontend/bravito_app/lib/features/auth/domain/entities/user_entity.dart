class UserEntity {
  final String id;
  final String username;
  final String email;
  final String firstName;
  final String lastName;
  final List<String> perfis;
  final List<String> recursos;

  UserEntity({
    required this.id,
    required this.username,
    required this.email,
    required this.firstName,
    required this.lastName,
    this.perfis = const [],
    this.recursos = const [],
  });

  bool possuiRecurso(String codigo) {
    return recursos.contains(codigo);
  }
}
