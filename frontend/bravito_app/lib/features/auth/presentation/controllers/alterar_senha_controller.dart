import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../domain/usecases/alterar_senha_usecase.dart';
import 'auth_controller.dart';

final alterarSenhaUseCaseProvider = Provider((ref) {
  return AlterarSenhaUseCase(ref.watch(authRepositoryProvider));
});

class AlterarSenhaState {
  final bool carregando;
  final String? erro;
  final bool sucesso;

  AlterarSenhaState({
    this.carregando = false,
    this.erro,
    this.sucesso = false,
  });

  AlterarSenhaState copyWith({
    bool? carregando,
    String? erro,
    bool? sucesso,
    bool clearErro = false,
  }) {
    return AlterarSenhaState(
      carregando: carregando ?? this.carregando,
      erro: clearErro ? null : (erro ?? this.erro),
      sucesso: sucesso ?? this.sucesso,
    );
  }
}

class AlterarSenhaController extends Notifier<AlterarSenhaState> {
  late AlterarSenhaUseCase _alterarSenhaUseCase;

  @override
  AlterarSenhaState build() {
    _alterarSenhaUseCase = ref.watch(alterarSenhaUseCaseProvider);
    return AlterarSenhaState();
  }

  Future<void> alterarSenha(String novaSenha) async {
    state = state.copyWith(carregando: true, clearErro: true);
    try {
      await _alterarSenhaUseCase(novaSenha);
      state = state.copyWith(carregando: false, sucesso: true);
    } catch (e) {
      state = state.copyWith(
        carregando: false,
        erro: e.toString().replaceFirst('Exception: ', ''),
      );
    }
  }
}

final alterarSenhaControllerProvider = NotifierProvider<AlterarSenhaController, AlterarSenhaState>(() {
  return AlterarSenhaController();
});
