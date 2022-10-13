using ApiCadastroAlunos.Models;

namespace ApiCadastroAlunos.ViewModel
{
    public class AlunoViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int ProfessorId { get; set; }

        public AlunoViewModel(string nome, string sobrenome, int professorId)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            ProfessorId = professorId;
        }
    }


}
