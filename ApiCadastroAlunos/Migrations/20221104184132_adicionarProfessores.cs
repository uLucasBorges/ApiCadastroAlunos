using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCadastroAlunos.Migrations
{
    public partial class adicionarProfessores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Alunos_professorId",
                table: "Alunos",
                column: "professorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Professores_professorId",
                table: "Alunos",
                column: "professorId",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Professores_professorId",
                table: "Alunos");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_professorId",
                table: "Alunos");
        }
    }
}
