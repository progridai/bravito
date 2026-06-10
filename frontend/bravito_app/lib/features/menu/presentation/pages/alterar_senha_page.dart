import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';
import '../../../../core/config/app_config.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../core/theme/app_theme.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../../../shared/widgets/bravito_primary_button.dart';
import '../../../../shared/widgets/bravito_card.dart';

class AlterarSenhaPage extends StatelessWidget {
  const AlterarSenhaPage({super.key});

  Future<void> _abrirKeycloak(BuildContext context) async {
    final Uri url = Uri.parse(AppConfig.keycloakAccountUrl);
    try {
      if (await canLaunchUrl(url)) {
        await launchUrl(
          url,
          mode: LaunchMode.externalApplication,
        );
      } else {
        throw Exception('Could not launch $url');
      }
    } catch (e) {
      if (context.mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Não foi possível abrir a página de alteração de senha. Tente novamente em alguns instantes.'),
            backgroundColor: Colors.red,
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return BravitoAppScaffold(
      title: 'Alterar Senha',
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(AppSpacing.md),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            BravitoCard(
              child: Column(
                children: [
                  const Icon(
                    Icons.security,
                    size: 64,
                    color: BravitoColors.dourado,
                  ),
                  const SizedBox(height: AppSpacing.md),
                  Text(
                    'Ambiente Seguro',
                    style: Theme.of(context).textTheme.titleLarge?.copyWith(
                          color: BravitoColors.dourado,
                          fontWeight: FontWeight.bold,
                        ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: AppSpacing.sm),
                  Text(
                    'Por questões de segurança, a alteração de sua senha é realizada diretamente no ambiente oficial de autenticação (Keycloak).',
                    style: Theme.of(context).textTheme.bodyMedium,
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: AppSpacing.sm),
                  Text(
                    'Nenhuma senha é salva ou trafegada por este aplicativo.',
                    style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                          fontWeight: FontWeight.bold,
                        ),
                    textAlign: TextAlign.center,
                  ),
                ],
              ),
            ),
            const SizedBox(height: AppSpacing.xl),
            BravitoPrimaryButton(
              text: 'Abrir alteração de senha',
              onPressed: () => _abrirKeycloak(context),
            ),
            const SizedBox(height: AppSpacing.md),
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: const Text(
                'Voltar',
                style: TextStyle(
                  color: BravitoColors.dourado,
                  fontWeight: FontWeight.bold,
                  fontSize: 16,
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
