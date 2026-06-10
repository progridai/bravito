import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../core/theme/app_theme.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../../../shared/widgets/bravito_primary_button.dart';
import '../../../../shared/widgets/bravito_text_field.dart';
import '../controllers/usuario_form_controller.dart';
import '../controllers/usuario_form_state.dart';
import '../../domain/entities/perfil_acesso.dart';

class UsuarioFormPage extends ConsumerStatefulWidget {
  final String? usuarioId;

  const UsuarioFormPage({super.key, this.usuarioId});

  @override
  ConsumerState<UsuarioFormPage> createState() => _UsuarioFormPageState();
}

class _UsuarioFormPageState extends ConsumerState<UsuarioFormPage> {
  final _formKey = GlobalKey<FormState>();
  final _nomeController = TextEditingController();
  final _emailController = TextEditingController();
  final _senhaController = TextEditingController();
  
  bool _ativo = true;
  List<String> _selectedPerfis = [];

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      ref.read(usuarioFormControllerProvider.notifier).inicializarFormulario(usuarioId: widget.usuarioId);
    });
  }

  @override
  void dispose() {
    _nomeController.dispose();
    _emailController.dispose();
    _senhaController.dispose();
    super.dispose();
  }

  void _preencherFormulario(UsuarioFormLoaded state) {
    if (state.usuario != null && _nomeController.text.isEmpty) {
      _nomeController.text = state.usuario!.nome;
      _emailController.text = state.usuario!.email;
      _ativo = state.usuario!.ativo;
      
      // Match perfis names with available profiles IDs.
      // The API returns names in 'perfis', but we need to send IDs.
      final usuarioPerfisNomes = state.usuario!.perfis;
      _selectedPerfis = state.perfisDisponiveis
          .where((p) => usuarioPerfisNomes.contains(p.nome))
          .map((p) => p.id)
          .toList();
    }
  }

  void _salvar() async {
    if (_formKey.currentState!.validate()) {
      if (_selectedPerfis.isEmpty) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(content: Text('Selecione pelo menos um perfil de acesso.'), backgroundColor: Colors.red),
        );
        return;
      }

      final controller = ref.read(usuarioFormControllerProvider.notifier);
      final errorMessage = await controller.salvarUsuario(
        id: widget.usuarioId,
        nome: _nomeController.text,
        email: _emailController.text,
        senhaTemporaria: _senhaController.text,
        ativo: _ativo,
        perfilIds: _selectedPerfis,
      );

      if (mounted) {
        if (errorMessage == null) {
          ScaffoldMessenger.of(context).showSnackBar(
            const SnackBar(content: Text('Usuário salvo com sucesso!'), backgroundColor: Colors.green),
          );
          context.pop();
        } else {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(content: Text(errorMessage), backgroundColor: Colors.red),
          );
        }
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final state = ref.watch(usuarioFormControllerProvider);



    return BravitoAppScaffold(
      title: widget.usuarioId == null ? 'Novo Usuário' : 'Editar Usuário',
      body: _buildBody(state),
    );
  }

  Widget _buildBody(UsuarioFormState state) {
    if (state is UsuarioFormLoading || state is UsuarioFormInitial || state is UsuarioFormSuccess) {
      return const Center(child: CircularProgressIndicator());
    }

    if (state is UsuarioFormLoaded) {
      _preencherFormulario(state);
      final isEdit = widget.usuarioId != null;

      return SingleChildScrollView(
        padding: const EdgeInsets.all(AppSpacing.md),
        child: Form(
          key: _formKey,
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              BravitoTextField(
                label: 'Nome completo',
                controller: _nomeController,
                validator: (val) => val == null || val.isEmpty ? 'Nome é obrigatório' : null,
              ),
              const SizedBox(height: AppSpacing.md),
              BravitoTextField(
                label: 'E-mail',
                controller: _emailController,
                keyboardType: TextInputType.emailAddress,
                validator: (val) {
                  if (val == null || val.isEmpty) return 'E-mail é obrigatório';
                  if (!val.contains('@')) return 'E-mail inválido';
                  return null;
                },
              ),
              const SizedBox(height: AppSpacing.md),
              if (!isEdit) ...[
                BravitoTextField(
                  label: 'Senha temporária',
                  controller: _senhaController,
                  obscureText: true,
                  validator: (val) => val == null || val.length < 6 ? 'Mínimo de 6 caracteres' : null,
                ),
                const SizedBox(height: AppSpacing.md),
              ],
              SwitchListTile(
                title: const Text('Usuário Ativo', style: TextStyle(fontWeight: FontWeight.bold)),
                value: _ativo,
                activeColor: BravitoColors.dourado,
                onChanged: (val) => setState(() => _ativo = val),
              ),
              const SizedBox(height: AppSpacing.md),
              const Text('Perfis de Acesso', style: TextStyle(fontWeight: FontWeight.bold, fontSize: 16)),
              const SizedBox(height: AppSpacing.sm),
              ...state.perfisDisponiveis.map((perfil) {
                return CheckboxListTile(
                  title: Text(perfil.nome, style: const TextStyle(fontWeight: FontWeight.bold)),
                  subtitle: perfil.descricao != null ? Text(perfil.descricao!) : null,
                  value: _selectedPerfis.contains(perfil.id),
                  activeColor: BravitoColors.dourado,
                  onChanged: (bool? checked) {
                    setState(() {
                      if (checked == true) {
                        _selectedPerfis.add(perfil.id);
                      } else {
                        _selectedPerfis.remove(perfil.id);
                      }
                    });
                  },
                );
              }),
              const SizedBox(height: AppSpacing.lg),
              SizedBox(
                width: double.infinity,
                child: BravitoPrimaryButton(
                  text: 'Salvar',
                  onPressed: _salvar,
                ),
              ),
            ],
          ),
        ),
      );
    }

    return const SizedBox.shrink();
  }
}
