using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.ExtensionsMethods
{
    public static class ProfessorValidate
    {
        public static ResultViewModel Create(Professor professor)
        {

            var sucess = new ResultViewModel
            {
                Message = "Professor criado com sucesso!",
                Success = true,
                Data = professor
            };

            var failed = new ResultViewModel
            {
                Message = "Professor não criado!",
                Success = false

            };

            return professor != null ? sucess : failed;

        }






        public static ResultViewModel List(List<ProfessorViewModel> professores)
        {

            var failed = new ResultViewModel
            {
                Message = "não existem Professores.",
                Success = false
            };


            var sucess = new ResultViewModel
            {
                Data = professores,
                Message = "Professores encontrados.",
                Success = true
            };

            return professores.Count > 0 ? sucess : failed;


        }

        public static ResultViewModel ListAlunos(List<Aluno> alunos)
        {

            var failed = new ResultViewModel
            {
                Message = "não existem alunos com o professor Informado.",
                Success = false
            };


            var sucess = new ResultViewModel
            {
                Data = alunos,
                Message = "Alunos encontrados.",
                Success = true
            };

            return alunos.Count > 0 ? sucess : failed;


        }



        public static ResultViewModel Select(ProfessorViewModel professor)
        {

            var failed = new ResultViewModel
            {
                Message = "Professor inexistente.",
                Success = false
            };


            var sucess = new ResultViewModel
            {
                Data = professor,
                Message = "Professor encontrado.",
                Success = true
            };

            return professor != null ? sucess : failed;
        }
    }
}
