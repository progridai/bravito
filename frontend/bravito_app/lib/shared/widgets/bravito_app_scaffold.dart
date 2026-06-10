import 'package:flutter/material.dart';
import '../../core/constants/app_colors.dart';


class BravitoAppScaffold extends StatelessWidget {
  final Widget body;
  final String? title;
  final Widget? titleWidget;
  final List<Widget>? actions;

  final Widget? floatingActionButton;

  const BravitoAppScaffold({
    super.key,
    required this.body,
    this.title,
    this.titleWidget,
    this.actions,
    this.floatingActionButton,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: (title != null || titleWidget != null)
          ? AppBar(
              backgroundColor: const Color(0xFF1E3A8A), // BravitoColors.dourado
              foregroundColor: Colors.white,
              title: titleWidget ?? (title != null ? Text(title!) : null),
              actions: actions,
            )
          : null,
      body: SafeArea(child: body),
      floatingActionButton: floatingActionButton,
    );
  }
}
