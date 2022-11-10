using System.Text.Json.Serialization;
using ApiCadastroAlunos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroAlunos.ViewModel
{
    [Keyless]
    public class AlunoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string email { get; set; }
        public string celular { get; set; }
        public int ProfessorId { get; set; }
        public string professorNome { get; set; }


        public AlunoViewModel()
        {
        }

        public AlunoViewModel(int id, string nome, string sobrenome, string email, string celular, int professorId)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            this.email = email;
            this.celular = celular;
            ProfessorId = professorId;
        }
    }


}
