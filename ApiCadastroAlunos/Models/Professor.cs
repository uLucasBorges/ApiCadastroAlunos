using System.ComponentModel.DataAnnotations;

namespace ApiCadastroAlunos.Models
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cep { get; set; }
        public string logradouro { get; set; }
        public string Cidade { get; set; }
        public string Celular { get; set; }
        public string Cpf { get; set; }
        public string Materia { get; set; }

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
        }
    }
}
