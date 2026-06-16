using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bravito.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaRecursoBaseConhecimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(4587));

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(4667));

            migrationBuilder.UpdateData(
                table: "perfis_acesso",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(4677));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000001"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(9297));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000002"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000003"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(9386));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000004"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(9389));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000005"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(9393));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000006"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(9397));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000101"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(9402));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000102"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(9404));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000201"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(9408));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(8182));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(8301));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(8305));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(8307));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(8309));

            migrationBuilder.UpdateData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(8313));

            migrationBuilder.InsertData(
                table: "recursos",
                columns: new[] { "id", "ativo", "codigo", "data_alteracao", "data_criacao", "descricao", "nome" },
                values: new object[] { new Guid("a0000000-0000-0000-0000-000000000007"), true, "base_conhecimento.acessar", null, new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(8316), "Permite acessar a Base de Conhecimento", "Acessar Base de Conhecimento" });

            migrationBuilder.InsertData(
                table: "perfis_acesso_recursos",
                columns: new[] { "id", "data_criacao", "perfil_acesso_id", "recurso_id" },
                values: new object[] { new Guid("b0000000-0000-0000-0000-000000000007"), new DateTime(2026, 6, 16, 21, 24, 11, 226, DateTimeKind.Utc).AddTicks(9400), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("a0000000-0000-0000-0000-000000000007") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "recursos",
                keyColumn: "id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000007"));

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

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000001"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(2943));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000002"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3329));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000003"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3335));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000004"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3339));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000005"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3342));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000006"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3348));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000101"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3359));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000102"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3362));

            migrationBuilder.UpdateData(
                table: "perfis_acesso_recursos",
                keyColumn: "id",
                keyValue: new Guid("b0000000-0000-0000-0000-000000000201"),
                column: "data_criacao",
                value: new DateTime(2026, 6, 11, 3, 54, 7, 558, DateTimeKind.Utc).AddTicks(3365));

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
        }
    }
}
