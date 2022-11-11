using ApiCadastroAlunos.ViewModel;
using CadastroAlunos.Core.DTOs;
using CadastroAlunos.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCadastroAlunos.Repositories
{
    public class UserService : IUserServices
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _role;

        public UserService(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IConfiguration configuration, RoleManager<IdentityRole> role)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _role = role;
        } 

        public async Task<ResultViewModel> Register(UserDTO model)
        {
            var userExists = _userManager.Users.Where(x => x.UserName == model.Email).FirstOrDefault();
            if(userExists != null)
            {
                return new ResultViewModel()
                {
                    Data = userExists,
                    Message = "Usuário já existente",
                    Success = false
                };           
            }

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


            await ValidationRole();
            await _userManager.AddToRoleAsync(user, "Member");

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
        
        private async Task ValidationRole()
        {
            var roleNames = new List<string>() { "Admin", "Member" };

            foreach (string name in roleNames)
            {
                var roleExist = await _role.RoleExistsAsync(name);
                if (!roleExist)
                {
                     await _role.CreateAsync(new IdentityRole(name));
                }
            }

        }

    }
}
