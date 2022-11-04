using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroAlunos.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Aluno>()
        //        .HasOne(x => x.professor)
        //        .WithMany(x => x.Alunos)
        //        .HasForeignKey(x => x.professorId)
        //        .IsRequired().HasPrincipalKey(x => x.Id);


        //    modelBuilder.Entity<Professor>()
        //        .HasMany(x => x.Alunos)
        //        .WithOne(x => x.professor)
        //        .HasPrincipalKey(x => x.Id)
        //        .IsRequired();
        //}

    }
}


