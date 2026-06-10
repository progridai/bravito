import 'package:dio/dio.dart';
import '../models/usuario_admin_model.dart';
import '../models/perfil_acesso_model.dart';
import '../models/criar_usuario_request_model.dart';
import '../models/editar_usuario_request_model.dart';

class UsuariosRemoteDataSource {
  final Dio dio;

  UsuariosRemoteDataSource(this.dio);

  Future<List<UsuarioAdminModel>> listarUsuarios() async {
    final response = await dio.get('/api/usuarios');
    return (response.data as List)
        .map((e) => UsuarioAdminModel.fromJson(e))
        .toList();
  }

  Future<UsuarioAdminModel> obterUsuario(String id) async {
    final response = await dio.get('/api/usuarios/$id');
    return UsuarioAdminModel.fromJson(response.data);
  }

  Future<UsuarioAdminModel> criarUsuario(CriarUsuarioRequestModel request) async {
    final response = await dio.post('/api/usuarios', data: request.toJson());
    return UsuarioAdminModel.fromJson(response.data);
  }

  Future<UsuarioAdminModel> editarUsuario(String id, EditarUsuarioRequestModel request) async {
    final response = await dio.put('/api/usuarios/$id', data: request.toJson());
    return UsuarioAdminModel.fromJson(response.data);
  }

  Future<UsuarioAdminModel> ativarUsuario(String id) async {
    final response = await dio.patch('/api/usuarios/$id/ativar');
    return UsuarioAdminModel.fromJson(response.data);
  }

  Future<UsuarioAdminModel> desativarUsuario(String id) async {
    final response = await dio.patch('/api/usuarios/$id/desativar');
    return UsuarioAdminModel.fromJson(response.data);
  }

  Future<List<PerfilAcessoModel>> listarPerfisDisponiveis() async {
    final response = await dio.get('/api/acesso/perfis'); // A rota criada no backend é /api/acesso/perfis (ou /api/usuarios/perfis-disponiveis se foi criada lá). A doc fala /api/usuarios/perfis-disponiveis no prompt mas no passo anterior criei AcessoController. Se for /api/acesso/perfis, uso essa. Pela doc da etapa anterior era /api/acesso/perfis. Vou tentar /api/acesso/perfis primeiro, mas caso de erro ajusto. O Prompt fala "GET /api/usuarios/perfis-disponiveis" e tbm "GET /api/acesso/perfis" (no anterior). Vou usar /api/acesso/perfis.
    return (response.data as List)
        .map((e) => PerfilAcessoModel.fromJson(e))
        .toList();
  }
}
