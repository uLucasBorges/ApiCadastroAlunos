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
            if (alunoExists != null)
            {

                return StatusCode(200, new ResultViewModel
                {
                    Message = "Alunos encontrados com sucesso!",
                    Success = true,
                    Data = alunoExists
                });
            }

            return BadRequest(new ResultViewModel
            {
                Message = "Alunos não encontrados!",
                Success = false

            });

        }

        [HttpGet("/api/alunos/search/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var alunoExists = await _aluno.GetById(id);
            if (alunoExists != null)
            {

                return Ok(new ResultViewModel
                {
                    Message = "Aluno encontrado com sucesso!",
                    Success = true,
                    Data = alunoExists
                });
            }

            return BadRequest(new ResultViewModel
            {
                Message = "Aluno não encontrado!",
                Success = false

            });
        }

        [HttpGet("/api/alunos/search/name")]
        public async Task<IActionResult> GetByName(string nome, string sobrenome)
        {
            var alunoExists = await _aluno.GetBy(nome, sobrenome);
            if (alunoExists != null)
            {

                return Ok(new ResultViewModel
                {
                    Message = "Aluno encontrado com sucesso!",
                    Success = true,
                    Data = alunoExists
                });
            }

            return BadRequest(new ResultViewModel
            {
                Message = "Aluno não encontrado!",
                Success = false

            });
        }

        [HttpPost]

        [Route("/api/alunos/create")]
        public async Task<IActionResult> New([FromBody] AlunoViewModel aluno)
        {
            var alunoCreated = await _aluno.create(aluno);
            if (alunoCreated.Success)
            {
                return Ok(alunoCreated);
            }


            return StatusCode(500, alunoCreated);



        }


        [HttpPut("/api/set/aluno/{id}")]
        public async Task<IActionResult> Set([FromBody] Aluno aluno)
        {
            var alunoExists = await _aluno.Update(aluno);

            if (alunoExists != null)
                return Ok(new ResultViewModel
                {
                    Message = "Aluno Atualizado com sucesso!",
                    Success = true,
                    Data = aluno
                });



            return BadRequest(new ResultViewModel
            {
                Message = "Aluno não existente.",
                Success = false
            });
        }

        [HttpDelete("api/delete/aluno/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            {
                var userExists = await _aluno.Delete(id);
                if (userExists != null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Aluno deletado com sucesso!",
                        Success = true,

                    });
            }


            return BadRequest(new ResultViewModel
            {
                Message = "Aluno não existente.",
                Success = false
            });

        }



    }
}
