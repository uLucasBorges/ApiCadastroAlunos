
using System.Data;
using ApiCadastroAlunos.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

public class AppDb : IDisposable
{
    public IDbConnection Connection { get; set; }

    public AppDb(IConfiguration configuration)
    {
        Connection = new SqlConnection(configuration.GetConnectionString("Default"));
        Connection.Open();
    }

    public void Dispose() => Connection?.Dispose();

}



