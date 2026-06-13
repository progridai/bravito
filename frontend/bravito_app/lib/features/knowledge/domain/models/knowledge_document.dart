class KnowledgeDocument {
  final String id;
  final String fileName;
  final String status;
  final DateTime uploadedAt;
  final DateTime? processedAt;
  final int? chunkCount;
  final int? fileSize;
  final String? errorMessage;
  final String? app;

  KnowledgeDocument({
    required this.id,
    required this.fileName,
    required this.status,
    required this.uploadedAt,
    this.processedAt,
    this.chunkCount,
    this.fileSize,
    this.errorMessage,
    this.app,
  });

  factory KnowledgeDocument.fromJson(Map<String, dynamic> json) {
    return KnowledgeDocument(
      id: json['id'] as String,
      fileName: json['fileName'] as String? ?? 'Desconhecido',
      status: json['status'] as String? ?? 'uploaded',
      uploadedAt: json['uploadedAt'] != null 
          ? DateTime.parse(json['uploadedAt'] as String) 
          : DateTime.now(),
      processedAt: json['processedAt'] != null 
          ? DateTime.parse(json['processedAt'] as String) 
          : null,
      chunkCount: json['chunkCount'] as int?,
      fileSize: json['fileSize'] as int?,
      errorMessage: json['errorMessage'] as String?,
      app: json['app'] as String?,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'fileName': fileName,
      'status': status,
      'uploadedAt': uploadedAt.toIso8601String(),
      'processedAt': processedAt?.toIso8601String(),
      'chunkCount': chunkCount,
      'fileSize': fileSize,
      'errorMessage': errorMessage,
      'app': app,
    };
  }

  String get friendlyStatus {
    switch (status.toLowerCase()) {
      case 'uploaded':
        return 'Enviado';
      case 'processing':
        return 'Processando';
      case 'processed':
        return 'Processado';
      case 'error':
        return 'Erro';
      case 'deleted':
        return 'Excluído';
      default:
        return status;
    }
  }
}
