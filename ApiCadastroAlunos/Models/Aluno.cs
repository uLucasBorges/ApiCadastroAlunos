using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiCadastroAlunos.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do aluno é obrigatório.")]
        [StringLength(50, MinimumLength = 1 , ErrorMessage = "o tamanho minimo do nome de aluno é de {2} e o máximo de {1} caracteres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome do aluno é obrigatório.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "o tamanho minimo do sobrenome de aluno é de {2} e o máximo de {1} caracteres.")]
        public string? Sobrenome { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "O email do aluno é obrigatório.")]
        public string? email { get; set; }

        [Phone]
        [Required(ErrorMessage = "o número de telefone do aluno é obrigatório.")]
        public string? celular { get; set; }

        [Required(ErrorMessage = "é obrigatório se matricular em uma aula")]
        public int? professorId { get; set; }
        //[JsonIgnore]
        //public Professor? professor { get; set; }


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
