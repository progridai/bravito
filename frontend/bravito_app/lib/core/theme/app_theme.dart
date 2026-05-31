import 'package:flutter/material.dart';

class AppColors {
  // Cores oficiais do Bravito
  static const Color azulPrincipal = Color(0xFF1E3A8A);
  static const Color azulSecundario = Color(0xFF2563EB);
  static const Color dourado = Color(0xFFD4AF37);
  static const Color cinzaClaro = Color(0xFFF2F4F7);
  static const Color cinzaEscuro = Color(0xFF334155);
}

class AppTheme {
  static ThemeData get lightTheme {
    return ThemeData(
      useMaterial3: true,
      colorScheme: ColorScheme.fromSeed(
        seedColor: AppColors.azulPrincipal,
        primary: AppColors.azulPrincipal,
        secondary: AppColors.azulSecundario,
        tertiary: AppColors.dourado,
        background: AppColors.cinzaClaro,
        surface: Colors.white,
        onPrimary: Colors.white,
        onSecondary: Colors.white,
        onBackground: AppColors.cinzaEscuro,
        onSurface: AppColors.cinzaEscuro,
      ),
      scaffoldBackgroundColor: AppColors.cinzaClaro,
      appBarTheme: const AppBarTheme(
        backgroundColor: AppColors.azulPrincipal,
        foregroundColor: Colors.white,
        elevation: 0,
      ),
      elevatedButtonTheme: ElevatedButtonThemeData(
        style: ElevatedButton.styleFrom(
          backgroundColor: AppColors.azulPrincipal,
          foregroundColor: Colors.white,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(8),
          ),
        ),
      ),
      textTheme: const TextTheme(
        bodyLarge: TextStyle(color: AppColors.cinzaEscuro),
        bodyMedium: TextStyle(color: AppColors.cinzaEscuro),
        titleLarge: TextStyle(color: AppColors.azulPrincipal, fontWeight: FontWeight.bold),
      ),
    );
  }
}
