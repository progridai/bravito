import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

import '../../../../core/constants/app_colors.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../../../shared/widgets/bravito_primary_button.dart';
import '../../../../shared/widgets/bravito_text_field.dart';
import '../../../auth/presentation/controllers/alterar_senha_controller.dart';

class AlterarSenhaPage extends ConsumerStatefulWidget {
  const AlterarSenhaPage({super.key});

  @override
  ConsumerState<AlterarSenhaPage> createState() => _AlterarSenhaPageState();
}

class _AlterarSenhaPageState extends ConsumerState<AlterarSenhaPage> {
  final _formKey = GlobalKey<FormState>();
  final _senhaController = TextEditingController();
  final _confirmacaoController = TextEditingController();

  @override
  void dispose() {
    _senhaController.dispose();
    _confirmacaoController.dispose();
    super.dispose();
  }

  void _submit() {
    if (_formKey.currentState!.validate()) {
      ref.read(alterarSenhaControllerProvider.notifier).alterarSenha(_senhaController.text);
    }
  }

  @override
  Widget build(BuildContext context) {
    ref.listen<AlterarSenhaState>(alterarSenhaControllerProvider, (previous, next) {
      if (next.erro != null) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text(next.erro!),
            backgroundColor: BravitoColors.erro,
          ),
        );
      } else if (next.sucesso) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Senha alterada com sucesso!'),
            backgroundColor: BravitoColors.sucesso,
          ),
        );
        context.pop(); // Volta pra tela anterior
      }
    });

    final state = ref.watch(alterarSenhaControllerProvider);

    return BravitoAppScaffold(
      title: 'Alterar Senha',
      body: Center(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(AppSpacing.xl),
          child: ConstrainedBox(
            constraints: const BoxConstraints(maxWidth: 400),
            child: Card(
              elevation: 4,
              shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(16)),
              child: Padding(
                padding: const EdgeInsets.all(AppSpacing.xl),
                child: Form(
                  key: _formKey,
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      const Icon(
                        Icons.lock_reset,
                        size: 64,
                        color: BravitoColors.azulPrincipal,
                      ),
                      const SizedBox(height: AppSpacing.md),
                      const Text(
                        'Crie uma nova senha de acesso.',
                        textAlign: TextAlign.center,
                        style: TextStyle(fontSize: 16),
                      ),
                      const SizedBox(height: AppSpacing.xl),
                      TextFormField(
                        controller: _senhaController,
                        obscureText: true,
                        decoration: const InputDecoration(
                          labelText: 'Nova Senha',
                          prefixIcon: Icon(Icons.lock),
                          border: OutlineInputBorder(),
                        ),
                        validator: (val) {
                          if (val == null || val.length < 6) {
                            return 'A senha deve ter pelo menos 6 caracteres';
                          }
                          return null;
                        },
                      ),
                      const SizedBox(height: AppSpacing.md),
                      TextFormField(
                        controller: _confirmacaoController,
                        obscureText: true,
                        decoration: const InputDecoration(
                          labelText: 'Confirmar Nova Senha',
                          prefixIcon: Icon(Icons.lock_outline),
                          border: OutlineInputBorder(),
                        ),
                        validator: (val) {
                          if (val != _senhaController.text) {
                            return 'As senhas não coincidem';
                          }
                          return null;
                        },
                      ),
                      const SizedBox(height: AppSpacing.xl),
                      SizedBox(
                        width: double.infinity,
                        child: BravitoPrimaryButton(
                          text: state.carregando ? 'Aguarde...' : 'Alterar Senha',
                          onPressed: state.carregando ? null : _submit,
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ),
          ),
        ),
      ),
    );
  }
}
