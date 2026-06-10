import '../entities/usuario.dart';
import '../repositories/usuarios_repository.dart';

class AtivarUsuarioUseCase {
  final UsuariosRepository repository;

  AtivarUsuarioUseCase(this.repository);

  Future<Usuario> call(String id) async {
    return await repository.ativarUsuario(id);
  }
}
