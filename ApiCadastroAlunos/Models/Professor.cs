using System.ComponentModel.DataAnnotations;
using ApiCadastroAlunos.Models.Validators;
using Newtonsoft.Json;

namespace ApiCadastroAlunos.Models
{
    
    public class Professor : Base
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "o nome é obrigatório.")]
        [StringLength(50 , MinimumLength = 5, ErrorMessage = "O nome deve conter o mínimo de {2} e máximo de {1} caracteres")]
        public string? Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O Sobrenome deve conter o mínimo de {2} e máximo de {1} caracteres")]
        public string? Sobrenome { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "O cep deve conter o mínimo de {2} e máximo de {1} digitos.")]
        public string? Cep { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "O logradouro deve conter o mínimo de {2} e máximo de {1} digitos.")]
        public string? logradouro { get ; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O cep deve conter o mínimo de {2} e máximo de {1} digitos.")]
        public string? Cidade { get ; set; }

        [Required]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "O cep deve conter o mínimo de {2} e máximo de {1} digitos.")]
        public string? Celular { get ; set; }

        [Required]
        [StringLength(11, ErrorMessage = "O cep deve conter {1} digitos.")]
        public string? Cpf { get ; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "O nome de matéria deve conter o mínimo de {2} e máximo de {1} digitos.")]
        public string? Materia { get;  set; }


        //public List<Aluno>? Alunos { get; set; }

        public Professor()
        {
        }

        public Professor(int id, string nome, string sobrenome, string cep, string logradouro, string cidade, string celular, string cpf, string materia)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Cep = cep;
            this.logradouro = logradouro;
            Cidade = cidade;
            Celular = celular;
            Cpf = cpf;
            Materia = materia;
            //Alunos = new List<Aluno>();
            _errors = new List<string>();
            Validate();
        }

        public bool Validate() => base.Validate(new ProfessorValidator(), this);

    }
}
