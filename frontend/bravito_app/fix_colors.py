import os
import glob

def run():
    files = glob.glob('d:/Potter/Rsul Automacoes/Projetos/bravito/frontend/bravito_app/lib/**/*.dart', recursive=True)
    for file in files:
        with open(file, 'r', encoding='utf-8') as f:
            content = f.read()
        
        changed = False
        
        # Mapping old variables to new variables
        replacements = {
            'BravitoColors.error': 'BravitoColors.erro',
            'BravitoColors.azulPrincipal': 'BravitoColors.dourado',
            'BravitoColors.azulSecundario': 'BravitoColors.pretoSuave',
            'BravitoColors.cinzaEscuro': 'BravitoColors.pretoSuave',
            'BravitoColors.cinzaClaro': 'BravitoColors.cinzaClaro', # Already exists but just in case
            "import '../../../../core/theme/app_theme.dart';": "import '../../../../core/constants/app_colors.dart';\nimport '../../../../core/theme/app_theme.dart';",
            "import '../../../core/theme/app_theme.dart';": "import '../../../core/constants/app_colors.dart';\nimport '../../../core/theme/app_theme.dart';"
        }
        
        for old, new in replacements.items():
            if old in content:
                content = content.replace(old, new)
                changed = True
                
        if changed:
            with open(file, 'w', encoding='utf-8') as f:
                f.write(content)

if __name__ == '__main__':
    run()
