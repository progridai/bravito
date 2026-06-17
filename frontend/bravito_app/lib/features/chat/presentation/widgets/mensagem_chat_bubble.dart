import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_markdown/flutter_markdown.dart';

import '../../../../core/constants/app_colors.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../domain/entities/mensagem_chat.dart';
import '../../domain/entities/tipo_remetente.dart';
import '../controllers/chat_controller.dart';

class MensagemChatBubble extends ConsumerWidget {
  final MensagemChat mensagem;

  const MensagemChatBubble({
    super.key,
    required this.mensagem,
  });

  void _mostrarOpcoes(BuildContext context, WidgetRef ref) {
    showModalBottomSheet(
      context: context,
      builder: (ctx) {
        return SafeArea(
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              ListTile(
                leading: const Icon(Icons.copy),
                title: const Text('Copiar'),
                onTap: () {
                  Clipboard.setData(ClipboardData(text: mensagem.texto));
                  Navigator.pop(ctx);
                  ScaffoldMessenger.of(context).showSnackBar(
                    const SnackBar(content: Text('Mensagem copiada!')),
                  );
                },
              ),
              ListTile(
                leading: const Icon(Icons.delete, color: BravitoColors.erro),
                title: const Text('Excluir', style: TextStyle(color: BravitoColors.erro)),
                onTap: () {
                  ref.read(chatControllerProvider.notifier).excluirMensagem(mensagem.id);
                  Navigator.pop(ctx);
                },
              ),
            ],
          ),
        );
      },
    );
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final isUsuario = mensagem.tipoRemetente == TipoRemetente.usuario;
    final isSistema = mensagem.tipoRemetente == TipoRemetente.sistema;

    Color bubbleColor;
    Color textColor;

    final isDark = Theme.of(context).brightness == Brightness.dark;

    if (isSistema) {
      bubbleColor = BravitoColors.erro.withOpacity(0.1);
      textColor = BravitoColors.erro;
    } else if (isUsuario) {
      bubbleColor = BravitoColors.dourado;
      textColor = BravitoColors.branco;
    } else {
      bubbleColor = isDark ? BravitoColors.pretoSuave.withOpacity(0.5) : BravitoColors.cinzaClaro;
      textColor = isDark ? BravitoColors.branco : BravitoColors.pretoSuave;
    }

    return Align(
      alignment: isSistema 
          ? Alignment.center 
          : isUsuario 
              ? Alignment.centerRight 
              : Alignment.centerLeft,
      child: GestureDetector(
        onLongPress: () => _mostrarOpcoes(context, ref),
        child: Container(
          constraints: BoxConstraints(
            maxWidth: MediaQuery.of(context).size.width * 0.75,
          ),
          margin: const EdgeInsets.symmetric(vertical: AppSpacing.xs),
          padding: const EdgeInsets.symmetric(
            horizontal: AppSpacing.md,
            vertical: AppSpacing.sm,
          ),
          decoration: BoxDecoration(
            color: bubbleColor,
            borderRadius: BorderRadius.only(
              topLeft: const Radius.circular(16),
              topRight: const Radius.circular(16),
              bottomLeft: Radius.circular(isUsuario || isSistema ? 16 : 4),
              bottomRight: Radius.circular(isUsuario ? 4 : 16),
            ),
          ),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              MarkdownBody(
                data: mensagem.texto,
                selectable: true,
                styleSheet: MarkdownStyleSheet(
                  p: TextStyle(color: textColor, fontSize: 15),
                  strong: TextStyle(color: textColor, fontSize: 15, fontWeight: FontWeight.bold),
                  em: TextStyle(color: textColor, fontSize: 15, fontStyle: FontStyle.italic),
                  listBullet: TextStyle(color: textColor, fontSize: 15),
                  h1: TextStyle(color: textColor, fontSize: 24, fontWeight: FontWeight.bold),
                  h2: TextStyle(color: textColor, fontSize: 22, fontWeight: FontWeight.bold),
                  h3: TextStyle(color: textColor, fontSize: 20, fontWeight: FontWeight.bold),
                ),
              ),
              if (mensagem.erro != null) ...[
                const SizedBox(height: 4),
                Text(
                  mensagem.erro!,
                  style: const TextStyle(
                    color: BravitoColors.erro,
                    fontSize: 12,
                  ),
                ),
              ]
            ],
          ),
        ),
      ),
    );
  }
}
