import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../../../shared/widgets/bravito_card.dart';

class MockDocumento {
  final String id;
  final String nome;
  final DateTime dataEnvio;
  final String status; // Processado, Processando, Erro
  final int? chunks;
  final String? tamanho;

  MockDocumento({
    required this.id,
    required this.nome,
    required this.dataEnvio,
    required this.status,
    this.chunks,
    this.tamanho,
  });
}

class BaseConhecimentoPage extends ConsumerStatefulWidget {
  const BaseConhecimentoPage({super.key});

  @override
  ConsumerState<BaseConhecimentoPage> createState() => _BaseConhecimentoPageState();
}

class _BaseConhecimentoPageState extends ConsumerState<BaseConhecimentoPage> {
  final List<MockDocumento> _documentos = [
    MockDocumento(
      id: '1',
      nome: 'Regulamento ExecPrev.pdf',
      dataEnvio: DateTime.now().subtract(const Duration(days: 2)),
      status: 'Processado',
      chunks: 45,
      tamanho: '2.4 MB',
    ),
    MockDocumento(
      id: '2',
      nome: 'FAQ Seguro de Vida.docx',
      dataEnvio: DateTime.now().subtract(const Duration(days: 5)),
      status: 'Processado',
      chunks: 12,
      tamanho: '500 KB',
    ),
    MockDocumento(
      id: '3',
      nome: 'Condições Gerais.pdf',
      dataEnvio: DateTime.now().subtract(const Duration(days: 1)),
      status: 'Erro',
      chunks: null,
      tamanho: '5.1 MB',
    ),
  ];

  void _adicionarDocumento() {
    // Simular o upload de um arquivo
    setState(() {
      _documentos.insert(
        0,
        MockDocumento(
          id: DateTime.now().millisecondsSinceEpoch.toString(),
          nome: 'Novo Documento ${DateTime.now().second}.txt',
          dataEnvio: DateTime.now(),
          status: 'Processando',
          tamanho: '---',
        ),
      );
    });

    ScaffoldMessenger.of(context).showSnackBar(
      const SnackBar(
        content: Text('Documento adicionado (Simulação). Status: Processando.'),
        backgroundColor: BravitoColors.azulPrincipal,
      ),
    );
  }

  void _acaoMockada(String acao) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: Text(acao),
        content: const Text('Funcionalidade ainda não integrada ao backend.'),
        actions: [
          TextButton(
            onPressed: () => Navigator.of(context).pop(),
            child: const Text('OK', style: TextStyle(color: BravitoColors.azulPrincipal)),
          ),
        ],
      ),
    );
  }

  Widget _buildStatusChip(String status) {
    Color corFundo;
    Color corTexto = Colors.white;

    switch (status) {
      case 'Processado':
        corFundo = Colors.green.shade600;
        break;
      case 'Processando':
        corFundo = BravitoColors.dourado;
        corTexto = BravitoColors.pretoSuave;
        break;
      case 'Erro':
        corFundo = Colors.red.shade600;
        break;
      default:
        corFundo = Colors.grey;
    }

    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
      decoration: BoxDecoration(
        color: corFundo,
        borderRadius: BorderRadius.circular(12),
      ),
      child: Text(
        status,
        style: TextStyle(
          color: corTexto,
          fontSize: 12,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return BravitoAppScaffold(
      title: 'Base de Conhecimento',
      body: Padding(
        padding: const EdgeInsets.all(AppSpacing.md),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Text(
              'Gerencie os documentos que a IA usa como fonte para responder perguntas.',
              style: TextStyle(
                fontSize: 16,
                color: BravitoColors.cinzaEscuro,
              ),
            ),
            const SizedBox(height: AppSpacing.lg),
            ElevatedButton.icon(
              onPressed: _adicionarDocumento,
              icon: const Icon(Icons.add_circle_outline),
              label: const Text('Adicionar documento'),
              style: ElevatedButton.styleFrom(
                backgroundColor: BravitoColors.azulPrincipal,
                foregroundColor: Colors.white,
                padding: const EdgeInsets.all(AppSpacing.md),
              ),
            ),
            const SizedBox(height: AppSpacing.lg),
            Expanded(
              child: _documentos.isEmpty
                  ? const Center(child: Text('Nenhum documento na base de conhecimento.'))
                  : ListView.separated(
                      itemCount: _documentos.length,
                      separatorBuilder: (context, index) => const SizedBox(height: AppSpacing.sm),
                      itemBuilder: (context, index) {
                        final doc = _documentos[index];
                        return BravitoCard(
                          padding: const EdgeInsets.all(AppSpacing.md),
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Row(
                                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                                children: [
                                  Expanded(
                                    child: Text(
                                      doc.nome,
                                      style: const TextStyle(
                                        fontWeight: FontWeight.bold,
                                        fontSize: 16,
                                        color: BravitoColors.azulPrincipal,
                                      ),
                                    ),
                                  ),
                                  _buildStatusChip(doc.status),
                                ],
                              ),
                              const SizedBox(height: AppSpacing.sm),
                              Text(
                                'Data de envio: ${doc.dataEnvio.day.toString().padLeft(2, '0')}/${doc.dataEnvio.month.toString().padLeft(2, '0')}/${doc.dataEnvio.year}',
                                style: const TextStyle(color: BravitoColors.cinzaEscuro, fontSize: 13),
                              ),
                              if (doc.tamanho != null)
                                Text(
                                  'Tamanho: ${doc.tamanho}',
                                  style: const TextStyle(color: BravitoColors.cinzaEscuro, fontSize: 13),
                                ),
                              if (doc.chunks != null)
                                Text(
                                  'Chunks processados: ${doc.chunks}',
                                  style: const TextStyle(color: BravitoColors.cinzaEscuro, fontSize: 13),
                                ),
                              const SizedBox(height: AppSpacing.sm),
                              Row(
                                mainAxisAlignment: MainAxisAlignment.end,
                                children: [
                                  TextButton.icon(
                                    onPressed: () => _acaoMockada('Substituir Documento'),
                                    icon: const Icon(Icons.sync, size: 18),
                                    label: const Text('Substituir'),
                                    style: TextButton.styleFrom(
                                      foregroundColor: BravitoColors.azulSecundario,
                                    ),
                                  ),
                                  const SizedBox(width: AppSpacing.sm),
                                  TextButton.icon(
                                    onPressed: () => _acaoMockada('Excluir Documento'),
                                    icon: const Icon(Icons.delete_outline, size: 18),
                                    label: const Text('Excluir'),
                                    style: TextButton.styleFrom(
                                      foregroundColor: Colors.red.shade700,
                                    ),
                                  ),
                                ],
                              )
                            ],
                          ),
                        );
                      },
                    ),
            ),
          ],
        ),
      ),
    );
  }
}
