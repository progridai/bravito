import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../../../core/theme/app_theme.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../core/security/recursos_app.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../../../shared/widgets/bravito_primary_button.dart';
import '../../../auth/presentation/controllers/auth_controller.dart';
import '../../../auth/presentation/controllers/auth_state.dart';

class HomePage extends ConsumerWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final authState = ref.watch(authControllerProvider);
    String userName = 'Usuário';
    bool temAcessoChat = false;
    bool possuiRecursos = false;

    if (authState is AuthAuthenticated) {
      userName = authState.user.firstName;
      temAcessoChat = authState.user.possuiRecurso(RecursosApp.chatAcessar);
      possuiRecursos = authState.user.recursos.isNotEmpty;
    }

    return BravitoAppScaffold(
      title: 'Início',
      actions: [
        IconButton(
          icon: const Icon(Icons.logout),
          onPressed: () {
            ref.read(authControllerProvider.notifier).logout();
          },
        ),
      ],
      body: Padding(
        padding: const EdgeInsets.all(AppSpacing.md),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Icon(
              Icons.pets, 
              size: 80,
              color: AppColors.azulPrincipal,
            ),
            const SizedBox(height: AppSpacing.lg),
            Text(
              'Olá, $userName!',
              style: Theme.of(context).textTheme.headlineSmall?.copyWith(
                    color: AppColors.azulPrincipal,
                    fontWeight: FontWeight.bold,
                  ),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: AppSpacing.sm),
            Text(
              possuiRecursos 
                ? 'Bem-vindo ao Bravito. O que você deseja fazer hoje?'
                : 'Seu usuário ainda não possui permissões configuradas. Entre em contato com o administrador.',
              style: Theme.of(context).textTheme.bodyMedium,
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: AppSpacing.xl),
            if (temAcessoChat) ...[
              BravitoPrimaryButton(
                text: 'Abrir Chat',
                onPressed: () {
                  context.push('/chat');
                },
              ),
              const SizedBox(height: AppSpacing.md),
            ],
            OutlinedButton(
              onPressed: () {
                context.push('/menu');
              },
              style: OutlinedButton.styleFrom(
                foregroundColor: AppColors.azulPrincipal,
                side: const BorderSide(color: AppColors.azulPrincipal, width: 2),
                padding: const EdgeInsets.symmetric(vertical: 16),
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(8),
                ),
              ),
              child: const Text(
                'Abrir Menu',
                style: TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
