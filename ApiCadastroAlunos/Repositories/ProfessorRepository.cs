using ApiCadastroAlunos.Data;
using ApiCadastroAlunos.Exceptions;
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
            var result = await _db.Professores.Include(x => x.Alunos).ToListAsync();
        

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
                    string query = @"SELECT p.id AS Id, p.nome AS nome , p.sobrenome , p.cep , p.logradouro , p.cidade, p.celular , p.cpf , p.materia AS Materia,
                                     (SELECT COUNT(1) FROM alunos a  WHERE a.professorid = p.id) alunos
                                     FROM
                                     professores p";

                    List<ProfessorViewModel> professores = (await conn.QueryAsync<ProfessorViewModel>(sql: query)).ToList();

                    return ProfessorValidate.List(professores);
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

                    string query = @"SELECT p.id AS Id, p.nome AS nome , p.sobrenome , p.cep , p.logradouro , p.cidade, p.celular , p.cpf , p.materia AS Materia,
                                     (SELECT COUNT(1) FROM alunos a  WHERE a.professorid = p.id) alunos
                                     FROM
                                     professores p, alunos a
                                     WHERE p.id = @id
                                     GROUP BY
                                     p.id, p.nome, p.sobrenome , p.cep , p.logradouro , p.cidade, p.celular , p.cpf , p.materia";

                    ProfessorViewModel professor = (await conn.QueryAsync<ProfessorViewModel>(sql: query , new {Id = id})).FirstOrDefault();
                    
                    return ProfessorValidate.Select(professor);
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
                    string query = @"SELECT a.id , a.nome  , a.sobrenome, a.email, a.celular ,p.id as professorId
                                     FROM
                                     professores p
                                     INNER JOIN alunos a
                                     ON a.professorid = p.id and p.id = @id
                                     GROUP BY 
                                     a.id , a.nome,a.sobrenome ,a.email, a.celular , p.id";
                    List<Aluno> alunos = (await conn.QueryAsync<Aluno>(sql: query, new { Id = id })).ToList();

                    return ProfessorValidate.ListAlunos(alunos);
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

                await _db.Professores.AddAsync(professor);
                await _db.SaveChangesAsync();

                return ProfessorValidate.Create(professor);

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
