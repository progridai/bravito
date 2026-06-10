import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../core/theme/app_theme.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';

class ConversasPage extends StatelessWidget {
  const ConversasPage({super.key});

  @override
  Widget build(BuildContext context) {
    return BravitoAppScaffold(
      title: 'Visualizar Conversas',
      body: Center(
        child: Padding(
          padding: const EdgeInsets.all(AppSpacing.xl),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              const Icon(
                Icons.chat_bubble_outline,
                size: 80,
                color: BravitoColors.pretoSuave,
              ),
              const SizedBox(height: AppSpacing.lg),
              Text(
                'Nenhuma conversa carregada ainda.',
                style: Theme.of(context).textTheme.titleLarge?.copyWith(
                      color: BravitoColors.pretoSuave,
                    ),
                textAlign: TextAlign.center,
              ),
              const SizedBox(height: AppSpacing.sm),
              Text(
                'A listagem de conversas será implementada em etapa futura.',
                style: Theme.of(context).textTheme.bodyMedium,
                textAlign: TextAlign.center,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
