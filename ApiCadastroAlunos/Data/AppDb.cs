using System.Data;
using Microsoft.Data.SqlClient;

namespace ApiCadastroAlunos.Data
{
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


}
