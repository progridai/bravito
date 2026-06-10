import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../../core/http/dio_client.dart';
import '../../data/datasources/usuarios_remote_datasource.dart';
import '../../data/repositories/usuarios_repository_impl.dart';
import '../../domain/repositories/usuarios_repository.dart';
import '../../domain/usecases/listar_usuarios_usecase.dart';
import '../../domain/usecases/ativar_usuario_usecase.dart';
import '../../domain/usecases/desativar_usuario_usecase.dart';
import 'usuarios_state.dart';
import 'package:dio/dio.dart';

final usuariosRemoteDataSourceProvider = Provider((ref) {
  return UsuariosRemoteDataSource(ref.watch(dioClientProvider).dio);
});

final usuariosRepositoryProvider = Provider<UsuariosRepository>((ref) {
  return UsuariosRepositoryImpl(ref.watch(usuariosRemoteDataSourceProvider));
});

final listarUsuariosUseCaseProvider = Provider((ref) {
  return ListarUsuariosUseCase(ref.watch(usuariosRepositoryProvider));
});

final ativarUsuarioUseCaseProvider = Provider((ref) {
  return AtivarUsuarioUseCase(ref.watch(usuariosRepositoryProvider));
});

final desativarUsuarioUseCaseProvider = Provider((ref) {
  return DesativarUsuarioUseCase(ref.watch(usuariosRepositoryProvider));
});

class UsuariosController extends Notifier<UsuariosState> {
  late ListarUsuariosUseCase _listarUsuarios;
  late AtivarUsuarioUseCase _ativarUsuario;
  late DesativarUsuarioUseCase _desativarUsuario;

  @override
  UsuariosState build() {
    _listarUsuarios = ref.watch(listarUsuariosUseCaseProvider);
    _ativarUsuario = ref.watch(ativarUsuarioUseCaseProvider);
    _desativarUsuario = ref.watch(desativarUsuarioUseCaseProvider);
    return UsuariosInitial();
  }

  Future<void> carregarUsuarios() async {
    state = UsuariosLoading();
    try {
      final usuarios = await _listarUsuarios();
      state = UsuariosLoaded(usuarios);
    } on DioException catch (e) {
      if (e.response?.statusCode == 403) {
        state = UsuariosError('Você não possui permissão para executar esta ação.');
      } else {
        state = UsuariosError('Não foi possível carregar os usuários. Tente novamente.');
      }
    } catch (e) {
      state = UsuariosError('Não foi possível carregar os usuários. Tente novamente.');
    }
  }

  Future<bool> ativarUsuario(String id) async {
    try {
      await _ativarUsuario(id);
      await carregarUsuarios(); // recarrega a lista
      return true;
    } on DioException catch (e) {
      if (e.response?.statusCode == 403) {
        state = UsuariosError('Você não possui permissão para executar esta ação.');
      } else {
        state = UsuariosError('Não foi possível ativar o usuário.');
      }
      return false;
    } catch (e) {
      state = UsuariosError('Não foi possível ativar o usuário.');
      return false;
    }
  }

  Future<bool> desativarUsuario(String id) async {
    try {
      await _desativarUsuario(id);
      await carregarUsuarios(); // recarrega a lista
      return true;
    } on DioException catch (e) {
      if (e.response?.statusCode == 403) {
        state = UsuariosError('Você não possui permissão para executar esta ação.');
      } else {
        state = UsuariosError('Não foi possível desativar o usuário.');
      }
      return false;
    } catch (e) {
      state = UsuariosError('Não foi possível desativar o usuário.');
      return false;
    }
  }
}

final usuariosControllerProvider = NotifierProvider<UsuariosController, UsuariosState>(() {
  return UsuariosController();
});
