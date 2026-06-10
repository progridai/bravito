import 'package:flutter/material.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../domain/entities/mensagem_chat.dart';
import '../../domain/entities/tipo_remetente.dart';

class MensagemChatBubble extends StatelessWidget {
  final MensagemChat mensagem;

  const MensagemChatBubble({
    super.key,
    required this.mensagem,
  });

  @override
  Widget build(BuildContext context) {
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
            Text(
              mensagem.texto,
              style: TextStyle(
                color: textColor,
                fontSize: 15,
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
    );
  }
}
