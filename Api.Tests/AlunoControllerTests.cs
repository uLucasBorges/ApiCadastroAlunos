using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiCadastroAlunos.Controllers;
using ApiCadastroAlunos.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Tests
{
    public class AlunoControllerTests
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IAlunoRepository> mockAlunoRepository;

        public AlunoControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Loose);
            this.mockAlunoRepository = this.mockRepository.Create<IAlunoRepository>();
        }

        private AlunoController CriarAlunoController()
        {
            return new AlunoController(this.mockAlunoRepository.Object);
        }



        [Fact(DisplayName = "Deve retornar 200 na listagem de alunos")]
        public async Task Deve_Retornar_200_Na_Listagem_Alunos()
        {
            // Arrange
            var alunooController = this.CriarAlunoController();

            // Act
            var result = (ObjectResult)await alunooController.GetAll();

            // Assert
            Assert.Equal(200, result.StatusCode);
            this.mockRepository.VerifyAll();
        }
    }
}
