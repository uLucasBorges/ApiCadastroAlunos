using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.Repositories.Interfaces
{
    public interface IProfessorRepository
    {
        Task<List<ProfessorViewModel>> Get();
        Task<List<Aluno>> GetAlunosPorProfessor(int id);
        Task<Professor> GetDadosProf(int id);
  

    }
}
