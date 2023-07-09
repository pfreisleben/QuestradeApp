using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreApi.Infrastructure.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Occurrences",
                columns: new[] { "Id", "Description", "Value" },
                values: new object[,]
                {
                    { 1, "Conta paga antes do vencimento", 20 },
                    { 2, "Conta paga depois do vencimento", -20 },
                    { 3, "Finalizou o pagamento do empréstimo", 60 },
                    { 4, "Atrasou a parcela do empréstimo", -40 },
                    { 5, "Contratou um empréstimo", -50 },
                    { 6, "Cadastrou valor da renda mensal", 30 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Occurrences",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Occurrences",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Occurrences",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Occurrences",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Occurrences",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Occurrences",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
