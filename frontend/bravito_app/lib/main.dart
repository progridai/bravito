import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_web_plugins/url_strategy.dart';
import 'app/router.dart';
import 'app/theme.dart';

void main() {
  usePathUrlStrategy();
  runApp(const ProviderScope(child: BravitoApp()));
}

class BravitoApp extends ConsumerWidget {
  const BravitoApp({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final router = ref.watch(routerProvider);

    return MaterialApp.router(
      title: 'Bravito',
      theme: AppTheme.lightTheme,
      routerConfig: router,
      debugShowCheckedModeBanner: false,
    );
  }
}
