
using ApiCadastroAlunos.Models;
using CadastroAlunos.Core.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiCadastroAlunos.Core.Interfaces;


namespace ApiCadastroAlunos.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Authorize("Admin")]
    public class AlunoController : Controller
    {
        private readonly IAlunoRepository _aluno;

        public AlunoController(IAlunoRepository aluno)
        {
            _aluno = aluno;
        }

        /// <summary>
        /// list of student
        /// </summary>
        /// <returns>Lista de Alunos</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("/api/alunos/list")]
        public async Task<IActionResult> GetAll()
        {
            //_logger.LogTrace($"Iniciando busca de informações de alunos");


            var alunoExists = await _aluno.Get();

                if (alunoExists.Success)
                {
                    return Ok(alunoExists);
                }

                if (alunoExists.Message == "Problemas ao capturar aluno.")
                    return StatusCode(StatusCodes.Status500InternalServerError, alunoExists);

                return StatusCode(StatusCodes.Status404NotFound, alunoExists);
        }



        /// <summary>
        /// student by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Aluno</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize("Admin")]
        [HttpGet("/api/alunos/search/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var alunoExists = await _aluno.GetById(id);

            if (alunoExists.Success)
            {
                return Created("Alunos encontrado com Sucesso !!", alunoExists);
            }

            if (alunoExists.Message == "Problemas ao capturar aluno.")
            return StatusCode(StatusCodes.Status500InternalServerError, alunoExists);

            return StatusCode(StatusCodes.Status404NotFound, alunoExists);
        }


        /// <summary>
        /// search student by name
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="sobrenome"></param>
        /// <returns>Aluno</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("/api/alunos/search/name")]
        public async Task<IActionResult> GetByName(string nome, string sobrenome)
        {
            var alunoExists = await _aluno.GetBy(nome, sobrenome);

            if (alunoExists.Success)
            {
                return Ok(alunoExists);
            }

            if (alunoExists.Message == "Problemas ao capturar aluno.")
           return StatusCode(StatusCodes.Status500InternalServerError, alunoExists);

           return StatusCode(StatusCodes.Status404NotFound, alunoExists);

        }


        /// <summary>
        /// create new student
        /// </summary>
        /// <param name="aluno"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Route("/api/alunos/create")]
        public async Task<IActionResult> New([FromBody] AlunoViewModel aluno)
        {

            var alunoCreated = await _aluno.create(aluno);
            if (alunoCreated.Success)
            {
                return Ok(alunoCreated);
            }

            if (alunoCreated.Message == "Problemas ao criar aluno." || alunoCreated.Message == "Erros encontrados!")
            return StatusCode(StatusCodes.Status500InternalServerError, alunoCreated);

            return StatusCode(StatusCodes.Status404NotFound, alunoCreated);

        }



        /// <summary>
        /// update student existing
        /// </summary>
        /// <param name="aluno"></param>
        /// <returns>Aluno</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("/api/set/aluno/{id}")]
        public async Task<IActionResult> Set([FromBody] AlunoViewModel aluno)
        {
            
            var alunoExists = await _aluno.Update(aluno);

                if (alunoExists.Success)
                    return Ok(alunoExists);

                if (alunoExists.Message == "Problemas ao atualizar aluno." || alunoExists.Message == "Erros encontrados!")
                return StatusCode(StatusCodes.Status500InternalServerError, alunoExists);

                return StatusCode(StatusCodes.Status404NotFound, alunoExists);

        }


        /// <summary>
        /// drop student existing
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Aluno</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("api/delete/aluno/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            
            var userExists = await _aluno.Delete(id);
          
            if (userExists.Success)
            return Ok(userExists);

            if (userExists.Message == "Problemas ao deletar aluno.")
            return StatusCode(StatusCodes.Status500InternalServerError, userExists);


            return StatusCode(404, userExists);

        }




    }
}
