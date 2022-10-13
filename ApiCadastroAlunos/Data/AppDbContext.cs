using ApiCadastroAlunos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroAlunos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }

    }
}


