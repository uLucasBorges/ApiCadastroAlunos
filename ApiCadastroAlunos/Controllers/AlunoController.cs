using System.Collections.Generic;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiCadastroAlunos.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IAlunoRepository _aluno;

        public AlunoController(IAlunoRepository aluno)
        {
            _aluno = aluno;
        }

        [HttpGet("/api/alunos/list")]
        public async Task<IActionResult> GetAll()
        {
            var alunoExists = await _aluno.Get();

            if (alunoExists.Success)
            {
                return Ok(alunoExists);
            }

            return StatusCode(StatusCodes.Status404NotFound, alunoExists);

        }



        [HttpGet("/api/alunos/search/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var alunoExists = await _aluno.GetById(id);

            if (alunoExists.Success)
            {
                return Created("Alunos encontrado com Sucesso !!", alunoExists);
            }

            return StatusCode(StatusCodes.Status404NotFound, alunoExists);
        }

        [HttpGet("/api/alunos/search/name")]
        public async Task<IActionResult> GetByName(string nome, string sobrenome)
        {
            var alunoExists = await _aluno.GetBy(nome, sobrenome);

            if (alunoExists.Success)
            {
                return Ok(alunoExists);
            }

            return StatusCode(StatusCodes.Status404NotFound, alunoExists);

        }

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


            return StatusCode(StatusCodes.Status404NotFound, alunoCreated);

        }


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
            {
                return Ok(alunoExists);
            }

            return StatusCode(StatusCodes.Status404NotFound, alunoExists);
          
        }

        [HttpDelete("api/delete/aluno/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            
            var userExists = await _aluno.Delete(id);
          
            if (userExists.Success)
            {
                return Ok(userExists);
            }

            return StatusCode(404, userExists);

        }

    }
}
