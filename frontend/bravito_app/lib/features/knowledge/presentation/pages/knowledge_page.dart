import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:file_picker/file_picker.dart';
import '../../../../core/constants/app_colors.dart';
import '../../../../core/constants/app_spacing.dart';
import '../../../../shared/widgets/bravito_app_scaffold.dart';
import '../../../../shared/widgets/bravito_card.dart';
import '../controllers/knowledge_controller.dart';
import '../../data/services/knowledge_service.dart';

class KnowledgePage extends ConsumerStatefulWidget {
  const KnowledgePage({super.key});

  @override
  ConsumerState<KnowledgePage> createState() => _KnowledgePageState();
}

class _KnowledgePageState extends ConsumerState<KnowledgePage> {
  bool _isUploading = false;

  void _showError(Object error) {
    if (!mounted) return;
    String message = 'Ocorreu um erro inesperado.';
    if (error is KnowledgeApiException) {
      message = error.message;
    } else if (error is Exception) {
      message = error.toString().replaceAll('Exception: ', '');
    }
    
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(message),
        backgroundColor: Colors.red,
      ),
    );
  }

  void _showSuccess(String message) {
    if (!mounted) return;
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(message),
        backgroundColor: Colors.green,
      ),
    );
  }

  Future<void> _handleUpload() async {
    if (_isUploading) return;
    
    try {
      final result = await FilePicker.pickFiles(
        type: FileType.custom,
        allowedExtensions: ['pdf', 'docx', 'txt'],
        withData: true,
      );

      if (result != null && result.files.isNotEmpty) {
        final file = result.files.first;
        if (file.bytes == null) {
          _showError('Não foi possível ler os dados do arquivo.');
          return;
        }

        setState(() => _isUploading = true);

        await ref.read(knowledgeControllerProvider.notifier).uploadDocument(
              file.bytes!,
              file.name,
            );

        _showSuccess('Documento adicionado com sucesso!');
      }
    } catch (e) {
      _showError(e);
    } finally {
      if (mounted) {
        setState(() => _isUploading = false);
      }
    }
  }

  Future<void> _handleReplace(String id) async {
    if (_isUploading) return;
    
    try {
      final result = await FilePicker.pickFiles(
        type: FileType.custom,
        allowedExtensions: ['pdf', 'docx', 'txt'],
        withData: true,
      );

      if (result != null && result.files.isNotEmpty) {
        final file = result.files.first;
        if (file.bytes == null) {
          _showError('Não foi possível ler os dados do arquivo.');
          return;
        }

        setState(() => _isUploading = true);

        await ref.read(knowledgeControllerProvider.notifier).replaceDocument(
              id,
              file.bytes!,
              file.name,
            );

        _showSuccess('Documento substituído com sucesso!');
      }
    } catch (e) {
      _showError(e);
    } finally {
      if (mounted) {
        setState(() => _isUploading = false);
      }
    }
  }

  Future<void> _handleDelete(String id) async {
    final confirm = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Excluir documento'),
        content: const Text(
            'Ao excluir este documento, o assistente deixará de usar esse conteúdo nas respostas. Deseja continuar?'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context, false),
            child: const Text('Cancelar'),
          ),
          ElevatedButton(
            style: ElevatedButton.styleFrom(backgroundColor: Colors.red),
            onPressed: () => Navigator.pop(context, true),
            child: const Text('Excluir', style: TextStyle(color: Colors.white)),
          ),
        ],
      ),
    );

    if (confirm != true) return;

    setState(() => _isUploading = true);
    try {
      await ref.read(knowledgeControllerProvider.notifier).deleteDocument(id);
      _showSuccess('Documento excluído com sucesso!');
    } catch (e) {
      _showError(e);
    } finally {
      if (mounted) {
        setState(() => _isUploading = false);
      }
    }
  }

  Future<void> _handleReprocess(String id) async {
    final confirm = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Reprocessar documento'),
        content: const Text('Deseja reprocessar este documento?'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context, false),
            child: const Text('Cancelar'),
          ),
          ElevatedButton(
            onPressed: () => Navigator.pop(context, true),
            child: const Text('Reprocessar'),
          ),
        ],
      ),
    );

    if (confirm != true) return;

    setState(() => _isUploading = true);
    try {
      await ref.read(knowledgeControllerProvider.notifier).reprocessDocument(id);
      _showSuccess('Reprocessamento iniciado com sucesso!');
    } catch (e) {
      _showError(e);
    } finally {
      if (mounted) {
        setState(() => _isUploading = false);
      }
    }
  }

  void _checkHealth() async {
    setState(() => _isUploading = true);
    try {
      final service = ref.read(knowledgeServiceProvider);
      final health = await service.getKnowledgeHealth();
      if (!mounted) return;
      showDialog(
        context: context,
        builder: (context) => AlertDialog(
          title: const Text('Health Check'),
          content: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: health.entries
                .map((e) => Text('${e.key}: ${e.value}'))
                .toList(),
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: const Text('OK'),
            ),
          ],
        ),
      );
    } catch (e) {
      _showError(e);
    } finally {
      if (mounted) {
        setState(() => _isUploading = false);
      }
    }
  }

  String _formatDate(DateTime date) {
    return '${date.day.toString().padLeft(2, '0')}/${date.month.toString().padLeft(2, '0')}/${date.year} ${date.hour.toString().padLeft(2, '0')}:${date.minute.toString().padLeft(2, '0')}';
  }

  String _formatSize(int? bytes) {
    if (bytes == null) return '-';
    if (bytes < 1024) return '$bytes B';
    if (bytes < 1024 * 1024) return '${(bytes / 1024).toStringAsFixed(1)} KB';
    return '${(bytes / (1024 * 1024)).toStringAsFixed(1)} MB';
  }

  @override
  Widget build(BuildContext context) {
    final state = ref.watch(knowledgeControllerProvider);

    return BravitoAppScaffold(
      title: 'Base de Conhecimento',
      actions: [
        IconButton(
          icon: const Icon(Icons.monitor_heart_outlined),
          tooltip: 'Verificar conexão',
          onPressed: _isUploading ? null : _checkHealth,
        ),
      ],
      body: Stack(
        children: [
          Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              Padding(
                padding: const EdgeInsets.all(AppSpacing.md),
                child: Text(
                  'Gerencie os documentos que a IA usa como fonte para responder perguntas.',
                  style: Theme.of(context).textTheme.bodyLarge,
                ),
              ),
              Padding(
                padding: const EdgeInsets.symmetric(horizontal: AppSpacing.md),
                child: ElevatedButton.icon(
                  onPressed: _isUploading ? null : _handleUpload,
                  icon: const Icon(Icons.upload_file),
                  label: const Text('Adicionar documento'),
                ),
              ),
              const SizedBox(height: AppSpacing.md),
              Expanded(
                child: state.when(
                  loading: () => const Center(child: CircularProgressIndicator()),
                  error: (err, stack) => Center(
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        const Icon(Icons.error_outline, color: Colors.red, size: 48),
                        const SizedBox(height: 16),
                        Text('Erro ao carregar documentos: $err'),
                        TextButton(
                          onPressed: () => ref.read(knowledgeControllerProvider.notifier).loadDocuments(),
                          child: const Text('Tentar novamente'),
                        ),
                      ],
                    ),
                  ),
                  data: (documents) {
                    if (documents.isEmpty) {
                      return const Center(
                        child: Text('Nenhum documento adicionado ainda.'),
                      );
                    }

                    return ListView.builder(
                      padding: const EdgeInsets.all(AppSpacing.md),
                      itemCount: documents.length,
                      itemBuilder: (context, index) {
                        final doc = documents[index];
                        return Padding(
                          padding: const EdgeInsets.only(bottom: AppSpacing.md),
                          child: BravitoCard(
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Row(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Expanded(
                                    child: Text(
                                      doc.fileName,
                                      style: const TextStyle(
                                          fontWeight: FontWeight.bold,
                                          fontSize: 16),
                                    ),
                                  ),
                                  Container(
                                    padding: const EdgeInsets.symmetric(
                                        horizontal: 8, vertical: 4),
                                    decoration: BoxDecoration(
                                      color: _getStatusColor(doc.status),
                                      borderRadius: BorderRadius.circular(12),
                                    ),
                                    child: Text(
                                      doc.friendlyStatus,
                                      style: const TextStyle(
                                          color: Colors.white, fontSize: 12),
                                    ),
                                  ),
                                ],
                              ),
                              const SizedBox(height: 8),
                              Text('Enviado em: ${_formatDate(doc.uploadedAt)}'),
                              if (doc.processedAt != null)
                                Text('Processado em: ${_formatDate(doc.processedAt!)}'),
                              if (doc.chunkCount != null)
                                Text('Chunks: ${doc.chunkCount}'),
                              if (doc.fileSize != null)
                                Text('Tamanho: ${_formatSize(doc.fileSize)}'),
                              if (doc.status == 'error' && doc.errorMessage != null)
                                Padding(
                                  padding: const EdgeInsets.only(top: 8.0),
                                  child: Text(
                                    'Erro: ${doc.errorMessage}',
                                    style: const TextStyle(color: Colors.red),
                                  ),
                                ),
                              const Divider(),
                              Row(
                                mainAxisAlignment: MainAxisAlignment.end,
                                children: [
                                  TextButton.icon(
                                    onPressed: _isUploading
                                        ? null
                                        : () => _handleReprocess(doc.id),
                                    icon: const Icon(Icons.refresh, size: 16),
                                    label: const Text('Reprocessar'),
                                  ),
                                  TextButton.icon(
                                    onPressed: _isUploading
                                        ? null
                                        : () => _handleReplace(doc.id),
                                    icon: const Icon(Icons.file_upload, size: 16),
                                    label: const Text('Substituir'),
                                  ),
                                  TextButton.icon(
                                    onPressed: _isUploading
                                        ? null
                                        : () => _handleDelete(doc.id),
                                    icon: const Icon(Icons.delete_outline,
                                        size: 16, color: Colors.red),
                                    label: const Text('Excluir',
                                        style: TextStyle(color: Colors.red)),
                                  ),
                                ],
                              )
                            ],
                          ),
                        ),
                      );
                    },
                    );
                  },
                ),
              ),
            ],
          ),
          if (_isUploading)
            Container(
              color: Colors.black54,
              child: const Center(
                child: CircularProgressIndicator(),
              ),
            ),
        ],
      ),
    );
  }

  Color _getStatusColor(String status) {
    switch (status.toLowerCase()) {
      case 'uploaded':
        return Colors.blue;
      case 'processing':
        return Colors.orange;
      case 'processed':
        return Colors.green;
      case 'error':
        return Colors.red;
      case 'deleted':
        return Colors.grey;
      default:
        return Colors.grey;
    }
  }
}
