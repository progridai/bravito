import 'package:flutter/material.dart';
import '../../core/constants/app_colors.dart';

class BravitoTextField extends StatelessWidget {
  final String label;
  final String hint;
  final bool obscureText;
  final IconData? prefixIcon;

  const BravitoTextField({
    super.key,
    required this.label,
    required this.hint,
    this.obscureText = false,
    this.prefixIcon,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          label,
          style: const TextStyle(
            fontWeight: FontWeight.w600,
            color: AppColors.darkGray,
          ),
        ),
        const SizedBox(height: 8),
        TextField(
          obscureText: obscureText,
          decoration: InputDecoration(
            hintText: hint,
            prefixIcon: prefixIcon != null ? Icon(prefixIcon, color: AppColors.secondaryBlue) : null,
          ),
        ),
      ],
    );
  }
}
