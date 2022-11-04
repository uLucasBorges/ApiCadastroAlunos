using ApiCadastroAlunos.Data;
using ApiCadastroAlunos.ExtensionsMethods;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Models.Validators;
using ApiCadastroAlunos.Repositories.Interfaces;
using ApiCadastroAlunos.ViewModel;
using Dapper;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ResultViewModel> create(AlunoViewModel aluno)
        {
            try
            {
          

                var Aluno = new Aluno(0,
                        aluno.Nome,
                        aluno.Sobrenome,
                        aluno.email,
                        aluno.telefone,
                        aluno.ProfessorId);

                 
               if(Aluno.IsValid)
                {

                    if (aluno != null)

                    await _db.Alunos.AddAsync(Aluno);
                    await _db.SaveChangesAsync();

                    return AlunoValidate.Create(aluno);
                }

                return new ResultViewModel()
                {
                    Data = Aluno.Erros,
                    Message = "Erros encontrados",
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
                if (userExists.Data != null)
                {
                    _db.Alunos.Remove(userExists.Data);
                    _db.SaveChanges();         
                }

                return AlunoValidate.Delete(userExists);

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
                    string query = @"SELECT a.Nome  , a.Sobrenome,  p.Id as professorId , a.Email , a.Celular as telefone
                                     FROM
                                     professor p
                                     INNER JOIN Alunos a
                                     ON a.professorid = p.Id
                                     GROUP BY
                                     a.Nome  , a.Sobrenome,  p.Id , a.Email , a.Celular";

                   List<AlunoViewModel> alunos = (await conn.QueryAsync<AlunoViewModel>(sql: query)).ToList();

                   return AlunoValidate.List(alunos);

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
                    
                    var aluno = (await conn.QueryFirstOrDefaultAsync<Aluno>(sql: query, new { Nome = Nome, Sobrenome = Sobrenome }));

                   return AlunoValidate.Select(aluno);

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

                    string query = @"SELECT a.id , a.Nome  , a.Sobrenome, a.Email , a.Celular , p.Id as professorId ,p.NomeProfessor as nomeProfessor
                                     FROM
                                     professores p
                                     INNER JOIN Alunos a
                                     ON a.professorid = p.Id and a.id = @id
                                     GROUP BY
                                     a.Id , a.Nome,a.Sobrenome ,a.Email , a.Celular, p.Id ,p.NomeProfessor";
                    var aluno = (await conn.QueryFirstOrDefaultAsync<Aluno>(sql: query, new { Id = id }));
                    
                    return AlunoValidate.Select(aluno);
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

        public async Task<ResultViewModel> Update(Aluno aluno)
        {
            try
            {
                if (aluno.IsValid)
                {
                    var userExists = await this.GetById(aluno.Id);

                    if (userExists != null)
                    {
                        _db.Entry(aluno).State = EntityState.Modified;
                        await _db.SaveChangesAsync();
                    }

                    return AlunoValidate.Update(aluno);
                }

                return new ResultViewModel()
                {
                    Data = aluno.Erros,
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
