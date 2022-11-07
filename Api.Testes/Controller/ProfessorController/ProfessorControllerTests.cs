using System.Collections;
using System.Collections.Generic;
using ApiCadastroAlunos.Controllers;
using ApiCadastroAlunos.Core.Interfaces;
using ApiCadastroAlunos.Core.Models;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Testes
{
    public class ProfessorControllerTests
    {
        private readonly MockRepository mockRepository;
        private readonly Mock<IProfessorRepository> mockProfessorRepository;



        public ProfessorControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Loose);
            this.mockProfessorRepository = this.mockRepository.Create<IProfessorRepository>();
        }

        private ProfessorController CriarProfessorController()
        {
            return new ProfessorController(mockProfessorRepository.Object);
        }



        [Fact(DisplayName = "Deve retornar 200 na listagem de professores")]
        public async Task Deve_Retornar_200_Na_Listagem_Professores()
        {
            // Arrange

            var professorController = this.CriarProfessorController();
            mockProfessorRepository.Setup(x => x.Get()).ReturnsAsync(new ResultViewModel() {Success = true });

            // Act
            var result = (ObjectResult) await professorController.GetAll();
           
            // Assert
            mockProfessorRepository.Verify(x => x.Get(), Times.Once);

            Assert.Equal(200,result.StatusCode);  
            this.mockRepository.VerifyAll();
        }

        [Fact(DisplayName = "Deve retornar 200 na busca de professor por id")]
        public async Task Deve_Retornar_200_Na_Busca_AlunoId()
        {
            // Arrange
            var id = 100;
            var professorController = this.CriarProfessorController();
            mockProfessorRepository.Setup(x => x.GetById(id)).ReturnsAsync(new ResultViewModel() { Success = true });

            // Act
            var result = (ObjectResult)await professorController.GetById(id);

            // Assert
            mockProfessorRepository.Verify(x => x.GetById(id), Times.Once);

            Assert.Equal(200, result.StatusCode);
            this.mockRepository.VerifyAll();
        }

        [Fact(DisplayName = "Deve retornar 200 na busca de alunos por professor")]
        public async Task Deve_Retornar_200_Na_Busca_AlunoByProfessor()
        {
            // Arrange
            var professorid = 1;
            var professorController = this.CriarProfessorController();
            mockProfessorRepository.Setup(x => x.GetAlunosPorProfessor(professorid)).ReturnsAsync(new ResultViewModel() { Success = true });

            // Act
            var result = (ObjectResult)await professorController.Get(professorid);

            // Assert
            mockProfessorRepository.Verify(x => x.GetAlunosPorProfessor(professorid), Times.Once);

            Assert.Equal(200, result.StatusCode);
            this.mockRepository.VerifyAll();
        }

        [Fact(DisplayName = "Deve retornar 200 na criação de professores")]
        public async Task Deve_Retornar_200_Na_Criacao_Professor()
        {
            // Arrange
            var professor = new Professor();
            var professorController = this.CriarProfessorController();
            mockProfessorRepository.Setup(x => x.Create(professor)).ReturnsAsync(new ResultViewModel() { Success = true });

            // Act
            var result = (ObjectResult)await professorController.create(professor);

            // Assert
            mockProfessorRepository.Verify(x => x.Create(professor), Times.Once);

            Assert.Equal(201, result.StatusCode);
            this.mockRepository.VerifyAll();
        }



    }
}