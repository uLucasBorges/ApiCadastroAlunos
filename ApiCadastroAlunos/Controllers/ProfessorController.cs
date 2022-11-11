using ApiCadastroAlunos.Core.Interfaces;
using ApiCadastroAlunos.Core.Models;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;
using CadastroAlunos.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiCadastroAlunos.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize("Member")]
    public class ProfessorController : Controller
    {
        private readonly IProfessorRepository _professor;

        public ProfessorController(IProfessorRepository aluno)
        {
            _professor = aluno; 
        }

        /// <summary>
        /// list of teacher´s
        /// </summary>
        /// <returns>teacher</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// list of students by teachers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// teacher by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>teacher</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        ////retorna todos alunos por professor , ou seja , o professor e sua lista de alunos.
        ///// <summary>
        ///// students by teacher Id
        ///// </summary>
        ///// <returns>list students</returns>
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[HttpGet("/api/professor/teste")]
        //public async Task<IActionResult> Teste()
        //{

        //    var result = await _professor.Testando();
        //    return StatusCode(StatusCodes.Status200OK, result);

        //}



        /// <summary>
        /// create new teacher
        /// </summary>
        /// <param name="professor"></param>
        /// <returns>teacher</returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("/api/professor/create")]
        public async Task<IActionResult> create([FromBody]Professor professor)
        {
            //if (!ModelState.IsValid)
            //{
            //    var erros = ModelState.SelectMany(x => x.Value.Errors);
            //    return BadRequest(erros);
            //}
            if (professor.IsValid)
            {

                var created = await _professor.Create(professor);

                if (created.Success)
                    return StatusCode(StatusCodes.Status201Created, created);

                if (created.Message == "Problemas ao criar professor.")
                    return StatusCode(StatusCodes.Status500InternalServerError, created);


                return StatusCode(StatusCodes.Status409Conflict, created);
            }

            return BadRequest(new ResultViewModel()
            {
                Data = professor.Erros,
                Message = "Erros encontrados",
                Success = false

            });

        }

    }

}

