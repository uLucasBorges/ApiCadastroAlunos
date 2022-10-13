using ApiCadastroAlunos.Data;
using ApiCadastroAlunos.Exceptions;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Repositories.Interfaces;
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

        public async Task<string> GetById(int id)
        {
            try
            {
                using (var conn = bdb.Connection)
                {

                    //string query = "SELECT * FROM Alunos WHERE id = @id";
                    //var aluno = (await conn.QueryFirstOrDefaultAsync<Professor>(sql: query, new { Id = id }));
                    //if (aluno != null)
                    // return aluno;

                    //return null;

                    string query = "select p.nome from professores p inner join alunos a on p.id = a.professorid and a.id = @id";
                    var professor = await conn.QueryAsync<string>(sql: query, new { Id = id });

                    return professor.FirstOrDefault();

                }
            }
            catch (SystemException ex)
            {
                throw new Exception("Erro interno");
            }
        }

        public async Task<List<Aluno>> GetAlunoByProf(int id)
        {
            try
            {
                using (var conn = bdb.Connection)
                {
                    string query = "select a.id , a.nome , a.sobrenome from alunos a inner join professores p on a.professorId = p.id where p.id = @id";
                    var professor = await conn.QueryAsync<Aluno>(sql: query, new { Id = id });

                    return professor.ToList();

                }
            }
            catch (SystemException ex)
            {
                throw new Exception("Erro interno");
            }
        }

        public async Task<List<Professor>> Get()
        {
            try
            {
                using (var conn = bdb.Connection)
                {
                    string query = "SELECT * FROM Professores";
                    List<Professor> alunos = (await conn.QueryAsync<Professor>(sql: query)).ToList();
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

        public int GetContagem(int professorid)
        {
            return 100;
        }

        public int GetContagem()
        {
            throw new NotImplementedException();
        }
    }
}
