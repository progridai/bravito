import '../entities/usuario.dart';
import '../repositories/usuarios_repository.dart';
import '../../data/models/criar_usuario_request_model.dart';

class CriarUsuarioUseCase {
  final UsuariosRepository repository;

  CriarUsuarioUseCase(this.repository);

  Future<Usuario> call(CriarUsuarioRequestModel request) async {
    return await repository.criarUsuario(request);
  }
}
