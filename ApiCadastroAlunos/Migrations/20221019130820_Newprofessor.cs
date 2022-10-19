using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCadastroAlunos.Migrations
{
    public partial class Newprofessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "professor",
                table: "Alunos",
                newName: "email");

            migrationBuilder.AddColumn<string>(
                name: "celular",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "celular",
                table: "Alunos");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Alunos",
                newName: "professor");
        }
    }
}
