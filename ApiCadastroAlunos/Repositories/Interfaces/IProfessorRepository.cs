using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.Repositories.Interfaces
{
    public interface IProfessorRepository
    {
        public Task<string> GetById(int id);
        public Task<List<Professor>> Get();
        Task<List<Aluno>> GetAlunoByProf(int id);
        public int GetContagem();

    }
}
