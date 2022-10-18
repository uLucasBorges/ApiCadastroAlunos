using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCadastroAlunos.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Professores_Professorid",
                table: "Alunos");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_Professorid",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Professores",
                newName: "NomeProfessor");

            migrationBuilder.RenameColumn(
                name: "Professorid",
                table: "Alunos",
                newName: "professorId");

            migrationBuilder.AddColumn<int>(
                name: "Alunos",
                table: "Professores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "professor",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alunos",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "professor",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "NomeProfessor",
                table: "Professores",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "professorId",
                table: "Alunos",
                newName: "Professorid");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_Professorid",
                table: "Alunos",
                column: "Professorid");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Professores_Professorid",
                table: "Alunos",
                column: "Professorid",
                principalTable: "Professores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
