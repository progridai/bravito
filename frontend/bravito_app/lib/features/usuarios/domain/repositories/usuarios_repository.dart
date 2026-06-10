import '../../domain/entities/usuario.dart';
import '../../domain/entities/perfil_acesso.dart';
import '../models/criar_usuario_request_model.dart';
import '../models/editar_usuario_request_model.dart';

abstract class UsuariosRepository {
  Future<List<Usuario>> listarUsuarios();
  Future<Usuario> obterUsuario(String id);
  Future<Usuario> criarUsuario(CriarUsuarioRequestModel request);
  Future<Usuario> editarUsuario(String id, EditarUsuarioRequestModel request);
  Future<Usuario> ativarUsuario(String id);
  Future<Usuario> desativarUsuario(String id);
  Future<List<PerfilAcesso>> listarPerfisDisponiveis();
}
