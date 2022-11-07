using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiCadastroAlunos.DTOs;
using ApiCadastroAlunos.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ApiCadastroAlunos.Controllers
{
    [Produces("application/json")]
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
        public async Task<ActionResult> RegisterUser([FromBody] UserDTO model)
        {
      
            var result = await _service.Register(model);

            if (!result.Success)
            {
                return BadRequest(result.Data);
            }

            var autorizacao = GerarToken(model);
            autorizacao.Token = "autentique-se.";
            autorizacao.Authenticated = false;
            autorizacao.Expiration = DateTime.Now.AddHours(0);
            autorizacao.Message = "você foi cadastrado com sucesso.";

            return Ok(autorizacao);
        }


        /// <summary>
        /// Login in System
        /// </summary>
        /// <param name="userInfo"></param>
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
                return Ok(GerarToken(model));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido....");
                return BadRequest(ModelState);
            }
        }



        private UsuarioToken GerarToken(UserDTO userInfo)
        {
            //define declarações do usuário
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                 new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            //gera uma chave com base em um algoritmo simetrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
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