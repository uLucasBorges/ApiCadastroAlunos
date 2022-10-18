using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.Repositories.Interfaces
{
    public interface IProfessorRepository
    {
        Task<ResultViewModel> Get();
        Task<ResultViewModel> GetAlunosPorProfessor(int id);
        Task<ResultViewModel> GetById(int id);


    }
}
