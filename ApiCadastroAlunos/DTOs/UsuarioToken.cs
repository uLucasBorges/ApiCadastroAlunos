using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiCadastroAlunos.DTOs
{
    [Keyless]
    public class UsuarioToken
    {
        public int Id { get; set; }
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
