import 'package:flutter/material.dart';
import '../constants/app_colors.dart';

class AppThemeLegacy {
  static ThemeData get lightTheme {
    return ThemeData(
      useMaterial3: true,
      fontFamily: 'Inter',
      colorScheme: ColorScheme.fromSeed(
        seedColor: BravitoColors.dourado,
        primary: BravitoColors.dourado,
        secondary: BravitoColors.dourado,
        tertiary: BravitoColors.dourado,
        background: BravitoColors.cinzaClaro,
        surface: Colors.white,
        onPrimary: Colors.white,
        onSecondary: Colors.white,
        onBackground: BravitoColors.pretoSuave,
        onSurface: BravitoColors.pretoSuave,
      ),
      scaffoldBackgroundColor: BravitoColors.cinzaClaro,
      appBarTheme: const AppBarTheme(
        backgroundColor: BravitoColors.dourado,
        foregroundColor: Colors.white,
        elevation: 0,
      ),
      elevatedButtonTheme: ElevatedButtonThemeData(
        style: ElevatedButton.styleFrom(
          backgroundColor: BravitoColors.dourado,
          foregroundColor: Colors.white,
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(8),
          ),
        ),
      ),
      textTheme: const TextTheme(
        bodyLarge: TextStyle(color: BravitoColors.pretoSuave),
        bodyMedium: TextStyle(color: BravitoColors.pretoSuave),
        titleLarge: TextStyle(color: BravitoColors.dourado, fontWeight: FontWeight.bold, fontFamily: 'Montserrat'),
      ),
    );
  }
}
