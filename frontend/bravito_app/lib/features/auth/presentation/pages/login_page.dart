import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../core/constants/app_text_styles.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../../../shared/widgets/bravito_card.dart';
import '../../../../shared/widgets/bravito_primary_button.dart';
import '../controllers/auth_controller.dart';
import '../controllers/auth_state.dart';

class LoginPage extends ConsumerWidget {
  const LoginPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final authState = ref.watch(authControllerProvider);

    // Redirecionamento é melhor lidado no router, mas podemos reagir a erros aqui
    ref.listen<AuthState>(authControllerProvider, (previous, next) {
      if (next is AuthError) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text(next.message),
            backgroundColor: AppColors.error,
          ),
        );
      }
    });

    final isLoading = authState is AuthLoading;

    return BravitoAppScaffold(
      body: Center(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(AppSpacing.lg),
          child: ConstrainedBox(
            constraints: const BoxConstraints(maxWidth: 400),
            child: BravitoCard(
              padding: const EdgeInsets.all(AppSpacing.xl),
              child: Column(
                mainAxisSize: MainAxisSize.min,
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  const Icon(
                    Icons.smart_toy_rounded,
                    size: 64,
                    color: AppColors.primaryBlue,
                  ),
                  const SizedBox(height: AppSpacing.md),
                  const Text(
                    'Bravito',
                    textAlign: TextAlign.center,
                    style: AppTextStyles.heading1,
                  ),
                  const SizedBox(height: AppSpacing.sm),
                  Text(
                    'Sua IA parceira para vender mais e melhor.',
                    textAlign: TextAlign.center,
                    style: AppTextStyles.bodyText.copyWith(color: AppColors.darkGray.withOpacity(0.7)),
                  ),
                  const SizedBox(height: AppSpacing.xl),
                  Text(
                    'Você será redirecionado para o portal de login seguro do sistema.',
                    textAlign: TextAlign.center,
                    style: TextStyle(color: AppColors.darkGray.withOpacity(0.8), fontSize: 14),
                  ),
                  const SizedBox(height: AppSpacing.xl),
                  BravitoPrimaryButton(
                    text: 'Entrar com Keycloak',
                    isLoading: isLoading,
                    onPressed: () {
                      ref.read(authControllerProvider.notifier).login();
                    },
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
