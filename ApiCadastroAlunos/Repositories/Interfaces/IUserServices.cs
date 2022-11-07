using ApiCadastroAlunos.DTOs;
using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.Repositories.Interfaces
{
    public interface IUserServices
    {
        Task<ResultViewModel> Register(UserDTO model);
        Task<ResultViewModel> Login(UserDTO model);
    }
}
