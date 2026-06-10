using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bravito.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingModelChanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("08a786d2-1303-4ee0-9bf2-2b617bda8031"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("1e321de9-4572-4f38-bf68-685a0873e9d6"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("3e428deb-7d98-47a3-9717-b4d98b387d44"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("536153eb-7a4f-44b0-b166-f3ea11c0eaac"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("54fe480a-cba7-4a18-bdc5-31540f12d1f5"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("6ed3f254-473a-4123-8554-478ae1eb9c69"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("7500289c-4c5c-4d7d-bd3f-bafe10990f19"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("78997cb4-48fe-4fe4-89c3-700d604ad220"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("c0e834a8-527c-4402-a402-70f458f103de"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 9, 10, 15, DateTimeKind.Utc).AddTicks(2254));

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 9, 10, 15, DateTimeKind.Utc).AddTicks(2646));

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 9, 10, 15, DateTimeKind.Utc).AddTicks(2649));

            migrationBuilder.InsertData(
                table: "perfis_acesso_recursos",
                columns: new[] { "id", "data_criacao", "perfil_acesso_id", "recurso_id" },
                values: new object[,]
                {
                    { new Guid("08a786d2-1303-4ee0-9bf2-2b617bda8031"), new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(6305), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000005") },
                    { new Guid("1e321de9-4572-4f38-bf68-685a0873e9d6"), new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(6330), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("3e428deb-7d98-47a3-9717-b4d98b387d44"), new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(6327), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a0000000-0000-0000-0000-000000000002") },
                    { new Guid("536153eb-7a4f-44b0-b166-f3ea11c0eaac"), new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(5902), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("54fe480a-cba7-4a18-bdc5-31540f12d1f5"), new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(6313), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000006") },
                    { new Guid("6ed3f254-473a-4123-8554-478ae1eb9c69"), new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(6324), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a0000000-0000-0000-0000-000000000001") },
                    { new Guid("7500289c-4c5c-4d7d-bd3f-bafe10990f19"), new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(6301), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000003") },
                    { new Guid("78997cb4-48fe-4fe4-89c3-700d604ad220"), new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(6303), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000004") },
                    { new Guid("c0e834a8-527c-4402-a402-70f458f103de"), new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(6291), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000002") }
                });

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(2132));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(2530));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(2557));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(2561));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(2565));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 2, 9, 10, 16, DateTimeKind.Utc).AddTicks(2576));
        }
    }
}
