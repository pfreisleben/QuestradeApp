using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ScoreApi.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Occurrences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occurrences", x => x.Id);
                    table.UniqueConstraint("AK_Occurrences_Description", x => x.Description);
                });

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
                    { 6, "Cadastrou valor da renda mensal", 30 },
                    { 7, "Usuário foi criado", 500 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Occurrences");
        }
    }
}
