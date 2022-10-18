using System.ComponentModel.DataAnnotations;

namespace ApiCadastroAlunos.Models
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Insira um nome.")]
        public string NomeProfessor { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Insira uma matéria.")]
        public string Materia { get; set; }

        public int? Alunos { get; set; }

    }
}
