import 'package:flutter/material.dart';
import 'app_colors.dart';

class AppTextStyles {
  AppTextStyles._();

  static const TextStyle heading1 = TextStyle(
    fontSize: 32,
    fontWeight: FontWeight.bold,
    color: AppColors.primaryBlue,
  );

  static const TextStyle heading2 = TextStyle(
    fontSize: 24,
    fontWeight: FontWeight.bold,
    color: AppColors.primaryBlue,
  );

  static const TextStyle bodyText = TextStyle(
    fontSize: 16,
    color: AppColors.darkGray,
  );

  static const TextStyle caption = TextStyle(
    fontSize: 12,
    color: AppColors.darkGray,
  );
}
