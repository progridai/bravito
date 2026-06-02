import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_web_plugins/url_strategy.dart';
import 'app/router.dart';
import 'app/theme.dart';
import 'app/theme_provider.dart';
import 'core/security/auth_helper.dart';
import 'core/storage/secure_storage_service.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  usePathUrlStrategy();

  // Captura o redirecionamento OIDC (Web) ANTES do GoRouter limpar a URL do navegador!
  try {
    final authHelper = AuthHelper();
    final tokens = await authHelper.handleRedirect();
    if (tokens != null) {
      final storage = SecureStorageService();
      await storage.saveTokens(
        accessToken: tokens['access_token']!,
        refreshToken: tokens['refresh_token']!,
      );
      print('DEBUG: [MAIN] Token OIDC resgatado e salvo com sucesso ANTES do GoRouter!');
    }
  } catch (e) {
    debugPrint('Erro ao processar redirect no main: $e');
  }

  runApp(const ProviderScope(child: BravitoApp()));
}

class BravitoApp extends ConsumerWidget {
  const BravitoApp({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final router = ref.watch(routerProvider);
    final themeMode = ref.watch(themeModeProvider);

    return MaterialApp.router(
      title: 'Bravito',
      theme: AppTheme.lightTheme,
      darkTheme: AppTheme.darkTheme,
      themeMode: themeMode,
      routerConfig: router,
      debugShowCheckedModeBanner: false,
    );
  }
}
