using ApiCadastroAlunos.Data;
using ApiCadastroAlunos.Exceptions;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Dapper;

namespace ApiCadastroAlunos.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {

        //metodos de inserir com o EF , metodos de get com o dapper;

        private readonly AppDbContext _db;
        private readonly AppDb bdb;

        public AlunoRepository(AppDbContext db, AppDb bdb)
        {
            _db = db;
            this.bdb = bdb;
        }

        public async Task<Aluno> create(AlunoViewModel aluno)
        {

            var Aluno = new Aluno(0, aluno.Nome , aluno.Sobrenome);
            var userExists = await this.GetBy(aluno.Nome, aluno.Sobrenome);
            if (userExists != null)
            {
                return null;
            }



            await _db.Alunos.AddAsync(Aluno);
            await _db.SaveChangesAsync();
            return Aluno;

        }

        public async Task<Aluno> Delete(int id)
        {

            var userExists = await this.GetById(id);
            if (userExists != null)
            {
                _db.Alunos.Remove(userExists);
                _db.SaveChanges();
            }
            else
            {
                return null;
            }



            return userExists;
        }

        public async Task<List<Aluno>> Get()
        {
            try
            {
                using (var conn = bdb.Connection)
                {
                    string query = "SELECT * FROM Alunos";
                    List<Aluno> alunos = (await conn.QueryAsync<Aluno>(sql: query)).ToList();
                    if (alunos.Count == 0)
                        return null;

                    return alunos;

                }

            }
            catch (SystemException ex)
            {
                throw new DomainException("Erro interno");
            }
        }

        public async Task<Aluno> GetBy(string Nome, string Sobrenome)
        {

            try
            {
                using (var conn = bdb.Connection)
                {
                    string query = "SELECT * FROM Alunos WHERE Nome = @Nome and sobrenome = @Sobrenome";
                    var aluno = (await conn.QueryFirstOrDefaultAsync<Aluno>(sql: query, new { Nome = Nome, Sobrenome = Sobrenome }));
                    if (aluno != null)
                        
                    return aluno;

                }
                return null;
            }
            catch (SystemException ex)
            {
                throw new Exception("Erro interno");
            }
        }

        public async Task<Aluno> GetById(int id)
        {

            try
            {
                using (var conn = bdb.Connection)
                {

                    string query = "SELECT * FROM Alunos WHERE id = @id";
                    var aluno = (await conn.QueryFirstOrDefaultAsync<Aluno>(sql: query, new { Id = id }));
                    if (aluno != null)
                        return aluno;

                        return null;
                }
            }
            catch (SystemException ex)
            {
                throw new Exception("Erro interno");
            }
         
        }

        public async Task<Aluno> Update(Aluno aluno)
        {
            try
            {
                var userExists = await this.GetById(aluno.Id);

                if (userExists != null)
                    _db.Alunos.Update(aluno);
                    _db.SaveChanges();
                    return aluno;
                
                    return null;
               
            }
            catch (Exception ex)
            {
                throw new Exception("Erro Interno");
            }

        }
    }
}
