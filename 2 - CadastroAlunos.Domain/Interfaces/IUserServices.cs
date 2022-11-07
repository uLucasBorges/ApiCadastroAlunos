using ApiCadastroAlunos.ViewModel;
using CadastroAlunos.Core.DTOs;

namespace CadastroAlunos.Core.Interfaces
{
    public interface IUserServices
    {
        Task<ResultViewModel> Register(UserDTO model);
        Task<ResultViewModel> Login(UserDTO model);
    }
}
