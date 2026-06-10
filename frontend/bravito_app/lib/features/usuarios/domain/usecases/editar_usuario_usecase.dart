import '../entities/usuario.dart';
import '../repositories/usuarios_repository.dart';
import '../../data/models/editar_usuario_request_model.dart';

class EditarUsuarioUseCase {
  final UsuariosRepository repository;

  EditarUsuarioUseCase(this.repository);

  Future<Usuario> call(String id, EditarUsuarioRequestModel request) async {
    return await repository.editarUsuario(id, request);
  }
}
