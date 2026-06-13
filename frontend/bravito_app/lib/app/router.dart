import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../features/auth/presentation/pages/login_page.dart';
import '../features/auth/presentation/controllers/auth_controller.dart';
import '../features/auth/presentation/controllers/auth_state.dart';
import '../features/chat/presentation/pages/chat_page.dart';
import '../features/home/presentation/pages/home_page.dart';
import '../features/menu/presentation/pages/menu_page.dart';
import '../features/menu/presentation/pages/alterar_senha_page.dart';
import '../features/menu/presentation/pages/conversas_page.dart';
import '../features/usuarios/presentation/pages/usuarios_page.dart';
import '../features/usuarios/presentation/pages/usuario_form_page.dart';
import '../features/base_conhecimento/presentation/pages/base_conhecimento_page.dart';
import '../shared/pages/acesso_negado_page.dart';
import '../core/security/recursos_app.dart';

final routerProvider = Provider<GoRouter>((ref) {
  final authState = ref.watch(authControllerProvider);

  return GoRouter(
    initialLocation: '/login',
    redirect: (context, state) {
      final isAuth = authState is AuthAuthenticated;
      final isLoginRoute = state.matchedLocation == '/login';

      if (authState is AuthInitial || authState is AuthLoading) {
        // Se estiver carregando, podemos manter na rota atual ou exibir splash (no momento mantemos no login)
        return null;
      }

      if (!isAuth && !isLoginRoute) {
        return '/login';
      }

      if (isAuth && isLoginRoute) {
        return '/home';
      }

      if (isAuth) {
        final user = (authState as AuthAuthenticated).user;
        final path = state.matchedLocation;

        if (path == '/chat' && !user.possuiRecurso(RecursosApp.chatAcessar)) {
          return '/acesso-negado';
        }
        if (path == '/menu/conversas' && !user.possuiRecurso(RecursosApp.conversasVisualizar)) {
          return '/acesso-negado';
        }
        if (path.startsWith('/menu/usuarios')) {
          if (!user.possuiRecurso(RecursosApp.usuariosVisualizar)) {
            return '/acesso-negado';
          }
          if (path == '/menu/usuarios/form' && !user.possuiRecurso(RecursosApp.usuariosCadastrar)) {
            return '/acesso-negado';
          }
          if (path.startsWith('/menu/usuarios/form/') && !user.possuiRecurso(RecursosApp.usuariosEditar)) {
            return '/acesso-negado';
          }
        }
      }

      return null;
    },
    routes: [
      GoRoute(
        path: '/login',
        builder: (context, state) => const LoginPage(),
      ),
      GoRoute(
        path: '/acesso-negado',
        builder: (context, state) => const AcessoNegadoPage(),
      ),
      GoRoute(
        path: '/chat',
        builder: (context, state) => const ChatPage(),
      ),
      GoRoute(
        path: '/home',
        builder: (context, state) => const HomePage(),
      ),
      GoRoute(
        path: '/menu',
        builder: (context, state) => const MenuPage(),
      ),
      GoRoute(
        path: '/menu/alterar-senha',
        builder: (context, state) => const AlterarSenhaPage(),
      ),
      GoRoute(
        path: '/menu/conversas',
        builder: (context, state) => const ConversasPage(),
      ),
      GoRoute(
        path: '/menu/usuarios',
        builder: (context, state) => const UsuariosPage(),
      ),
      GoRoute(
        path: '/menu/usuarios/form',
        builder: (context, state) => const UsuarioFormPage(),
      ),
      GoRoute(
        path: '/menu/usuarios/form/:id',
        builder: (context, state) {
          final id = state.pathParameters['id'];
          return UsuarioFormPage(usuarioId: id);
        },
      ),
      GoRoute(
        path: '/menu/base-conhecimento',
        builder: (context, state) => const BaseConhecimentoPage(),
      ),
    ],
  );
});
