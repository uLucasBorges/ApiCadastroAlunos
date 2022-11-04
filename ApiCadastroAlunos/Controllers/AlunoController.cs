using System.Collections.Generic;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiCadastroAlunos.Controllers
{

    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AlunoController : Controller
    {
        private readonly IAlunoRepository _aluno;

        public AlunoController(IAlunoRepository aluno)
        {
            _aluno = aluno;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("/api/alunos/list")]
        public async Task<IActionResult> GetAll()
        {
          
          
                var alunoExists = await _aluno.Get();

                if (alunoExists.Success)
                {
                    return Ok(alunoExists);
                }

                if (alunoExists.Message == "Problemas ao capturar aluno.")
                    return StatusCode(StatusCodes.Status500InternalServerError, alunoExists);

                return StatusCode(StatusCodes.Status404NotFound, alunoExists);
        }




        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Route("/api/alunos/create")]
        public async Task<IActionResult> New([FromBody] AlunoViewModel aluno)
        {
            //if (!ModelState.IsValid)
            //{
            //    var erros = ModelState.SelectMany(x => x.Value.Errors);
            //    return BadRequest(erros);
            //}


            var alunoCreated = await _aluno.create(aluno);
            if (alunoCreated.Success)
            {
                return Ok(alunoCreated);
            }

            if (alunoCreated.Message == "Problemas ao criar aluno.")
            return StatusCode(StatusCodes.Status500InternalServerError, alunoCreated);

            return StatusCode(StatusCodes.Status404NotFound, alunoCreated);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("/api/set/aluno/{id}")]
        public async Task<IActionResult> Set([FromBody] Aluno aluno)
        {
            //if (!ModelState.IsValid)
            //{
            //    var erros = ModelState.SelectMany(x => x.Value.Errors);
            //    return BadRequest(erros);
            //}


            var alunoExists = await _aluno.Update(aluno);

            if (alunoExists.Success)
            return Ok(alunoExists);
            

            if (alunoExists.Message == "Problemas ao atualizar aluno.")
            return StatusCode(StatusCodes.Status500InternalServerError, alunoExists);

            return StatusCode(StatusCodes.Status404NotFound, alunoExists);
          
        }

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
