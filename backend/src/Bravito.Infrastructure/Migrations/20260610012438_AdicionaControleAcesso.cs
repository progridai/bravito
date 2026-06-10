using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bravito.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaControleAcesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "perfis_acesso",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfis_acesso", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "recursos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    codigo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    descricao = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recursos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    keycloak_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_alteracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "perfis_acesso_recursos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    perfil_acesso_id = table.Column<Guid>(type: "uuid", nullable: false),
                    recurso_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfis_acesso_recursos", x => x.id);
                    table.ForeignKey(
                        name: "FK_perfis_acesso_recursos_perfis_acesso_perfil_acesso_id",
                        column: x => x.perfil_acesso_id,
                        principalTable: "perfis_acesso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_perfis_acesso_recursos_recursos_recurso_id",
                        column: x => x.recurso_id,
                        principalTable: "recursos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usuarios_perfis_acesso",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    perfil_acesso_id = table.Column<Guid>(type: "uuid", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios_perfis_acesso", x => x.id);
                    table.ForeignKey(
                        name: "FK_usuarios_perfis_acesso_perfis_acesso_perfil_acesso_id",
                        column: x => x.perfil_acesso_id,
                        principalTable: "perfis_acesso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuarios_perfis_acesso_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "perfis_acesso",
                columns: new[] { "id", "ativo", "data_alteracao", "data_criacao", "descricao", "nome" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), true, null, new DateTime(2026, 6, 10, 1, 24, 33, 782, DateTimeKind.Utc).AddTicks(7858), "Acesso total ao sistema", "Administrador" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), true, null, new DateTime(2026, 6, 10, 1, 24, 33, 782, DateTimeKind.Utc).AddTicks(8496), "Acesso a operações diárias e chat", "Operador" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), true, null, new DateTime(2026, 6, 10, 1, 24, 33, 782, DateTimeKind.Utc).AddTicks(8501), "Acesso restrito ao chat", "Somente Chat" }
                });

            migrationBuilder.InsertData(
                table: "recursos",
                columns: new[] { "id", "ativo", "codigo", "data_alteracao", "data_criacao", "descricao", "nome" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000001"), true, "chat.acessar", null, new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(713), "Permite acessar e enviar mensagens no chat", "Acessar Chat" },
                    { new Guid("a0000000-0000-0000-0000-000000000002"), true, "conversas.visualizar", null, new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(1213), "Permite visualizar histórico de conversas", "Visualizar Conversas" },
                    { new Guid("a0000000-0000-0000-0000-000000000003"), true, "usuarios.visualizar", null, new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(1221), "Permite visualizar lista de usuários", "Visualizar Usuários" },
                    { new Guid("a0000000-0000-0000-0000-000000000004"), true, "usuarios.cadastrar", null, new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(1245), "Permite cadastrar novos usuários", "Cadastrar Usuários" },
                    { new Guid("a0000000-0000-0000-0000-000000000005"), true, "usuarios.editar", null, new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(1249), "Permite editar usuários existentes", "Editar Usuários" },
                    { new Guid("a0000000-0000-0000-0000-000000000006"), true, "usuarios.desativar", null, new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(1267), "Permite desativar/ativar usuários", "Desativar Usuários" }
                });

            migrationBuilder.InsertData(
                table: "perfis_acesso_recursos",
                columns: new[] { "id", "data_criacao", "perfil_acesso_id", "recurso_id" },
                values: new object[,]
                {
                    { new Guid("0e742b5f-a6c4-46c1-864f-dd85e950207f"), new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(5086), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("28358a04-9e81-48b2-b8b0-54d0f399a223"), new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(5064), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000003") },
                    { new Guid("304bf687-de5b-4122-9182-5b14ca475800"), new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(5092), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("6f1461ca-9f5a-4b7a-a82c-5c852ddcf794"), new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(5068), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000005") },
                    { new Guid("70bdf262-235c-4773-a788-755a004772ab"), new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(4663), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("b1c000dd-cde3-4e22-9f49-cb30cc9e2510"), new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(5066), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000004") },
                    { new Guid("c74a4d20-085e-4a9d-a24a-982d0660b629"), new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(5075), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000006") },
                    { new Guid("df2eec64-573f-4cfd-9694-d7aa62411323"), new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(5053), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000002") },
                    { new Guid("ee0a0225-c756-4b89-aa56-86cc2b77da35"), new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(5089), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a0000000-0000-0000-0000-000000000002") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_perfis_acesso_nome",
                table: "perfis_acesso",
                column: "nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_perfis_acesso_recursos_perfil_acesso_id_recurso_id",
                table: "perfis_acesso_recursos",
                columns: new[] { "perfil_acesso_id", "recurso_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_perfis_acesso_recursos_recurso_id",
                table: "perfis_acesso_recursos",
                column: "recurso_id");

            migrationBuilder.CreateIndex(
                name: "IX_recursos_codigo",
                table: "recursos",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_keycloak_id",
                table: "usuarios",
                column: "keycloak_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_perfis_acesso_perfil_acesso_id",
                table: "usuarios_perfis_acesso",
                column: "perfil_acesso_id");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_perfis_acesso_usuario_id_perfil_acesso_id",
                table: "usuarios_perfis_acesso",
                columns: new[] { "usuario_id", "perfil_acesso_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "perfis_acesso_recursos");

            migrationBuilder.DropTable(
                name: "usuarios_perfis_acesso");

            migrationBuilder.DropTable(
                name: "recursos");

            migrationBuilder.DropTable(
                name: "perfis_acesso");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
