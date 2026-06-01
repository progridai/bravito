import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../features/auth/presentation/pages/login_page.dart';
import '../features/auth/presentation/controllers/auth_controller.dart';
import '../features/auth/presentation/controllers/auth_state.dart';
import '../features/chat/presentation/pages/chat_page.dart';

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
        return '/chat';
      }

      return null;
    },
    routes: [
      GoRoute(
        path: '/login',
        builder: (context, state) => const LoginPage(),
      ),
      GoRoute(
        path: '/chat',
        builder: (context, state) => const ChatPage(),
      ),
    ],
  );
});
