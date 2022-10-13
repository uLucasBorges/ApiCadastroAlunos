using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiCadastroAlunos.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 1 , ErrorMessage = "Insira um nome.")]
        public string Nome { get; set; }
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Insira um sobrenome.")]
        public string Sobrenome { get; set; }
        public int professorId { get; set; }

        public Aluno(int id, string nome, string sobrenome, int professorId)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            this.professorId = professorId;
        }
    }
}
