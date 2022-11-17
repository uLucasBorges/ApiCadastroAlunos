using System.Collections;
using ApiCadastroAlunos.ViewModel;
using CadastroAlunos.Core.DTOs;
using Microsoft.AspNetCore.Identity;

namespace CadastroAlunos.Core.Interfaces
{
    public interface IUserServices
    {
        Task<ResultViewModel> Register(UserDTO model);
        Task<ResultViewModel> Login(UserDTO model);
        Task<IList<string>> GetRoles(IdentityUser user);
    }
}
