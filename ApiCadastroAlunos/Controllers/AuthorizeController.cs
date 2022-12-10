using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CadastroAlunos.Core.DTOs;
using CadastroAlunos.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using System.Web;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Authentication;

namespace ApiCadastroAlunos.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthorizeController : Controller
    {

        private readonly IUserServices _service;
        private readonly IConfiguration _configuration;

        public AuthorizeController(IUserServices service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }



        /// <summary>
        /// Register in System
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Usuario</returns>
        [HttpPost("register")]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<ActionResult> RegisterUser([FromBody] UserDTO model)
        {
      
            var result = await _service.Register(model);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(new ResultViewModel
            {
                Data = model.Email,
                Message = "Cadastro realizado.",
                Success = true
                
            });
        }


        /// <summary>
        /// Login in System
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Your Token Jwt</returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO model)
        {
            //verifica se o modelo é válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            var result = await _service.Login(model);

            if (result.Success)
            {
                var JWT = await GerarToken(model);
          
                return Ok(JWT);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido....");
                return BadRequest(ModelState);
            }
        }



        private async Task<UsuarioToken> GerarToken(UserDTO userInfo)
        {
            //define declarações do usuário

            var claims = new List<Claim> {
                 new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Name),
                 new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var user = new IdentityUser
            {
                UserName = userInfo.Name,
                Email = userInfo.Email,
                EmailConfirmed = true
            };

            
            var roles = await _service.GetRoles(user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            //gera uma chave com base em um algoritmo simetrico
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            //gera a assinatura digital do token usando o algoritmo Hmac e a chave privada
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiracão do token.
            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            // classe que representa um token JWT e gera o token
            JwtSecurityToken token = new JwtSecurityToken(
              issuer: _configuration["TokenConfiguration:Issuer"],
              audience: _configuration["TokenConfiguration:Audience"],
              claims: claims,
              expires: expiration,
              signingCredentials: credenciais);


            //retorna os dados com o token e informacoes
            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };


           
        }
    }
}