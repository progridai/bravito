import '../entities/usuario.dart';
import '../repositories/usuarios_repository.dart';

class DesativarUsuarioUseCase {
  final UsuariosRepository repository;

  DesativarUsuarioUseCase(this.repository);

  Future<Usuario> call(String id) async {
    return await repository.desativarUsuario(id);
  }
}
