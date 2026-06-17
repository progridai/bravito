# Guia de Implementação: Comando de Voz no Chat (Speech-to-Text)

Este guia documenta o recurso de comando de voz (Speech-to-Text) que foi implementado e posteriormente removido da versão atual do projeto a pedido da equipe. Quando houver a necessidade de reativá-lo, siga rigorosamente os passos abaixo.

O recurso implementa:
1. Um botão no chat dinâmico (Microfone quando vazio, Enviar quando preenchido, Stop enquanto gravando).
2. Transcrição de áudio para texto em tempo real (Android e Web).
3. Gerenciamento seguro do estado de gravação no navegador.

---

## Passo 1: Dependências

Adicione as bibliotecas no `pubspec.yaml`:
```bash
flutter pub add speech_to_text permission_handler
```

## Passo 2: Permissões Nativas (Android)

No arquivo `android/app/src/main/AndroidManifest.xml`, adicione as seguintes configurações dentro da tag `<manifest>`:

1. Permissão de gravação de áudio (acima de `<application>`):
```xml
<uses-permission android:name="android.permission.RECORD_AUDIO" />
```

2. Query do serviço de reconhecimento (dentro da tag `<queries>` no final do arquivo):
```xml
<intent>
    <action android:name="android.speech.RecognitionService" />
</intent>
```

---

## Passo 3: Código do `chat_page.dart`

Substitua ou adicione as importações no topo do arquivo:
```dart
import 'package:flutter/foundation.dart' show kIsWeb;
import 'package:speech_to_text/speech_to_text.dart';
import 'package:speech_to_text/speech_recognition_result.dart';
import 'package:permission_handler/permission_handler.dart';
```

Dentro da classe `_ChatPageState`, declare as variáveis de controle:
```dart
  final SpeechToText _speechToText = SpeechToText();
  
  bool _speechEnabled = false;
  bool _isTextFieldEmpty = true;
  bool _isListening = false;
```

Atualize o método `initState` e adicione os métodos de controle de áudio:
```dart
  @override
  void initState() {
    super.initState();
    _initSpeech();
    _textController.addListener(_onTextChanged);
    WidgetsBinding.instance.addPostFrameCallback((_) {
      ref.read(chatControllerProvider.notifier).carregarHistorico();
    });
  }

  void _onTextChanged() {
    setState(() {
      _isTextFieldEmpty = _textController.text.trim().isEmpty;
    });
  }

  Future<void> _initSpeech() async {
    if (!kIsWeb) {
      await Permission.microphone.request();
    }
    _speechEnabled = await _speechToText.initialize(
      onStatus: (status) {
        setState(() {
          _isListening = status == 'listening';
        });
      },
      onError: (error) {
        setState(() {
          _isListening = false;
        });
      },
    );
    setState(() {});
  }

  void _startListening() async {
    setState(() {
      _isListening = true;
    });
    await _speechToText.listen(
      onResult: _onSpeechResult,
      localeId: 'pt_BR',
    );
  }

  void _stopListening() async {
    await _speechToText.stop();
    setState(() {
      _isListening = false;
    });
  }

  void _onSpeechResult(SpeechRecognitionResult result) {
    setState(() {
      _textController.text = result.recognizedWords;
    });
  }
```

Atualize o `_enviarMensagem` para cancelar a escuta se enviar enquanto grava:
```dart
  void _enviarMensagem() {
    if (_isListening || _speechToText.isListening) {
      _speechToText.stop();
      setState(() {
        _isListening = false;
      });
    }
    
    final texto = _textController.text;
    if (texto.trim().isEmpty) return;

    ref.read(chatControllerProvider.notifier).enviarMensagem(texto);
    _textController.clear();
    
    _rolarParaFinal();
  }
```

No método `dispose`, lembre de remover o listener:
```dart
  @override
  void dispose() {
    _textController.removeListener(_onTextChanged);
    _textController.dispose();
    _scrollController.dispose();
    super.dispose();
  }
```

Por fim, substitua o bloco do botão de `CircleAvatar` no `build` pela versão dinâmica:
```dart
CircleAvatar(
  backgroundColor: _isListening ? BravitoColors.erro : BravitoColors.dourado,
  child: IconButton(
    icon: Icon(
      _isListening 
          ? Icons.stop 
          : (_isTextFieldEmpty && _speechEnabled ? Icons.mic : Icons.send),
      color: BravitoColors.branco, 
      size: 20
    ),
    onPressed: chatState.carregando 
        ? null 
        : (_isListening
            ? _stopListening
            : (_isTextFieldEmpty && _speechEnabled ? _startListening : _enviarMensagem)),
  ),
)
```

Seguindo esses passos, o recurso volta a funcionar exatamente como testado e validado.
