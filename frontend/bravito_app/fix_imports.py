import os
import glob
import re

def run():
    files = glob.glob('d:/Potter/Rsul Automacoes/Projetos/bravito/frontend/bravito_app/lib/**/*.dart', recursive=True)
    for file in files:
        with open(file, 'r', encoding='utf-8') as f:
            content = f.read()
            
        if 'BravitoColors' in content and 'app_colors.dart' not in content:
            # We need to add the import for app_colors.dart
            # Let's find the depth by counting how many parts after 'lib/'
            rel_path = file.split('lib\\')[1].replace('\\', '/')
            depth = rel_path.count('/')
            
            import_prefix = '../' * depth
            import_stmt = f"import '{import_prefix}core/constants/app_colors.dart';\n"
            
            # Insert after the last import
            lines = content.split('\n')
            last_import_idx = 0
            for i, line in enumerate(lines):
                if line.startswith('import '):
                    last_import_idx = i
            
            lines.insert(last_import_idx + 1, import_stmt)
            content = '\n'.join(lines)
            
            with open(file, 'w', encoding='utf-8') as f:
                f.write(content)

if __name__ == '__main__':
    run()
