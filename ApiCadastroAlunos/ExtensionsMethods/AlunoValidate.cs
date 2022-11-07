using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.ExtensionsMethods
{
    public static class AlunoValidate<T>
    {

        public static ResultViewModel Create(T T)
        {

           var sucess = new ResultViewModel
              {
                  Message = "Aluno criado com sucesso!",
                  Success = true,
                  Data = T
                };
            

           var failed = new ResultViewModel
            {
                Message = "Aluno já existente!",
                Success = false
            };

            return T != null ? sucess : failed;

        }

        public static ResultViewModel Update(T T)
        {

             var sucess = new ResultViewModel()
                    {
                        Message = "aluno atualizado com sucesso.",
                        Data = T,
                        Success = true
                    };

              var failed = new ResultViewModel()
                {
                    Message = "aluno não existente",
                    Success = false
                };
               

            return T != null ? sucess : failed;

        }


        public static ResultViewModel Delete(T T)
        {

            var sucess = new ResultViewModel()
            {
                Message = "Aluno deletado com sucesso!",
                Success = true,
                Data = T

            };

            var failed = new ResultViewModel()
            {
                Message = "Aluno não existente.",
                Success = false,
                Data = T
            };


            return T != null ? sucess : failed;

        }


        public static ResultViewModel List(List<T> T)
        {
           
              var failed = new ResultViewModel
                {
                    Message = "não existem alunos.",
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
                Message = "Aluno inexistente.",
                Success = false
            };


            var sucess = new ResultViewModel
            {
                Data = T,
                Message = "Aluno encontrado.",
                Success = true
            };

            return T != null ? sucess : failed;


        }



    }
}
