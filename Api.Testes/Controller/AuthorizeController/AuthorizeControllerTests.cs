using System;
using System.Collections.Generic;
using System.Linq;
using ApiCadastroAlunos.Controllers;
using ApiCadastroAlunos.DTOs;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Api.Testes
{
    public class AuthorizeControllerTests
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IUserServices> _mockService;
        private readonly IConfiguration _configuration;

        public AuthorizeControllerTests()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            this.mockRepository = new MockRepository(MockBehavior.Loose);
            this._mockService = this.mockRepository.Create<IUserServices>();

        }

        private AuthorizeController CriarAuthorizeController()
        {
            return new AuthorizeController(_mockService.Object,_configuration);
        }



        [Fact(DisplayName = "Deve retornar 200 na criação de usuário.")]
        public async Task Deve_Retornar_200_Na_Criacao_Usuario()
        {
            // Arrange
            var user = new UserDTO()
            {
                Email = "hugo.teste@clear.sale",
                Password = "154874Uu@",
                ConfirmPassword = "154874Uu@"
            };

            var authorizeController = this.CriarAuthorizeController();
            _mockService.Setup(x => x.Register(user)).ReturnsAsync(new ResultViewModel { Data = user, Message = "Usuário registrado.", Success = true});

            // Act
         
            var result = (ObjectResult)await authorizeController.RegisterUser(user);

            // Assert
            _mockService.Verify(x => x.Register(user), Times.Once);

            Assert.Equal(200, result.StatusCode);
            this.mockRepository.VerifyAll();
        }


        [Fact(DisplayName = "Deve retornar 200 no login de usuário.")]
        public async Task Deve_Retornar_200_No_Login_Usuario()
        {
            // Arrange
            var user = new UserDTO()
            {
                Email = "hugo.teste@clear.sale",
                Password = "154874Uu@",
                ConfirmPassword = "154874Uu@"
            };

            var authorizeController = this.CriarAuthorizeController();
            _mockService.Setup(x => x.Login(user)).ReturnsAsync(new ResultViewModel { Data = user, Success = true });

            // Act

            var result = (ObjectResult)await authorizeController.Login(user);

            // Assert
            _mockService.Verify(x => x.Login(user), Times.Once);

            Assert.Equal(200, result.StatusCode);
            this.mockRepository.VerifyAll();
        }
    }
}
