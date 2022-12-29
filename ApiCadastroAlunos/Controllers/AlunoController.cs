
using ApiCadastroAlunos.Models;
using CadastroAlunos.Core.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiCadastroAlunos.Core.Interfaces;
using Microsoft.AspNetCore.Cors;
using _2___CadastroAlunos.Domain.Notification;

namespace ApiCadastroAlunos.Controllers
{

    [ApiController]
    [Authorize(Policy = "School")]
    [Produces("application/json")]
    public class AlunoController : BaseController
    {
        private readonly IAlunoRepository _aluno;

        public AlunoController(IAlunoRepository aluno , INotificationContext _context) : base (_context)
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
            try
            {
                var alunoExists = await _aluno.Get();

                if (alunoExists.Success)
                {
                    return Ok(alunoExists.Data);
                }

                return Response();
            }
            catch (Exception ex)
            {

                return StatusCode(500 , ex.Message);
            }

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
        [HttpGet("/api/alunos/search/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var alunoExists = await _aluno.GetById(id);

                if (alunoExists.Success)
                {
                    return Created("Aluno encontrado com Sucesso !!", alunoExists.Data);
                }

                return Response();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        
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
            try
            {
                var alunoCreated = await _aluno.create(aluno);
                if (alunoCreated.Success)
                {
                    return Ok(alunoCreated);
                }

                return Response();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
         


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
                return Response(alunoExists);

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
            try
            {
                var userExists = await _aluno.Delete(id);

                if (userExists.Success)
                return Ok(userExists.Data);

                return Response();
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }



        }
    }
}
