import '../../domain/entities/usuario.dart';

abstract class UsuariosState {}

class UsuariosInitial extends UsuariosState {}

class UsuariosLoading extends UsuariosState {}

class UsuariosLoaded extends UsuariosState {
  final List<Usuario> usuarios;
  UsuariosLoaded(this.usuarios);
}

class UsuariosError extends UsuariosState {
  final String message;
  UsuariosError(this.message);
}
