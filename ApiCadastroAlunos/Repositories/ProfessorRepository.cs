using ApiCadastroAlunos.Data;
using ApiCadastroAlunos.Exceptions;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Dapper;

namespace ApiCadastroAlunos.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDb bdb;

        public ProfessorRepository(AppDb bdb)
        {
            this.bdb = bdb;
        }


        public async Task<ResultViewModel> Get()
        {
            try
            {
                using (var conn = bdb.Connection)
                {
                    string query = @"SELECT p.id AS Id, p.nome AS NomeProfessor , p.materia AS Materia,
                                     COUNT(*) alunos 
                                     FROM
                                     professores p
                                     INNER JOIN alunos a
                                     ON a.professorid = p.id
                                     GROUP BY 
                                     p.id , p.nome, p.materia";

                    List<Professor> professores = (await conn.QueryAsync<Professor>(sql: query)).ToList();
                    if (professores != null)
                        return new ResultViewModel
                        {
                            Data = professores,
                            Message = "Professores encontrados com sucesso.",
                            Success = true
                        };


                    return new ResultViewModel
                    {
                        Message = "não existem Professores.",
                        Success = false
                    };
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
                    string query = @"SELECT p.id AS Id, p.nome AS NomeProfessor , p.materia AS Materia,
                                     COUNT(*) alunos 
                                     FROM
                                     professores p
                                     INNER JOIN alunos a
                                     ON p.id = @id
                                     GROUP BY 
                                     p.id , p.nome, p.materia";

                    Professor professor = (await conn.QueryAsync<Professor>(sql: query , new {Id = id})).FirstOrDefault();
                    if (professor != null)
                        return new ResultViewModel
                        {
                            Data = professor,
                            Message = "Professor encontrado com sucesso.",
                            Success = true
                        };


                    return new ResultViewModel
                    {
                        Message = "não existe Professor com o código unico informado informado.",
                        Success = false
                    };
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
                    string query = @"SELECT a.id , a.nome  , a.sobrenome, p.id as professorId ,p.nome as professor 
                                     FROM
                                     professores p
                                     INNER JOIN alunos a
                                     ON a.professorid = p.id
                                     GROUP BY 
                                     a.id , a.nome,a.sobrenome , p.id ,p.nome
";
                    List<Aluno> alunos = (await conn.QueryAsync<Aluno>(sql: query, new { Id = id })).ToList();
                    if (alunos != null)
                        return new ResultViewModel
                        {
                            Data = alunos,
                            Message = "",
                            Success = true
                        };


                    return new ResultViewModel
                    {
                        Message = "não existem alunos do Professor informado.",
                        Success = false
                    };
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
    }
}
