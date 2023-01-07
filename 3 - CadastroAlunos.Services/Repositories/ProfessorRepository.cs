using ApiCadastroAlunos.Core.Interfaces;
using ApiCadastroAlunos.Core.Models;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Utilities;
using ApiCadastroAlunos.ViewModel;
using CadastroAlunos.Core.Interfaces;
using CadastroAlunos.Infra.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiCadastroAlunos.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly ILogger<ProfessorRepository> _logger;
        private readonly AppDbContext _db;

        public ProfessorRepository(AppDbContext db, ILogger<ProfessorRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<ResultViewModel> Testando()
        {
            //var result = await _db.Professor.Include(x => x.Alunos).ToListAsync();
            var result = await _db.Professor.ToListAsync();


            if (result != null)
                return new ResultViewModel
                {
                    Data = result,
                    Message = "Professores encontrados com sucesso.",
                    Success = true
                };


            return new ResultViewModel
            {
                Message = "não existem Professores.",
                Success = false
            };
        }

        public async Task<ResultViewModel> Get()
        {
            try
            {
                using (var conn = _db.Connection)
                {
                    string query = @"SELECT p.Id AS Id, p.Nome AS nome , p.Sobrenome , p.Cep , p.Logradouro , p.Cidade, p.Celular , p.Cpf , p.Materia AS Materia,
                                     (SELECT COUNT(1) FROM alunos a  WHERE a.professorid = p.id) alunos
                                     FROM
                                     professor p";

                    List<ProfessorViewModel> professores = (await conn.QueryAsync<ProfessorViewModel>(sql: query)).ToList();

                    return Responses<ProfessorViewModel>.List(professores);
                }

            }
            catch (SystemException ex)
            {

                _logger.Log(LogLevel.Error, ex, "erro ao capturar professores.");

                return new ResultViewModel
                {
                    Message = "Erro ao capturar Professores."
                };
            }
        }

        public async Task<ResultViewModel> GetById(int id)
        {
            try
            {
                using (var conn = _db.Connection)
                {

                    string query = @"SELECT p.Id AS Id, p.Nome AS nome , p.Sobrenome , p.Cep , p.Logradouro , p.Cidade, p.Celular , p.Cpf , p.materia AS Materia ,
                                     (SELECT COUNT(1) FROM alunos a  WHERE a.professorid = p.id) alunos
                                     FROM
                                     professor p, alunos a
                                     WHERE p.id = @id";

                    ProfessorViewModel professor = (await conn.QueryAsync<ProfessorViewModel>(sql: query , new {Id = id})).FirstOrDefault();
                    
                    return Responses<ProfessorViewModel>.Select(professor);
                }

            }
            catch (SystemException ex)
            {
                _logger.Log(LogLevel.Error, ex, "erro ao capturar professor.");

                return new ResultViewModel
                {
                    Message = "Erro ao capturar Professor."
                };
            }
        }

        public async Task<ResultViewModel> GetAlunosPorProfessor(int id)
        {
            try
            {
                using (var conn = _db.Connection)
                {
                    string query = @"SELECT a.Id , a.Nome, a.Sobrenome , a.professorid as ProfessorId, a.email , a.Celular
                                     FROM
                                     professor p, alunos a
                                     WHERE p.id = a.professorid and p.id = @Id";
                    List<AlunoViewModel> alunos = (await conn.QueryAsync<AlunoViewModel>(sql: query, new { Id = id })).ToList();

                    return Responses<AlunoViewModel>.List(alunos);
                }

            }
            catch (SystemException ex)
            {
                _logger.Log(LogLevel.Error, ex, "erro ao capturar alunos de determinado professor.");

                return new ResultViewModel
                {

                    Message = "Erro ao capturar alunos por Professor."
                };
            }
        }

        public async Task<ResultViewModel> Create(Professor professor)
        {
            try
            {

                if (professor != null)

                await _db.Professor.AddAsync(professor);
                await _db.SaveChangesAsync();

                return Responses<Professor>.Create(professor);

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "erro ao criar professor.");

                return new ResultViewModel
                {
                    Message = "Problemas ao criar professor.",
                    Success = false
                };
            }

        }

    }

}
