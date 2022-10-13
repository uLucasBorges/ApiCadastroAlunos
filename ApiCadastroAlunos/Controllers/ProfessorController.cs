using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiCadastroAlunos.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly IProfessorRepository _professor;

        public ProfessorController(IProfessorRepository aluno)
        {
            _professor = aluno;
        }

        [HttpGet("/api/professores/list")]
        public async Task<IActionResult> GetAll()
        {
            var alunoExists = await _professor.Get();
            if (alunoExists != null)
            {

                return StatusCode(200, new ResultViewModel
                {
                    Message = "Professores encontrados com sucesso!",
                    Success = true,
                    Data = alunoExists
                });
            }

            return BadRequest(new ResultViewModel
            {
                Message = "Professores não encontrados!",
                Success = false

            });

        }

        [HttpGet("/api/alunos/by/professor/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var alunoExists = await _professor.GetAlunosPorProfessor(id);
            if (alunoExists.Count > 0)
            {

                return Ok(new ResultViewModel
                {
                    Message = "professor encontrado com sucesso!",
                    Success = true,
                    Data = alunoExists
                });
            }

            return BadRequest(new ResultViewModel
            {
                Message = "professor não encontrado!",
                Success = false

            });
        }


        [HttpGet("/api/professor/detalhes/{id}")]
        public async Task<IActionResult> GetDetalhesProfessor(int id)
        {
            var alunoExists = await _professor.GetDadosProf(id);
            if (alunoExists != null)
            {

                return Ok(new ResultViewModel
                {
                    Message = "professor encontrado com sucesso!",
                    Success = true,
                    Data = alunoExists
                });
            }

            return BadRequest(new ResultViewModel
            {
                Message = "professor não encontrado!",
                Success = false

            });
        }
    }
}
