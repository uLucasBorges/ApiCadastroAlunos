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

        [HttpGet("/api/professores/search/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var alunoExists = await _professor.GetAlunoByProf(id);
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

        //[HttpGet("/api/professores/search/name")]
        //public async Task<IActionResult> GetByName(string nome, string materia)
        //{
        //    var professorExists = await _professor.GetBy(nome, materia);
        //    if (professorExists != null)
        //    {

        //        return Ok(new ResultViewModel
        //        {
        //            Message = "Professor encontrado com sucesso!",
        //            Success = true,
        //            Data = professorExists
        //        });
        //    }

        //    return BadRequest(new ResultViewModel
        //    {
        //        Message = "Professor não encontrado!",
        //        Success = false

        //    });
        //}

        //[HttpPost]

        //[Route("/api/professores/create")]
        //public async Task<IActionResult> New([FromBody] ProfessorViewModel professor)
        //{
        //    var professorCreated = await _professor.create(professor);
        //    if (professorCreated.Success)
        //    {
        //        return Ok(professorCreated);
        //    }


        //    return StatusCode(500, professorCreated);



        //}


        //[HttpPut("/api/set/professor/{id}")]
        //public async Task<IActionResult> Set([FromBody] Professor professor)
        //{
        //    var professorExists = await _professor.Update(professor);

        //    if (professorExists != null)
        //        return Ok(new ResultViewModel
        //        {
        //            Message = "Aluno Atualizado com sucesso!",
        //            Success = true,
        //            Data = professor
        //        });



        //    return BadRequest(new ResultViewModel
        //    {
        //        Message = "Professor não existente.",
        //        Success = false
        //    });
        //}

        //[HttpDelete("api/delete/professor/{id}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    {
        //        var userExists = await _professor.Delete(id);
        //        if (userExists != null)
        //            return Ok(new ResultViewModel
        //            {
        //                Message = "Professor deletado com sucesso!",
        //                Success = true,

        //            });
        //    }


        //    return BadRequest(new ResultViewModel
        //    {
        //        Message = "professor não existente.",
        //        Success = false
        //    });

        //}
    }
}
