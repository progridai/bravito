import '../../domain/entities/usuario.dart';
import '../../domain/entities/perfil_acesso.dart';
import '../../domain/repositories/usuarios_repository.dart';
import '../datasources/usuarios_remote_datasource.dart';
import '../models/criar_usuario_request_model.dart';
import '../models/editar_usuario_request_model.dart';

class UsuariosRepositoryImpl implements UsuariosRepository {
  final UsuariosRemoteDataSource remoteDataSource;

  UsuariosRepositoryImpl(this.remoteDataSource);

  @override
  Future<List<Usuario>> listarUsuarios() async {
    return await remoteDataSource.listarUsuarios();
  }

  @override
  Future<Usuario> obterUsuario(String id) async {
    return await remoteDataSource.obterUsuario(id);
  }

  @override
  Future<Usuario> criarUsuario(CriarUsuarioRequestModel request) async {
    return await remoteDataSource.criarUsuario(request);
  }

  @override
  Future<Usuario> editarUsuario(String id, EditarUsuarioRequestModel request) async {
    return await remoteDataSource.editarUsuario(id, request);
  }

  @override
  Future<Usuario> ativarUsuario(String id) async {
    return await remoteDataSource.ativarUsuario(id);
  }

  @override
  Future<Usuario> desativarUsuario(String id) async {
    return await remoteDataSource.desativarUsuario(id);
  }

  @override
  Future<List<PerfilAcesso>> listarPerfisDisponiveis() async {
    return await remoteDataSource.listarPerfisDisponiveis();
  }
}
