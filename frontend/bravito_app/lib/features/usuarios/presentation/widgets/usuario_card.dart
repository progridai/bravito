import 'package:flutter/material.dart';
import '../../../../core/theme/app_theme.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../shared/widgets/bravito_card.dart';
import '../../domain/entities/usuario.dart';

class UsuarioCard extends StatelessWidget {
  final Usuario usuario;
  final bool podeEditar;
  final bool podeDesativar;
  final VoidCallback onEdit;
  final VoidCallback onToggleStatus;

  const UsuarioCard({
    super.key,
    required this.usuario,
    required this.podeEditar,
    required this.podeDesativar,
    required this.onEdit,
    required this.onToggleStatus,
  });

  @override
  Widget build(BuildContext context) {
    return BravitoCard(
      padding: const EdgeInsets.all(AppSpacing.md),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          CircleAvatar(
            backgroundColor: BravitoColors.azulPrincipal.withOpacity(0.1),
            child: Text(
              usuario.nome.isNotEmpty ? usuario.nome[0].toUpperCase() : 'U',
              style: const TextStyle(color: BravitoColors.azulPrincipal, fontWeight: FontWeight.bold),
            ),
          ),
          const SizedBox(width: AppSpacing.md),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  usuario.nome,
                  style: const TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 16,
                    color: BravitoColors.cinzaEscuro,
                  ),
                ),
                const SizedBox(height: 4),
                Text(
                  usuario.email,
                  style: const TextStyle(
                    color: Colors.grey,
                    fontSize: 14,
                  ),
                ),
                const SizedBox(height: 8),
                Wrap(
                  spacing: 4,
                  children: [
                    Container(
                      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 2),
                      decoration: BoxDecoration(
                        color: usuario.ativo ? Colors.green.withOpacity(0.1) : Colors.red.withOpacity(0.1),
                        borderRadius: BorderRadius.circular(12),
                      ),
                      child: Text(
                        usuario.ativo ? 'Ativo' : 'Inativo',
                        style: TextStyle(
                          fontSize: 12,
                          color: usuario.ativo ? Colors.green : Colors.red,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                    if (usuario.perfis.isNotEmpty)
                      Container(
                        padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 2),
                        decoration: BoxDecoration(
                          color: BravitoColors.dourado.withOpacity(0.1),
                          borderRadius: BorderRadius.circular(12),
                        ),
                        child: Text(
                          usuario.perfis.join(', '),
                          style: const TextStyle(
                            fontSize: 12,
                            color: BravitoColors.dourado,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                  ],
                ),
              ],
            ),
          ),
          Column(
            children: [
              if (podeEditar)
                IconButton(
                  icon: const Icon(Icons.edit, color: BravitoColors.azulSecundario, size: 20),
                  onPressed: onEdit,
                  tooltip: 'Editar',
                ),
              if (podeDesativar)
                IconButton(
                  icon: Icon(
                    usuario.ativo ? Icons.block : Icons.check_circle_outline,
                    color: usuario.ativo ? Colors.red : Colors.green,
                    size: 20,
                  ),
                  onPressed: onToggleStatus,
                  tooltip: usuario.ativo ? 'Desativar' : 'Ativar',
                ),
            ],
          )
        ],
      ),
    );
  }
}
