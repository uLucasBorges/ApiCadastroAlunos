using System.ComponentModel.DataAnnotations;

namespace ApiCadastroAlunos.Models
{
    public class Professor
    {
        public int? qntdAlunos { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Insira um nome.")]
        public string NomeProfessor { get; set; }

        [Key]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Insira uma matéria.")]
        public string Materia { get; set; }

        public Professor(int? qntdAlunos, string nomeProfessor, int id, string materia)
        {
            this.qntdAlunos = qntdAlunos;
            NomeProfessor = nomeProfessor;
            Id = id;
            Materia = materia;
        }
    }
}
