using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bravito.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "conversas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    usuario_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    identificador_externo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    canal_origem = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_ultima_interacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    metadados = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conversas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "conversas_contextos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    conversa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    resumo_atual = table.Column<string>(type: "text", nullable: false),
                    dados_auxiliares = table.Column<string>(type: "jsonb", nullable: true),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conversas_contextos", x => x.id);
                    table.ForeignKey(
                        name: "FK_conversas_contextos_conversas_conversa_id",
                        column: x => x.conversa_id,
                        principalTable: "conversas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "conversas_eventos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    conversa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo_evento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    detalhes = table.Column<string>(type: "jsonb", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conversas_eventos", x => x.id);
                    table.ForeignKey(
                        name: "FK_conversas_eventos_conversas_conversa_id",
                        column: x => x.conversa_id,
                        principalTable: "conversas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "conversas_mensagens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    conversa_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tipo_remetente = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    conteudo = table.Column<string>(type: "text", nullable: false),
                    conteudo_bruto = table.Column<string>(type: "jsonb", nullable: true),
                    tokens_entrada = table.Column<int>(type: "integer", nullable: true),
                    tokens_saida = table.Column<int>(type: "integer", nullable: true),
                    modelo_usado = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conversas_mensagens", x => x.id);
                    table.ForeignKey(
                        name: "FK_conversas_mensagens_conversas_conversa_id",
                        column: x => x.conversa_id,
                        principalTable: "conversas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_conversas_usuario_id",
                table: "conversas",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_conversas_contextos_conversa_id",
                table: "conversas_contextos",
                column: "conversa_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_conversas_eventos_conversa_id",
                table: "conversas_eventos",
                column: "conversa_id");

            migrationBuilder.CreateIndex(
                name: "IX_conversas_mensagens_conversa_id",
                table: "conversas_mensagens",
                column: "conversa_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "conversas_contextos");

            migrationBuilder.DropTable(
                name: "conversas_eventos");

            migrationBuilder.DropTable(
                name: "conversas_mensagens");

            migrationBuilder.DropTable(
                name: "conversas");
        }
    }
}
