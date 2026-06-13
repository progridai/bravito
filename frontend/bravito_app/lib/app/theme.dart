import 'package:flutter/material.dart';
import '../core/constants/app_colors.dart';
import '../core/constants/app_radius.dart';

class AppTheme {
  AppTheme._();

  static ThemeData get lightTheme {
    return ThemeData(
      useMaterial3: true,
      fontFamily: 'Inter',
      colorScheme: const ColorScheme.light(
        primary: BravitoColors.dourado,
        secondary: BravitoColors.dourado,
        tertiary: BravitoColors.dourado,
        surface: BravitoColors.cinzaClaro,
        onSurface: BravitoColors.pretoSuave,
      ),
      scaffoldBackgroundColor: BravitoColors.cinzaClaro,
      appBarTheme: const AppBarTheme(
        backgroundColor: BravitoColors.dourado,
        foregroundColor: BravitoColors.branco,
        elevation: 0,
        centerTitle: true,
      ),
      elevatedButtonTheme: ElevatedButtonThemeData(
        style: ElevatedButton.styleFrom(
          backgroundColor: BravitoColors.dourado,
          foregroundColor: BravitoColors.branco,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(AppRadius.md),
          ),
          padding: const EdgeInsets.symmetric(vertical: 16, horizontal: 24),
        ),
      ),
      inputDecorationTheme: InputDecorationTheme(
        filled: true,
        fillColor: BravitoColors.branco,
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(AppRadius.md),
          borderSide: BorderSide.none,
        ),
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(AppRadius.md),
          borderSide: BorderSide.none,
        ),
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(AppRadius.md),
          borderSide: const BorderSide(color: BravitoColors.dourado, width: 2),
        ),
        contentPadding: const EdgeInsets.symmetric(horizontal: 16, vertical: 16),
      ),
    );
  }

  static ThemeData get darkTheme {
    return ThemeData(
      useMaterial3: true,
      fontFamily: 'Inter',
      colorScheme: const ColorScheme.dark(
        primary: BravitoColors.dourado,
        secondary: BravitoColors.dourado,
        tertiary: BravitoColors.dourado,
        surface: Color(0xFF1A1A1A),
        onSurface: BravitoColors.branco,
      ),
      scaffoldBackgroundColor: const Color(0xFF121212), // Fundo principal escuro puro
      appBarTheme: const AppBarTheme(
        backgroundColor: Color(0xFF1A1A1A), // Fundo da AppBar escuro
        foregroundColor: BravitoColors.dourado, // Texto e ícones dourados
        elevation: 0,
        centerTitle: true,
      ),
      elevatedButtonTheme: ElevatedButtonThemeData(
        style: ElevatedButton.styleFrom(
          backgroundColor: BravitoColors.dourado,
          foregroundColor: BravitoColors.branco,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(AppRadius.md),
          ),
          padding: const EdgeInsets.symmetric(vertical: 16, horizontal: 24),
        ),
      ),
      inputDecorationTheme: InputDecorationTheme(
        filled: true,
        fillColor: BravitoColors.pretoSuave,
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(AppRadius.md),
          borderSide: BorderSide.none,
        ),
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(AppRadius.md),
          borderSide: BorderSide.none,
        ),
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(AppRadius.md),
          borderSide: const BorderSide(color: BravitoColors.dourado, width: 2),
        ),
        contentPadding: const EdgeInsets.symmetric(horizontal: 16, vertical: 16),
      ),
    );
  }
}
