using System.Data;
using System.Data.SqlClient;
using ApiCadastroAlunos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroAlunos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public IDbConnection Connection { get; set; }

        public DbSet<Aluno> Alunos { get; set; }


    }
}


