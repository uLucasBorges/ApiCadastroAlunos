using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.Repositories.Interfaces
{
    public interface IAlunoRepository
    {
        public Task<ResultViewModel> create(AlunoViewModel aluno);
        public Task<List<Aluno>> Get();
        public Task<Aluno> GetBy(string nome , string sobrenome);
        public Task<Aluno> GetById(int id);
        public Task<Aluno> Update(Aluno alunoId);
        public Task<Aluno> Delete(int id);

    }
}
