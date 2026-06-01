import subprocess
import sys

try:
    print("Running flutter build web...")
    result = subprocess.run(["flutter", "build", "web", "--release"], capture_output=True, text=True, shell=True)
    print("STDOUT:")
    print(result.stdout)
    print("STDERR:")
    print(result.stderr)
    print(f"Exit code: {result.returncode}")
except Exception as e:
    print(f"Error: {e}")
