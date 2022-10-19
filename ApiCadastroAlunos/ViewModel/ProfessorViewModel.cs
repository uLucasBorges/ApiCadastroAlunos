using System.ComponentModel.DataAnnotations;

namespace ApiCadastroAlunos.ViewModel
{
    public class ProfessorViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cep { get; set; }
        public string logradouro { get; set; }
        public string Cidade { get; set; }
        public string Celular { get; set; }
        public string Cpf { get; set; }
        public string Materia { get; set; }
        public int? Alunos { get; set; }

    }
}
