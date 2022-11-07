using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.ExtensionsMethods
{
    public static class ProfessorValidate<T>
    {
        public static ResultViewModel Create(T t)
        {

            var sucess = new ResultViewModel
            {
                Message = "Professor criado com sucesso!",
                Success = true,
                Data = t
            };

            var failed = new ResultViewModel
            {
                Message = "Professor não criado!",
                Success = false

            };

            return t != null ? sucess : failed;

        }






        public static ResultViewModel List(List<T> T)
        {

            var failed = new ResultViewModel
            {
                Message = "não existem Professores.",
                Success = false
            };


            var sucess = new ResultViewModel
            {
                Data = T,
                Message = "Professores encontrados.",
                Success = true
            };

            return T.Count > 0 ? sucess : failed;


        }

        public static ResultViewModel ListAlunos(List<T> T)
        {

            var failed = new ResultViewModel
            {
                Message = "não existem alunos com o professor Informado.",
                Success = false
            };


            var sucess = new ResultViewModel
            {
                Data = T,
                Message = "Alunos encontrados.",
                Success = true
            };

            return T.Count > 0 ? sucess : failed;


        }



        public static ResultViewModel Select(T T)
        {

            var failed = new ResultViewModel
            {
                Message = "Professor inexistente.",
                Success = false
            };


            var sucess = new ResultViewModel
            {
                Data = T,
                Message = "Professor encontrado.",
                Success = true
            };

            return T != null ? sucess : failed;
        }
    }
}
