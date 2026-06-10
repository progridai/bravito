import '../../domain/entities/usuario.dart';
import '../../domain/entities/perfil_acesso.dart';

abstract class UsuarioFormState {}

class UsuarioFormInitial extends UsuarioFormState {}

class UsuarioFormLoading extends UsuarioFormState {}

class UsuarioFormLoaded extends UsuarioFormState {
  final Usuario? usuario; // null se for criação
  final List<PerfilAcesso> perfisDisponiveis;

  UsuarioFormLoaded({this.usuario, required this.perfisDisponiveis});
}

class UsuarioFormSuccess extends UsuarioFormState {}

class UsuarioFormError extends UsuarioFormState {
  final String message;
  UsuarioFormError(this.message);
}
