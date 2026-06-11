import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:dio/dio.dart';
import '../../data/models/criar_usuario_request_model.dart';
import '../../data/models/editar_usuario_request_model.dart';
import '../../domain/usecases/obter_usuario_usecase.dart';
import '../../domain/usecases/criar_usuario_usecase.dart';
import '../../domain/usecases/editar_usuario_usecase.dart';
import '../../domain/usecases/listar_perfis_disponiveis_usecase.dart';
import 'usuarios_controller.dart';
import 'usuario_form_state.dart';

final obterUsuarioUseCaseProvider = Provider((ref) {
  return ObterUsuarioUseCase(ref.watch(usuariosRepositoryProvider));
});

final criarUsuarioUseCaseProvider = Provider((ref) {
  return CriarUsuarioUseCase(ref.watch(usuariosRepositoryProvider));
});

final editarUsuarioUseCaseProvider = Provider((ref) {
  return EditarUsuarioUseCase(ref.watch(usuariosRepositoryProvider));
});

final listarPerfisDisponiveisUseCaseProvider = Provider((ref) {
  return ListarPerfisDisponiveisUseCase(ref.watch(usuariosRepositoryProvider));
});

class UsuarioFormController extends Notifier<UsuarioFormState> {
  late ObterUsuarioUseCase _obterUsuario;
  late CriarUsuarioUseCase _criarUsuario;
  late EditarUsuarioUseCase _editarUsuario;
  late ListarPerfisDisponiveisUseCase _listarPerfisDisponiveis;

  @override
  UsuarioFormState build() {
    _obterUsuario = ref.watch(obterUsuarioUseCaseProvider);
    _criarUsuario = ref.watch(criarUsuarioUseCaseProvider);
    _editarUsuario = ref.watch(editarUsuarioUseCaseProvider);
    _listarPerfisDisponiveis = ref.watch(listarPerfisDisponiveisUseCaseProvider);
    return UsuarioFormInitial();
  }

  Future<void> inicializarFormulario({String? usuarioId}) async {
    state = UsuarioFormLoading();
    try {
      final perfis = await _listarPerfisDisponiveis();
      
      if (usuarioId != null) {
        final usuario = await _obterUsuario(usuarioId);
        state = UsuarioFormLoaded(usuario: usuario, perfisDisponiveis: perfis);
      } else {
        state = UsuarioFormLoaded(usuario: null, perfisDisponiveis: perfis);
      }
    } on DioException catch (e) {
      if (e.response?.statusCode == 403) {
        state = UsuarioFormError('Você não possui permissão para executar esta ação.');
      } else {
        state = UsuarioFormError('Não foi possível carregar os dados do formulário.');
      }
    } catch (e) {
      state = UsuarioFormError('Não foi possível carregar os dados do formulário.');
    }
  }

  Future<String?> salvarUsuario({
    String? id,
    required String nome,
    required String username,
    required String email,
    required String senhaTemporaria,
    required bool ativo,
    required List<String> perfilIds,
  }) async {
    UsuarioFormLoaded? previousState;
    if (state is UsuarioFormLoaded) {
      previousState = state as UsuarioFormLoaded;
    }
    state = UsuarioFormLoading();
    try {
      if (id == null) {
        await _criarUsuario(CriarUsuarioRequestModel(
          nome: nome,
          username: username,
          email: email,
          senhaTemporaria: senhaTemporaria,
          ativo: ativo,
          perfilIds: perfilIds,
        ));
      } else {
        await _editarUsuario(id, EditarUsuarioRequestModel(
          nome: nome,
          username: username,
          email: email,
          ativo: ativo,
          perfilIds: perfilIds,
        ));
      }
      
      // Refresh list
      ref.read(usuariosControllerProvider.notifier).carregarUsuarios();
      
      state = UsuarioFormSuccess();
      return null;
    } on DioException catch (e) {
      final data = e.response?.data;
      String errorMessage = 'Não foi possível salvar o usuário. Tente novamente.';
      
      if (data is Map && data.containsKey('erro')) {
        errorMessage = data['erro'];
      } else if (e.response?.statusCode == 403) {
        errorMessage = 'Você não possui permissão para executar esta ação.';
      } else if (e.response?.statusCode == 409) {
        errorMessage = 'Já existe um usuário cadastrado com este e-mail.';
      }

      if (previousState != null) {
        state = previousState;
      } else {
        state = UsuarioFormError(errorMessage);
      }
      return errorMessage;
    } catch (e) {
      String errorMessage = 'Não foi possível salvar o usuário. Tente novamente.';
      if (previousState != null) {
        state = previousState;
      } else {
        state = UsuarioFormError(errorMessage);
      }
      return errorMessage;
    }
  }
}

final usuarioFormControllerProvider = NotifierProvider<UsuarioFormController, UsuarioFormState>(() {
  return UsuarioFormController();
});
