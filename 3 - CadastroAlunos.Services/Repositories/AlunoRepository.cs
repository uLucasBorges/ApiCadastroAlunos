using ApiCadastroAlunos.Models;
using CadastroAlunos.Core.Interfaces;
using ApiCadastroAlunos.ViewModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CadastroAlunos.Infra.Data;
using System.Web.Http;
using ApiCadastroAlunos.ExtensionsMethods;
using Dapper;
using ApiCadastroAlunos.Core.Models;
using ApiCadastroAlunos.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace ApiCadastroAlunos.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly ILogger<AlunoRepository> _logger;


        //metodos de inserir com o EF , metodos de get com o dapper;

        private readonly AppDbContext _db;
        private readonly AppDb bdb;
        private readonly IMapper _mapper;

        public AlunoRepository(AppDbContext db, AppDb bd , IMapper mapper, ILogger<AlunoRepository> logger)
        {
            _db = db;
            bdb = bd;
            _mapper = mapper;
            _logger = logger;
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
                                     professo p
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


                _logger.Log(LogLevel.Error, ex, "erro ao capturar alunos.");

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
                _logger.Log(LogLevel.Error, ex, "erro ao capturar aluno.");

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
                _logger.Log(LogLevel.Error, ex, "erro ao capturar aluno.");

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
                _logger.Log(LogLevel.Error, ex, "erro ao atualizar aluno.");

                return new ResultViewModel
                {
                    Message = "Problemas ao atualizar aluno.",
                    Success = false
                };
            }



        }
    }
}
