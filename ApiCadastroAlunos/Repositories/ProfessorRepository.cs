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


        public async Task<Professor> GetDadosProf(int id)
        {
            try
            {
                using (var conn = bdb.Connection)
                {
                    string query = @"SELECT COUNT(a.Id) as qntdAlunos, p.nome as NomeProfessor, p.id as Id, p.materia as Materia
                                    FROM Alunos a , Professores p
                                    WHERE a.professorid = 1 
                                    GROUP BY a.id , p.nome , p.id , p.materia";

                    var professor = await conn.QueryAsync<Professor>(sql: query, new { Id = id });

                    return professor.FirstOrDefault();

                }
            }
            catch (SystemException ex)
            {
                throw new Exception("Erro interno");
            }
        }

        public async Task<List<ProfessorViewModel>> Get()
        {
            try
            {
                using (var conn = bdb.Connection)
                {
                    string query = "SELECT * FROM Professores";
                    List<ProfessorViewModel> alunos = (await conn.QueryAsync<ProfessorViewModel>(sql: query)).ToList();
                    if (alunos != null)
                        return alunos;

                    return null;
                }

            }
            catch (SystemException ex)
            {
                throw new DomainException("Erro interno");
            }
        }

        public async Task<List<Aluno>> GetAlunosPorProfessor(int id)
        {
            try
            {
                using (var conn = bdb.Connection)
                {
                    string query = "SELECT * FROM Alunos WHERE professorid = @id ";
                    List<Aluno> alunos = (await conn.QueryAsync<Aluno>(sql: query, new { Id = id })).ToList();
                    if (alunos != null)
                        return alunos;

                    return null;
                }

            }
            catch (SystemException ex)
            {
                throw new DomainException("Erro interno");
            }
        }
    }
}
