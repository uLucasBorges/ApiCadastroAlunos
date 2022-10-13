using ApiCadastroAlunos.Models;

namespace ApiCadastroAlunos.ViewModel
{
    public class AlunoViewModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public AlunoViewModel()
        {
        }

        public AlunoViewModel(string nome, string sobrenome)
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }
    }


}
