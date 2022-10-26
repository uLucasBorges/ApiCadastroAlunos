using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

                if(alunoExists.Message == "Erro ao capturar Professores.")
                return StatusCode(StatusCodes.Status500InternalServerError, alunoExists);

                return StatusCode(StatusCodes.Status404NotFound, alunoExists);
              
        }


        [HttpGet("/api/alunos/by/professor/{id:int:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {

            var alunoExists = await _professor.GetAlunosPorProfessor(id);
            if (alunoExists.Success)
            return StatusCode(200, alunoExists);

            if (alunoExists.Message == "Erro ao capturar alunos por Professor.")
                return StatusCode(StatusCodes.Status500InternalServerError, alunoExists);


            return StatusCode(StatusCodes.Status404NotFound, alunoExists);
        }



        [HttpGet("/api/professor/{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var alunoExists = await _professor.GetById(id);
            if (alunoExists.Success)
            return StatusCode(200, alunoExists);

            if (alunoExists.Message == "Erro ao capturar Professor.")
            return StatusCode(StatusCodes.Status500InternalServerError, alunoExists);


            return StatusCode(StatusCodes.Status404NotFound, alunoExists);

        }


        //retorna todos alunos por professor , ou seja , o professor e sua lista de alunos.
        [HttpGet("/api/professor/teste")]
        public async Task<IActionResult> Teste()
        {

            var result = await _professor.Testando();
            return StatusCode(StatusCodes.Status200OK, result);

        }


        [HttpPost("/api/professor/create")]
        public async Task<IActionResult> create([FromBody]Professor professor)
        {
            //if (!ModelState.IsValid)
            //{
            //    var erros = ModelState.SelectMany(x => x.Value.Errors);
            //    return BadRequest(erros);
            //}


            var created = await _professor.Create(professor);

            if (created.Success)
            return StatusCode(StatusCodes.Status201Created, created);

            if (created.Message == "Problemas ao criar professor.")
            return StatusCode(StatusCodes.Status500InternalServerError, created);


            return StatusCode(StatusCodes.Status409Conflict, created);

        }

    }

}

