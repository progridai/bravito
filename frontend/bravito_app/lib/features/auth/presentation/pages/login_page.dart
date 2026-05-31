import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../core/constants/app_text_styles.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../../../shared/widgets/bravito_card.dart';
import '../../../../shared/widgets/bravito_primary_button.dart';
import '../../../../shared/widgets/bravito_text_field.dart';

class LoginPage extends StatelessWidget {
  const LoginPage({super.key});

  @override
  Widget build(BuildContext context) {
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
                    Icons.smart_toy_rounded, // Placeholder para a coruja tecnológica
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
                  const BravitoTextField(
                    label: 'Usuário ou E-mail',
                    hint: 'Digite seu usuário',
                    prefixIcon: Icons.person_outline,
                  ),
                  const SizedBox(height: AppSpacing.md),
                  const BravitoTextField(
                    label: 'Senha',
                    hint: 'Digite sua senha',
                    obscureText: true,
                    prefixIcon: Icons.lock_outline,
                  ),
                  const SizedBox(height: AppSpacing.xl),
                  BravitoPrimaryButton(
                    text: 'Entrar',
                    onPressed: () {
                      // Sem autenticação real por enquanto, apenas navegação visual
                      context.go('/chat');
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
