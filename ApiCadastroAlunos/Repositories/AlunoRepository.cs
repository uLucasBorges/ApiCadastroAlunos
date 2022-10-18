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

        public async Task<ResultViewModel> create(AlunoViewModel aluno)
        {
            try
            {
                var Aluno = new Aluno(0, aluno.Nome, aluno.Sobrenome , 1);
                var userExists = await this.GetBy(aluno.Nome, aluno.Sobrenome);
                if (userExists != null)
                {
                    return new ResultViewModel
                    {
                        Message = "Aluno já existente.",
                        Success = false
                    };
                }



                await _db.Alunos.AddAsync(Aluno);
                await _db.SaveChangesAsync();
                
                return new ResultViewModel
                {
                    Message = "Aluno criado com sucesso!",
                    Success = true,
                    Data = aluno
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

                    return new ResultViewModel()
                    {
                        Message = "Aluno deletado com sucesso!",
                        Success = true,
                        Data = userExists.Data

                    };
                }
                else
                {
                    return new ResultViewModel()
                    {
                        Message = "Aluno não existente.",
                        Success = false,
                        Data = userExists.Data
                    };
                }

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
                    string query = @"SELECT a.id , a.nome  , a.sobrenome, p.id as professorId ,p.nome as professor 
                                     FROM
                                     professores p
                                     INNER JOIN alunos a
                                     ON a.professorid = p.id
                                     GROUP BY
                                     a.id , a.nome,a.sobrenome , p.id ,p.nome";

                    List<Aluno> alunos = (await conn.QueryAsync<Aluno>(sql: query)).ToList();
                    if (alunos.Count == 0)
                    {
                        return new ResultViewModel
                        {
                            Message = "não existem alunos.",
                            Success = false
                        };
                    }

                    return new ResultViewModel
                    {
                        Data = alunos,
                        Message = "Alunos encontrados.",
                        Success = true
                    };

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
                    if (aluno != null)
                     {

                        return new ResultViewModel
                        {
                            Data = aluno,
                            Message = "Aluno encontrado.",
                            Success = true
                        };
                    }

                    return new ResultViewModel
                    {
                        Message = "aluno inexistente.",
                        Success = false
                    };

                }
                return null;
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

                    string query = "SELECT * FROM Alunos WHERE id = @id";
                    var aluno = (await conn.QueryFirstOrDefaultAsync<Aluno>(sql: query, new { Id = id }));
                    if (aluno != null)
                    {

                        return new ResultViewModel
                        {
                            Data = aluno,
                            Message = "Aluno encontrado.",
                            Success = true
                        };
                    }

                    return new ResultViewModel
                    {
                        Message = "aluno inexistente.",
                        Success = false
                    };
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
                var userExists = await this.GetById(aluno.Id);

                if (userExists != null)
                {

                    _db.Alunos.Update(aluno);
                    await _db.SaveChangesAsync();

                    return new ResultViewModel()
                    {
                        Message = "aluno atualizado com sucesso.",
                        Data = aluno,
                        Success = true
                    };
                }

                return new ResultViewModel()
                {
                    Message = "aluno não existente",
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
