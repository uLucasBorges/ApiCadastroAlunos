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

            if (alunoExists.Success)
                return StatusCode(200, alunoExists);

            return StatusCode(404, alunoExists); 
      
        }

        [HttpGet("/api/alunos/by/professor/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var alunoExists = await _professor.GetAlunosPorProfessor(id);
            if (alunoExists.Success)
            return StatusCode(200, alunoExists);

            return StatusCode(404, alunoExists);
        }



        [HttpGet("/api/professor/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var alunoExists = await _professor.GetById(id);
            if (alunoExists.Success)
            return StatusCode(200, alunoExists);

            return StatusCode(404, alunoExists);

        }

        [HttpPost("/api/professor/create")]
        public async Task<IActionResult> create([FromBody]Professor professor)
        {
            var created = await _professor.Create(professor);
            if (created.Success)
            return StatusCode(200, created);

            return StatusCode(404, created);

        }

    }

}

