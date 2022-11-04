using ApiCadastroAlunos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroAlunos.ViewModel
{
    [Keyless]
    public class AlunoViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int ProfessorId { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }

        public AlunoViewModel()
        {
        }

        public AlunoViewModel(string nome, string sobrenome, int professorId, string email, string telefone)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            ProfessorId = professorId;
            this.email = email;
            this.telefone = telefone;
        }
    }


}
