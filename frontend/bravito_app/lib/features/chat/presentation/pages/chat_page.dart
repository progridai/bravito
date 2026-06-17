import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

import '../../../../core/constants/app_colors.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../features/auth/presentation/controllers/auth_controller.dart';
import '../../../../features/auth/presentation/controllers/auth_state.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../domain/entities/tipo_remetente.dart';
import '../controllers/chat_controller.dart';
import '../widgets/mensagem_chat_bubble.dart';
import '../../../../app/theme_provider.dart';

class ChatPage extends ConsumerStatefulWidget {
  const ChatPage({super.key});

  @override
  ConsumerState<ChatPage> createState() => _ChatPageState();
}

class _ChatPageState extends ConsumerState<ChatPage> {
  final TextEditingController _textController = TextEditingController();
  final ScrollController _scrollController = ScrollController();

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      ref.read(chatControllerProvider.notifier).carregarHistorico();
    });
  }

  void _enviarMensagem() {
    final texto = _textController.text;
    if (texto.trim().isEmpty) return;

    ref.read(chatControllerProvider.notifier).enviarMensagem(texto);
    _textController.clear();
    
    _rolarParaFinal();
  }

  void _rolarParaFinal() {
    Future.delayed(const Duration(milliseconds: 100), () {
      if (_scrollController.hasClients) {
        _scrollController.animateTo(
          _scrollController.position.maxScrollExtent + 200,
          duration: const Duration(milliseconds: 300),
          curve: Curves.easeOut,
        );
      }
    });
  }

  @override
  void dispose() {
    _textController.dispose();
    _scrollController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final authState = ref.watch(authControllerProvider);
    final chatState = ref.watch(chatControllerProvider);

    // Auto rolar para o final ao receber novas mensagens
    ref.listen(chatControllerProvider, (previous, next) {
      if (previous?.mensagens.length != next.mensagens.length) {
        _rolarParaFinal();
      }
    });

    String userName = 'Usuário';
    if (authState is AuthAuthenticated) {
      userName = authState.user.firstName.isNotEmpty ? authState.user.firstName : authState.user.username;
    }

    return BravitoAppScaffold(
      titleWidget: Row(
        children: [
          const CircleAvatar(
            backgroundImage: AssetImage('assets/images/bravito_avatar.png'),
            backgroundColor: BravitoColors.branco,
            radius: 18,
          ),
          const SizedBox(width: AppSpacing.sm),
          const Text('Bravito Chat', style: TextStyle(fontWeight: FontWeight.w600)),
        ],
      ),
      actions: [
        IconButton(
          icon: Icon(
            Theme.of(context).brightness == Brightness.dark 
                ? Icons.light_mode 
                : Icons.dark_mode,
          ),
          tooltip: 'Trocar Tema',
          onPressed: () {
            ref.read(themeModeProvider.notifier).toggle();
          },
        ),
        IconButton(
          icon: const Icon(Icons.logout),
          tooltip: 'Sair',
          onPressed: () {
            ref.read(authControllerProvider.notifier).logout();
          },
        ),
      ],
      body: Column(
        children: [
          Expanded(
            child: chatState.mensagens.isEmpty
                ? Center(
                    child: Text(
                      'Como posso ajudar você hoje?',
                      style: TextStyle(
                        color: Theme.of(context).colorScheme.onSurface.withValues(alpha: 0.6),
                        fontSize: 16,
                      ),
                    ),
                  )
                : ListView.builder(
                    controller: _scrollController,
                    padding: const EdgeInsets.all(AppSpacing.md),
                    itemCount: chatState.mensagens.length,
                    itemBuilder: (context, index) {
                      final mensagem = chatState.mensagens[index];
                      return MensagemChatBubble(mensagem: mensagem);
                    },
                  ),
          ),
          if (chatState.carregando)
            const Padding(
              padding: EdgeInsets.symmetric(vertical: AppSpacing.sm),
              child: SizedBox(
                height: 20,
                width: 20,
                child: CircularProgressIndicator(strokeWidth: 2, color: BravitoColors.dourado),
              ),
            ),
          Container(
            padding: const EdgeInsets.all(AppSpacing.md),
            decoration: BoxDecoration(
              color: Theme.of(context).colorScheme.surface,
              boxShadow: [
                BoxShadow(
                  color: BravitoColors.pretoSuave.withOpacity(0.05),
                  blurRadius: 10,
                  offset: const Offset(0, -4),
                ),
              ],
            ),
            child: Row(
              children: [
                Expanded(
                  child: Focus(
                    onKeyEvent: (node, event) {
                      if (event is KeyDownEvent && event.logicalKey == LogicalKeyboardKey.enter) {
                        if (!HardwareKeyboard.instance.isShiftPressed) {
                          if (!chatState.carregando) {
                            _enviarMensagem();
                          }
                          return KeyEventResult.handled;
                        }
                      }
                      return KeyEventResult.ignored;
                    },
                    child: TextField(
                      controller: _textController,
                      onSubmitted: (_) => _enviarMensagem(),
                      keyboardType: TextInputType.multiline,
                      textInputAction: TextInputAction.newline,
                      minLines: 1,
                      maxLines: 5,
                    decoration: InputDecoration(
                      hintText: 'Digite sua mensagem...',
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(24),
                        borderSide: BorderSide.none,
                      ),
                      filled: true,
                      fillColor: Theme.of(context).scaffoldBackgroundColor,
                      contentPadding: const EdgeInsets.symmetric(
                        horizontal: AppSpacing.lg,
                        vertical: AppSpacing.sm,
                      ),
                    ),
                  ),
                ),
              ),
                const SizedBox(width: AppSpacing.sm),
                CircleAvatar(
                  backgroundColor: BravitoColors.dourado,
                  child: IconButton(
                    icon: const Icon(Icons.send, color: BravitoColors.branco, size: 20),
                    onPressed: chatState.carregando ? null : _enviarMensagem,
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
