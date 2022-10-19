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
        public string email { get; set; }
        public string celular { get; set; }
        public int professorId { get; set; }
     

        public Aluno()
        {
        }

        public Aluno(int id, string nome, string sobrenome, string email, string celular, int professorId)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            this.email = email;
            this.celular = celular;
            this.professorId = professorId;
        }
    }
}
