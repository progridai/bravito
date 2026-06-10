using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bravito.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPendingModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("0e742b5f-a6c4-46c1-864f-dd85e950207f"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("28358a04-9e81-48b2-b8b0-54d0f399a223"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("304bf687-de5b-4122-9182-5b14ca475800"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("6f1461ca-9f5a-4b7a-a82c-5c852ddcf794"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("70bdf262-235c-4773-a788-755a004772ab"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b1c000dd-cde3-4e22-9f49-cb30cc9e2510"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("c74a4d20-085e-4a9d-a24a-982d0660b629"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("df2eec64-573f-4cfd-9694-d7aa62411323"));

            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("ee0a0225-c756-4b89-aa56-86cc2b77da35"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: new DateTime(2026, 6, 10, 1, 24, 33, 782, DateTimeKind.Utc).AddTicks(7858));

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 1, 24, 33, 782, DateTimeKind.Utc).AddTicks(8496));

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 1, 24, 33, 782, DateTimeKind.Utc).AddTicks(8501));

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

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(713));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(1213));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(1221));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(1245));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(1249));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 10, 1, 24, 33, 784, DateTimeKind.Utc).AddTicks(1267));
        }
    }
}
