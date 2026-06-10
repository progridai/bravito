import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import '../../core/theme/app_theme.dart';
import '../../core/constants/app_spacing.dart';
import '../widgets/bravito_primary_button.dart';
import '../widgets/bravito_app_scaffold.dart';

class AcessoNegadoPage extends StatelessWidget {
  const AcessoNegadoPage({super.key});

  @override
  Widget build(BuildContext context) {
    return BravitoAppScaffold(
      title: 'Acesso Negado',
      body: Center(
        child: Padding(
          padding: const EdgeInsets.all(AppSpacing.lg),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              const Icon(
                Icons.security_rounded,
                color: Colors.redAccent,
                size: 80,
              ),
              const SizedBox(height: AppSpacing.md),
              const Text(
                'Acesso Negado',
                style: TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.bold,
                  color: BravitoColors.pretoSuave,
                ),
              ),
              const SizedBox(height: AppSpacing.sm),
              const Text(
                'Você não possui permissão para acessar este recurso.',
                textAlign: TextAlign.center,
                style: TextStyle(
                  fontSize: 16,
                  color: Colors.grey,
                ),
              ),
              const SizedBox(height: AppSpacing.xl),
              BravitoPrimaryButton(
                text: 'Voltar para o Início',
                onPressed: () {
                  context.go('/home');
                },
              ),
            ],
          ),
        ),
      ),
    );
  }
}
