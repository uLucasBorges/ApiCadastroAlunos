using ApiCadastroAlunos.Core.Models;
using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.Core.Interfaces
{
    public interface IProfessorRepository
    {
        Task<ResultViewModel> Get();
        Task<ResultViewModel> GetAlunosPorProfessor(int id);
        Task<ResultViewModel> GetById(int id);
        Task<ResultViewModel> Create(Professor professor);
        Task<ResultViewModel> Testando();


    }
}
