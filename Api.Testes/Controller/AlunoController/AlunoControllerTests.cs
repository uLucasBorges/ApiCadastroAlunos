using System.Collections;
using System.Collections.Generic;
using ApiCadastroAlunos.Controllers;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Testes
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
            return new AlunoController(mockAlunoRepository.Object);
        }



        [Fact(DisplayName = "Deve retornar 200 na listagem de alunos")]
        public async Task Deve_Retornar_200_Na_Listagem_Alunos()
        {
            // Arrange

            var alunooController = this.CriarAlunoController();
            mockAlunoRepository.Setup(x => x.Get()).ReturnsAsync(new ResultViewModel() {Success = true });

            // Act
            var result = (ObjectResult) await alunooController.GetAll();
           
            // Assert
            mockAlunoRepository.Verify(x => x.Get(), Times.Once);

            Assert.Equal(200,result.StatusCode);  
            this.mockRepository.VerifyAll();
        }

        [Fact(DisplayName = "Deve retornar 200 na busca de aluno por id")]
        public async Task Deve_Retornar_200_Na_Busca_AlunoId()
        {
            // Arrange
            var id = 100;
            var alunooController = this.CriarAlunoController();
            mockAlunoRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new ResultViewModel() { Success = true });

            // Act
            var result = (ObjectResult)await alunooController.Get(id);

            // Assert
            mockAlunoRepository.Verify(x => x.GetById(id), Times.Once);

            Assert.Equal(201, result.StatusCode);
            this.mockRepository.VerifyAll();
        }



        [Fact(DisplayName = "Deve retornar 200 na busca de aluno por id")]
        public async Task Deve_Retornar_200_Na_Busca_AlunoNome()
        {
            // Arrange
            var nome = "";  
            var sobrenome = "";
            var alunooController = this.CriarAlunoController();
            mockAlunoRepository.Setup(x => x.GetBy(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new ResultViewModel() { Success = true });

            // Act
            var result = (ObjectResult)await alunooController.GetByName(nome , sobrenome);

            // Assert
            mockAlunoRepository.Verify(x => x.GetBy(nome , sobrenome), Times.Once);

            Assert.Equal(200, result.StatusCode);
            this.mockRepository.VerifyAll();
        }


        [Fact(DisplayName = "Deve retornar 200 na criação de alunos")]
        public async Task Deve_Retornar_200_Criacao_Alunos()
         {
            // Arrange
            var alunooController = this.CriarAlunoController();
            var aluno = new AlunoViewModel(It.IsAny<int>() ,It.IsAny<string>() , It.IsAny<string>() , It.IsAny<string>() , It.IsAny<string>() , It.IsAny<int>());
            mockAlunoRepository.Setup(x => x.create(aluno)).ReturnsAsync(new ResultViewModel() { Data = aluno , Success = true});

            // Act
            var result = (ObjectResult)await alunooController.New(aluno);

            // Assert
            mockAlunoRepository.Verify(x => x.create(aluno), Times.Once);

            Assert.Equal(200, result.StatusCode);
            this.mockRepository.VerifyAll();
        }

        [Fact(DisplayName = "Deve retornar 200 na remoção de alunos")]
        public async Task Deve_Retornar_200_Remover_Aluno()
        {
            // Arrange
            var alunoId = 100;
            var alunooController = this.CriarAlunoController();
            mockAlunoRepository.Setup(x => x.Delete(alunoId)).ReturnsAsync(new ResultViewModel() {Success = true });

            // Act
            var result = (ObjectResult)await alunooController.Delete(alunoId);

            // Assert
            mockAlunoRepository.Verify(x => x.Delete(alunoId), Times.Once);

            Assert.Equal(200, result.StatusCode);
            this.mockRepository.VerifyAll();
        }

        [Fact(DisplayName = "Deve retornar 200 em atualizar aluno")]
        public async Task Deve_Retornar_200_Atualizar_Aluno()
        {
            // Arrange
            var aluno = new AlunoViewModel();
            var alunooController = this.CriarAlunoController();
            mockAlunoRepository.Setup(x => x.Update(aluno)).ReturnsAsync(new ResultViewModel() { Data = aluno, Success = true });

            // Act
            var result = (ObjectResult)await alunooController.Set(aluno);

            // Assert
            mockAlunoRepository.Verify(x => x.Update(aluno), Times.Once);

            Assert.Equal(200, result.StatusCode);
            this.mockRepository.VerifyAll();
        }

    }
}