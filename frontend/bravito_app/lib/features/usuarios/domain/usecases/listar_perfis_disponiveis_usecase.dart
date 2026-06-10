import '../entities/perfil_acesso.dart';
import '../repositories/usuarios_repository.dart';

class ListarPerfisDisponiveisUseCase {
  final UsuariosRepository repository;

  ListarPerfisDisponiveisUseCase(this.repository);

  Future<List<PerfilAcesso>> call() async {
    return await repository.listarPerfisDisponiveis();
  }
}
