import 'package:flutter/material.dart';
import '../../core/constants/app_colors.dart';

class BravitoTextField extends StatelessWidget {
  final String label;
  final String? hint;
  final bool obscureText;
  final IconData? prefixIcon;
  final TextEditingController? controller;
  final TextInputType? keyboardType;
  final String? Function(String?)? validator;

  const BravitoTextField({
    super.key,
    required this.label,
    this.hint,
    this.obscureText = false,
    this.prefixIcon,
    this.controller,
    this.keyboardType,
    this.validator,
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
        TextFormField(
          controller: controller,
          obscureText: obscureText,
          keyboardType: keyboardType,
          validator: validator,
          decoration: InputDecoration(
            hintText: hint,
            prefixIcon: prefixIcon != null ? Icon(prefixIcon, color: AppColors.secondaryBlue) : null,
          ),
        ),
      ],
    );
  }
}
