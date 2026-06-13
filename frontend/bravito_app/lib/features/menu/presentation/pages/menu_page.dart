import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../core/theme/app_theme.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../core/security/recursos_app.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../../../shared/widgets/bravito_card.dart';
import '../../../auth/presentation/controllers/auth_controller.dart';
import '../../../auth/presentation/controllers/auth_state.dart';

class MenuPage extends ConsumerWidget {
  const MenuPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final authState = ref.watch(authControllerProvider);
    bool podeVerUsuarios = false;
    bool podeVerConversas = false;

    if (authState is AuthAuthenticated) {
      podeVerUsuarios = authState.user.possuiRecurso(RecursosApp.usuariosVisualizar);
      podeVerConversas = authState.user.possuiRecurso(RecursosApp.conversasVisualizar);
    }

    return BravitoAppScaffold(
      title: 'Menu',
      body: ListView(
        padding: const EdgeInsets.all(AppSpacing.md),
        children: [
          if (podeVerUsuarios) ...[
            BravitoCard(
              padding: const EdgeInsets.all(0),
              child: ListTile(
                leading: const Icon(Icons.people_outline, color: BravitoColors.dourado),
                title: const Text(
                  'Usuários',
                  style: TextStyle(fontWeight: FontWeight.bold),
                ),
                trailing: const Icon(Icons.arrow_forward_ios, size: 16),
                onTap: () {
                  context.push('/menu/usuarios');
                },
              ),
            ),
            const SizedBox(height: AppSpacing.sm),
          ],
          BravitoCard(
            padding: const EdgeInsets.all(0),
            child: ListTile(
              leading: const Icon(Icons.library_books_outlined, color: BravitoColors.dourado),
              title: const Text(
                'Base de Conhecimento',
                style: TextStyle(fontWeight: FontWeight.bold, color: BravitoColors.pretoSuave),
              ),
              trailing: const Icon(Icons.arrow_forward_ios, size: 16),
              onTap: () {
                context.push('/menu/base-conhecimento');
              },
            ),
          ),
          const SizedBox(height: AppSpacing.sm),
          BravitoCard(
            padding: const EdgeInsets.all(0),
            child: ListTile(
              leading: const Icon(Icons.lock_outline, color: BravitoColors.dourado),
              title: const Text(
                'Alterar Senha',
                style: TextStyle(fontWeight: FontWeight.bold),
              ),
              trailing: const Icon(Icons.arrow_forward_ios, size: 16),
              onTap: () {
                context.push('/menu/alterar-senha');
              },
            ),
          ),
          if (podeVerConversas) ...[
            const SizedBox(height: AppSpacing.sm),
            BravitoCard(
              padding: const EdgeInsets.all(0),
              child: ListTile(
                leading: const Icon(Icons.chat_bubble_outline, color: BravitoColors.dourado),
                title: const Text(
                  'Visualizar Conversas',
                  style: TextStyle(fontWeight: FontWeight.bold),
                ),
                trailing: const Icon(Icons.arrow_forward_ios, size: 16),
                onTap: () {
                  context.push('/menu/conversas');
                },
              ),
            ),
          ],
        ],
      ),
    );
  }
}
