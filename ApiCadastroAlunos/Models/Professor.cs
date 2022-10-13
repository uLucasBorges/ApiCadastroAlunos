using System.ComponentModel.DataAnnotations;

namespace ApiCadastroAlunos.Models
{
    public class Professor
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Insira um nome.")]
        public string Nome { get; set; }

        [StringLength(50, MinimumLength = 1, ErrorMessage = "Insira uma matéria.")]
        public string Materia { get; set; }


        public Professor()
        {
       
        }

        public Professor(int id, string nome, string materia)
        {
            Id = id;
            Nome = nome;
            Materia = materia;
       
        }
    }
}
