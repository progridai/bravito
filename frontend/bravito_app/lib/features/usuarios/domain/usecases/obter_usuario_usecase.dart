import '../entities/usuario.dart';
import '../repositories/usuarios_repository.dart';

class ObterUsuarioUseCase {
  final UsuariosRepository repository;

  ObterUsuarioUseCase(this.repository);

  Future<Usuario> call(String id) async {
    return await repository.obterUsuario(id);
  }
}
