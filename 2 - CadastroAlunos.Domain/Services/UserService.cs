using ApiCadastroAlunos.ViewModel;
using CadastroAlunos.Core.DTOs;
using CadastroAlunos.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ApiCadastroAlunos.Repositories
{
    public class UserService : IUserServices
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        } 

        public async Task<ResultViewModel> Register(UserDTO model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new ResultViewModel(){
                   Data = result.Errors,
                   Message = "Erros encontrados.",
                   Success = false
                };
            }

            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, false);
            return new ResultViewModel()
            {
                Data = model,
                Message = "Usuário registrado.",
                Success = true
            };
        }


        public async Task<ResultViewModel> Login(UserDTO userInfo)
        {
             var result = await _signInManager.PasswordSignInAsync(userInfo.Email,
             userInfo.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return new ResultViewModel()
                {
                    Data = result,
                    Message = "Logado com sucesso.",
                    Success = true
                };
            }

           
                return new ResultViewModel()
                {
                    Data = result.IsNotAllowed,
                    Success = false
                };
            

        }
    }
}
