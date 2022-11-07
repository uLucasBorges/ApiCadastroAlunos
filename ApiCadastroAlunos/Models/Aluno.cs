using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiCadastroAlunos.Models.Validators;

namespace ApiCadastroAlunos.Models
{
    public class Aluno : Base
    {
        [Key]
        public int Id { get; private set; }

        [Required(ErrorMessage = "O nome do aluno é obrigatório.")]
        [StringLength(50, MinimumLength = 3 , ErrorMessage = "o tamanho minimo do nome de aluno é de {2} e o máximo de {1} caracteres.")]
        public string? Nome { get; private set; }

        [Required(ErrorMessage = "O sobrenome do aluno é obrigatório.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "o tamanho minimo do sobrenome de aluno é de {2} e o máximo de {1} caracteres.")]
        public string? Sobrenome { get; private set; }

        [StringLength(180, MinimumLength = 10, ErrorMessage = "o tamanho minimo do email é de {2} e o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "O email do aluno é obrigatório.")]
        public string? email { get; private set; }

        [StringLength(15, MinimumLength = 10, ErrorMessage = "o tamanho minimo do telefone de aluno é de {2} e o máximo de {1} caracteres.")]
        [Required(ErrorMessage = "o número de telefone do aluno é obrigatório.")]
        public string? celular { get; private set; }

        [Required(ErrorMessage = "é obrigatório se matricular em uma aula")]
        public int? professorId { get; private set; }

        //[JsonIgnore]
        //public Professor? professor { get; private set; }


        public Aluno() {}

        public Aluno(int id, string? nome, string? sobrenome, string? email, string? celular, int? professorId)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            this.email = email;
            this.celular = celular;
            this.professorId = professorId;
            _errors = new List<string>();
            Validate();
        }

        public bool Validate() => base.Validate(new AlunoValidator(), this);

    }
}
