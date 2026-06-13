import '../repositories/auth_repository.dart';

class AlterarSenhaUseCase {
  final AuthRepository _repository;

  AlterarSenhaUseCase(this._repository);

  Future<void> call(String novaSenha) async {
    return await _repository.alterarSenha(novaSenha);
  }
}
