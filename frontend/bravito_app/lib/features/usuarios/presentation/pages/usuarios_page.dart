import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../core/theme/app_theme.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../../../shared/widgets/bravito_primary_button.dart';
import '../../../../core/security/recursos_app.dart';
import '../../../auth/presentation/controllers/auth_controller.dart';
import '../../../auth/presentation/controllers/auth_state.dart';
import '../controllers/usuarios_controller.dart';
import '../controllers/usuarios_state.dart';
import '../widgets/usuario_card.dart';

class UsuariosPage extends ConsumerStatefulWidget {
  const UsuariosPage({super.key});

  @override
  ConsumerState<UsuariosPage> createState() => _UsuariosPageState();
}

class _UsuariosPageState extends ConsumerState<UsuariosPage> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      ref.read(usuariosControllerProvider.notifier).carregarUsuarios();
    });
  }

  void _showError(String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(content: Text(message), backgroundColor: Colors.red),
    );
  }

  Future<void> _toggleStatus(String id, bool isAtivo) async {
    final bool? confirm = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Confirmação'),
        content: Text(isAtivo 
          ? 'Tem certeza que deseja desativar este usuário?' 
          : 'Deseja ativar este usuário novamente?'),
        actions: [
          TextButton(
            onPressed: () => Navigator.of(context).pop(false),
            child: const Text('Cancelar'),
          ),
          TextButton(
            onPressed: () => Navigator.of(context).pop(true),
            child: const Text('Confirmar', style: TextStyle(color: BravitoColors.dourado)),
          ),
        ],
      ),
    );

    if (confirm == true) {
      final controller = ref.read(usuariosControllerProvider.notifier);
      final success = isAtivo 
          ? await controller.desativarUsuario(id)
          : await controller.ativarUsuario(id);
          
      if (!success && mounted) {
        final state = ref.read(usuariosControllerProvider);
        if (state is UsuariosError) {
          _showError(state.message);
        }
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final state = ref.watch(usuariosControllerProvider);
    final authState = ref.watch(authControllerProvider);
    
    bool podeCadastrar = false;
    bool podeEditar = false;
    bool podeDesativar = false;

    if (authState is AuthAuthenticated) {
      podeCadastrar = authState.user.possuiRecurso(RecursosApp.usuariosCadastrar);
      podeEditar = authState.user.possuiRecurso(RecursosApp.usuariosEditar);
      podeDesativar = authState.user.possuiRecurso(RecursosApp.usuariosDesativar);
    }

    return BravitoAppScaffold(
      title: 'Usuários',
      body: _buildBody(state, podeEditar, podeDesativar),
      floatingActionButton: podeCadastrar
          ? FloatingActionButton(
              backgroundColor: BravitoColors.dourado,
              onPressed: () => context.push('/menu/usuarios/form'),
              child: const Icon(Icons.add, color: Colors.white),
            )
          : null,
    );
  }

  Widget _buildBody(UsuariosState state, bool podeEditar, bool podeDesativar) {
    if (state is UsuariosLoading) {
      return const Center(child: CircularProgressIndicator());
    }

    if (state is UsuariosError) {
      return Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            const Icon(Icons.error_outline, color: Colors.red, size: 48),
            const SizedBox(height: AppSpacing.md),
            Text(state.message, textAlign: TextAlign.center),
            const SizedBox(height: AppSpacing.md),
            BravitoPrimaryButton(
              text: 'Tentar novamente',
              onPressed: () => ref.read(usuariosControllerProvider.notifier).carregarUsuarios(),
            )
          ],
        ),
      );
    }

    if (state is UsuariosLoaded) {
      if (state.usuarios.isEmpty) {
        return const Center(
          child: Text('Nenhum usuário cadastrado.', style: TextStyle(color: Colors.grey)),
        );
      }

      return RefreshIndicator(
        onRefresh: () => ref.read(usuariosControllerProvider.notifier).carregarUsuarios(),
        child: ListView.separated(
          padding: const EdgeInsets.all(AppSpacing.md),
          itemCount: state.usuarios.length,
          separatorBuilder: (_, __) => const SizedBox(height: AppSpacing.sm),
          itemBuilder: (context, index) {
            final usuario = state.usuarios[index];
            return UsuarioCard(
              usuario: usuario,
              podeEditar: podeEditar,
              podeDesativar: podeDesativar,
              onEdit: () => context.push('/menu/usuarios/form/${usuario.id}'),
              onToggleStatus: () => _toggleStatus(usuario.id, usuario.ativo),
            );
          },
        ),
      );
    }

    return const SizedBox.shrink();
  }
}
