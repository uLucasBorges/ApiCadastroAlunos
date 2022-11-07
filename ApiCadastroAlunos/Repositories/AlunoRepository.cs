using ApiCadastroAlunos.Data;
using ApiCadastroAlunos.ExtensionsMethods;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Models.Validators;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroAlunos.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {

        //metodos de inserir com o EF , metodos de get com o dapper;

        private readonly AppDbContext _db;
        private readonly AppDb bdb;
        private readonly IMapper _mapper;

        public AlunoRepository(AppDbContext db, AppDb bdb , IMapper mapper)
        {
            _db = db;
            this.bdb = bdb;
            _mapper = mapper;
        }

        public async Task<ResultViewModel> create([FromBody]AlunoViewModel aluno)
        {
            try
            {
                var Aluno = _mapper.Map<Aluno>(aluno);

                if (Aluno.IsValid)
                {
                    if (Aluno != null)

                    await _db.Alunos.AddAsync(Aluno);
                    await _db.SaveChangesAsync();

                    return AlunoValidate<Aluno>.Create(Aluno);
                }


                return new ResultViewModel()
                {
                    Data = Aluno.Erros,
                    Message = "Erros encontrados!",
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new ResultViewModel
                {
                    Message = "Problemas ao criar aluno.",
                    Success = false
                };
            }

        }

        public async Task<ResultViewModel> Delete(int id)
        {
            try
            {
                var userExists = await this.GetById(id);
                var alunoTransfer = _mapper.Map<Aluno>(userExists.Data);
                if (alunoTransfer != null)
                {
                   _db.Alunos.Remove(alunoTransfer);
                   await _db.SaveChangesAsync();         
                }

                return AlunoValidate<Aluno>.Delete(alunoTransfer);

            }
            catch (Exception ex)
            {
                return new ResultViewModel
                {
                    Message = "Problemas ao deletar aluno.",
                    Success = false
                };
            }

        }

        public async Task<ResultViewModel> Get()
        {
            try
            {
                using (var conn = bdb.Connection)
                {
                    string query = @"SELECT a.Id, a.Nome  , a.Sobrenome,  p.Id as professorId , a.Email , a.Celular
                                     FROM
                                     professor p
                                     INNER JOIN Alunos a
                                     ON a.professorid = p.Id
                                     GROUP BY
                                     a.Id , a.Nome  , a.Sobrenome,  p.Id , a.Email , a.Celular";

                   List<AlunoViewModel> alunos = (await conn.QueryAsync<AlunoViewModel>(sql: query)).ToList();

                   return AlunoValidate<AlunoViewModel>.List(alunos);

                }

            }
            catch (SystemException ex)
            {
                return new ResultViewModel
                {
                    Message = "Problemas ao capturar aluno.",
                    Success = false
                };
            }
        }

        public async Task<ResultViewModel> GetBy(string Nome, string Sobrenome)
        {

            try
            {
                using (var conn = bdb.Connection)
                {
                    string query = "SELECT * FROM Alunos WHERE Nome = @Nome and sobrenome = @Sobrenome";
                    
                    var aluno = (await conn.QueryFirstOrDefaultAsync<AlunoViewModel>(sql: query, new { Nome = Nome, Sobrenome = Sobrenome }));

                   return AlunoValidate<AlunoViewModel>.Select(aluno);

                }
            }
            catch (SystemException ex)
            {
                return new ResultViewModel
                {
                    Message = "Problemas ao capturar aluno.",
                    Success = false
                };
            }
        }

        public async Task<ResultViewModel> GetById(int id)
        {

            try
            {
                using (var conn = bdb.Connection)
                {

                    string query = @"SELECT a.id , a.Nome  , a.Sobrenome, a.Email as email , a.Celular as celular , p.Id as ProfessorId
                                     FROM
                                     professor p
                                     INNER JOIN Alunos a
                                     ON a.professorid = p.Id and a.id = 1
                                     GROUP BY
                                     a.Id , a.Nome,a.Sobrenome ,a.Email , a.Celular, p.Id";
                    var aluno = (await conn.QueryFirstOrDefaultAsync<AlunoViewModel>(sql: query, new { Id = id }));
                    
                    return AlunoValidate<AlunoViewModel>.Select(aluno);
                }
            }
            catch (SystemException ex)
            {
                return new ResultViewModel
                {
                    Message = "Problemas ao capturar aluno.",
                    Success = false
                };
            }
         
        }

        public async Task<ResultViewModel> Update(AlunoViewModel aluno)
        {
            try
            {
                var alunoTransfer = _mapper.Map<Aluno>(aluno);


                if (alunoTransfer.IsValid){

                    var userExists = await this.GetById(aluno.Id);

                    if (userExists.Data != null)
                    {
                        _db.Entry(alunoTransfer).State = EntityState.Modified;
                        await _db.SaveChangesAsync();
                    }

                    return AlunoValidate<Aluno>.Update(alunoTransfer);

                }
                return new ResultViewModel()
                {
                    Data = alunoTransfer.Erros,
                    Message = "Erros encontrados",
                    Success = false

                };

            }
            catch (Exception ex)
            {
                return new ResultViewModel
                {
                    Message = "Problemas ao atualizar aluno.",
                    Success = false
                };
            }



        }
    }
}
