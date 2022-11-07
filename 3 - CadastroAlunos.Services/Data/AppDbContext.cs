
using ApiCadastroAlunos.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CadastroAlunos.Infra.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professor { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Aluno>(x =>
        //    {
        //        x.Property<int>("Id");
        //        x.HasKey("Id");
        //        x.HasOne(x => x.professor).WithMany(x => x.Alunos);
        //    });


        //    modelBuilder.Entity<Professor>(x =>
        //    {
        //        x.Property<int>("Id");
        //        x.HasKey("Id");
        //        x.HasMany(x => x.Alunos);
        //    });

        //}

    }
}


