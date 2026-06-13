import 'dart:async';
import 'dart:typed_data';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../domain/models/knowledge_document.dart';
import '../../data/services/knowledge_service.dart';

class KnowledgeController extends AsyncNotifier<List<KnowledgeDocument>> {
  FutureOr<List<KnowledgeDocument>> build() async {
    return _fetchDocuments();
  }

  Future<List<KnowledgeDocument>> _fetchDocuments() async {
    final service = ref.read(knowledgeServiceProvider);
    return await service.getKnowledgeDocuments();
  }

  Future<void> loadDocuments() async {
    state = const AsyncValue.loading();
    state = await AsyncValue.guard(() => _fetchDocuments());
  }

  Future<void> uploadDocument(Uint8List fileBytes, String fileName) async {
    final service = ref.read(knowledgeServiceProvider);
    await service.uploadKnowledgeDocument(fileBytes, fileName, app: 'bravito');
    await loadDocuments();
  }

  Future<void> deleteDocument(String id) async {
    final service = ref.read(knowledgeServiceProvider);
    await service.deleteKnowledgeDocument(id);
    await loadDocuments();
  }

  Future<void> replaceDocument(String id, Uint8List fileBytes, String fileName) async {
    final service = ref.read(knowledgeServiceProvider);
    await service.replaceKnowledgeDocument(id, fileBytes, fileName);
    await loadDocuments();
  }

  Future<void> reprocessDocument(String id) async {
    final service = ref.read(knowledgeServiceProvider);
    await service.reprocessKnowledgeDocument(id);
    await loadDocuments();
  }
}

final knowledgeControllerProvider =
    AsyncNotifierProvider<KnowledgeController, List<KnowledgeDocument>>(
  () => KnowledgeController(),
);
