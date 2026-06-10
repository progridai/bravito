import '../entities/usuario.dart';
import '../repositories/usuarios_repository.dart';

class ListarUsuariosUseCase {
  final UsuariosRepository repository;

  ListarUsuariosUseCase(this.repository);

  Future<List<Usuario>> call() async {
    return await repository.listarUsuarios();
  }
}
