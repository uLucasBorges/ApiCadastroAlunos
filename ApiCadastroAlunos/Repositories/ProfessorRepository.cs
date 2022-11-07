using ApiCadastroAlunos.Data;
using ApiCadastroAlunos.ExtensionsMethods;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroAlunos.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDb bdb;
        private readonly AppDbContext _db;

        public ProfessorRepository(AppDb bdb, AppDbContext db)
        {
            this.bdb = bdb;
            _db = db;
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
                using (var conn = bdb.Connection)
                {
                    string query = @"SELECT p.Id AS Id, p.Nome AS nome , p.Sobrenome , p.Cep , p.Logradouro , p.Cidade, p.Celular , p.Cpf , p.Materia AS Materia,
                                     (SELECT COUNT(1) FROM alunos a  WHERE a.professorid = p.id) alunos
                                     FROM
                                     professor p";

                    List<ProfessorViewModel> professores = (await conn.QueryAsync<ProfessorViewModel>(sql: query)).ToList();

                    return ProfessorValidate<ProfessorViewModel>.List(professores);
                }

            }
            catch (SystemException ex)
            {
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
                using (var conn = bdb.Connection)
                {

                    string query = @"SELECT p.Id AS Id, p.Nome AS nome , p.Sobrenome , p.Cep , p.Logradouro , p.Cidade, p.Celular , p.Cpf , p.materia AS Materia,
                                     (SELECT COUNT(1) FROM alunos a  WHERE a.professorid = p.id) alunos
                                     FROM
                                     professor p, alunos a
                                     WHERE p.id = @id
                                     GROUP BY
                                     p.Id, p.Nome, p.Sobrenome , p.Cep , p.Logradouro , p.Cidade, p.Celular , p.Cpf , p.Materia";

                    ProfessorViewModel professor = (await conn.QueryAsync<ProfessorViewModel>(sql: query , new {Id = id})).FirstOrDefault();
                    
                    return ProfessorValidate<ProfessorViewModel>.Select(professor);
                }

            }
            catch (SystemException ex)
            {
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
                using (var conn = bdb.Connection)
                {
                    string query = @"SELECT a.Id , a.Nome, a.Sobrenome , a.professorid as ProfessorId, a.email , a.Celular
                                     FROM
                                     professor p, alunos a
                                     WHERE p.id = a.professorid and p.id = @Id
                                     GROUP BY
                                     a.Id , a.Nome , a.Sobrenome , a.professorid , a.Email , a.Celular , a.professorid";
                    List<AlunoViewModel> alunos = (await conn.QueryAsync<AlunoViewModel>(sql: query, new { Id = id })).ToList();

                    return ProfessorValidate<AlunoViewModel>.ListAlunos(alunos);
                }

            }
            catch (SystemException ex)
            {
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

                return ProfessorValidate<Professor>.Create(professor);

            }
            catch (Exception ex)
            {
                return new ResultViewModel
                {
                    Message = "Problemas ao criar professor.",
                    Success = false
                };
            }

        }

    }

}
