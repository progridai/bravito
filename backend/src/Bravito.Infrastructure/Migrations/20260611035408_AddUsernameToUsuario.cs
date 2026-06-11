using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bravito.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsernameToUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("1ccc16be-e08f-4e95-830d-c9cd038f7521"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("41ffaebc-094b-48e2-8cfc-3cc3df4199de"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("425ca36a-c9ef-42a2-8c65-4de0daef02e4"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("493fc9d8-0787-4047-9b0f-9794058f7a31"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("9dc5fa23-d249-4c00-b28a-231acae0ce37"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("c73b0af7-76ac-4e22-a462-573c7ee8512d"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("d2704adf-88f5-496c-9b1e-9c4d3dcda463"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("e817a276-964a-49d3-95e9-3b53a73b7299"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("f0a781ce-4290-425e-8c60-2f3c68d0173a"));

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "usuarios",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.Sql("UPDATE usuarios SET username = email WHERE username IS NULL;");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "usuarios",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 556, DateTimeKind.Utc).AddTicks(9521));

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 556, DateTimeKind.Utc).AddTicks(9929));

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 556, DateTimeKind.Utc).AddTicks(9933));

            migrationBuilder.InsertData(
                table: "perfis_acesso_recursos",
                columns: new[] { "id", "data_criacao", "perfil_acesso_id", "recurso_id" },
                values: new object[,]
                {
                    { new Guid("b0000000-0000-0000-0000-000000000001"), new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(2943), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("b0000000-0000-0000-0000-000000000002"), new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3329), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000002") },
                    { new Guid("b0000000-0000-0000-0000-000000000003"), new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3335), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000003") },
                    { new Guid("b0000000-0000-0000-0000-000000000004"), new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3339), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000004") },
                    { new Guid("b0000000-0000-0000-0000-000000000005"), new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3342), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000005") },
                    { new Guid("b0000000-0000-0000-0000-000000000006"), new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3348), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000006") },
                    { new Guid("b0000000-0000-0000-0000-000000000101"), new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3359), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("b0000000-0000-0000-0000-000000000102"), new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3362), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a0000000-0000-0000-0000-000000000002") },
                    { new Guid("b0000000-0000-0000-0000-000000000201"), new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3365), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a0000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 557, DateTimeKind.Utc).AddTicks(9263));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 557, DateTimeKind.Utc).AddTicks(9701));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 557, DateTimeKind.Utc).AddTicks(9706));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 557, DateTimeKind.Utc).AddTicks(9710));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 557, DateTimeKind.Utc).AddTicks(9734));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 557, DateTimeKind.Utc).AddTicks(9741));

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_username",
                table: "usuarios",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuarios_username",
                table: "usuarios");

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000101"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000102"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000201"));

            migrationBuilder.DropColumn(
                name: "username",
                table: "usuarios");

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 14, 5, 691, DateTimeKind.Utc).AddTicks(1447));

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 14, 5, 691, DateTimeKind.Utc).AddTicks(2067));

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 14, 5, 691, DateTimeKind.Utc).AddTicks(2073));

            migrationBuilder.InsertData(
                table: "perfis_acesso_recursos",
                columns: new[] { "id", "data_criacao", "perfil_acesso_id", "recurso_id" },
                values: new object[,]
                {
                    { new Guid("1ccc16be-e08f-4e95-830d-c9cd038f7521"), new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(9352), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("41ffaebc-094b-48e2-8cfc-3cc3df4199de"), new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(9344), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000006") },
                    { new Guid("425ca36a-c9ef-42a2-8c65-4de0daef02e4"), new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(9358), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a0000000-0000-0000-0000-000000000002") },
                    { new Guid("493fc9d8-0787-4047-9b0f-9794058f7a31"), new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(9323), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000002") },
                    { new Guid("9dc5fa23-d249-4c00-b28a-231acae0ce37"), new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(9361), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("c73b0af7-76ac-4e22-a462-573c7ee8512d"), new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(9326), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000003") },
                    { new Guid("d2704adf-88f5-496c-9b1e-9c4d3dcda463"), new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(8941), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("e817a276-964a-49d3-95e9-3b53a73b7299"), new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(9336), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000004") },
                    { new Guid("f0a781ce-4290-425e-8c60-2f3c68d0173a"), new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(9338), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000005") }
                });

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(5299));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(5742));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(5747));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(5752));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(5756));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 14, 5, 692, DateTimeKind.Utc).AddTicks(5791));
        }
    }
}
