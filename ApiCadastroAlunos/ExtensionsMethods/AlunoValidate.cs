using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.ExtensionsMethods
{
    public static class AlunoValidate
    {

        public static ResultViewModel Create(AlunoViewModel aluno)
        {

           var sucess = new ResultViewModel
              {
                  Message = "Aluno criado com sucesso!",
                  Success = true,
                  Data = aluno
                };
            

           var failed = new ResultViewModel
            {
                Message = "Aluno já existente!",
                Success = false
            };

            return aluno != null ? sucess : failed;

        }

        public static ResultViewModel Update(Aluno aluno)
        {

             var sucess = new ResultViewModel()
                    {
                        Message = "aluno atualizado com sucesso.",
                        Data = aluno,
                        Success = true
                    };

              var failed = new ResultViewModel()
                {
                    Message = "aluno não existente",
                    Success = false
                };
               

            return aluno != null ? sucess : failed;

        }


        public static ResultViewModel Delete(ResultViewModel resultViewModel)
        {

            var sucess = new ResultViewModel()
            {
                Message = "Aluno deletado com sucesso!",
                Success = true,
                Data = resultViewModel.Data

            };

            var failed = new ResultViewModel()
            {
                Message = "Aluno não existente.",
                Success = false,
                Data = resultViewModel.Data
            };


            return resultViewModel.Data != null ? sucess : failed;

        }


        public static ResultViewModel List(List<AlunoViewModel> alunos)
        {
           
              var failed = new ResultViewModel
                {
                    Message = "não existem alunos.",
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


        public static ResultViewModel Select(Aluno aluno)
        {

            var failed = new ResultViewModel
            {
                Message = "Aluno inexistente.",
                Success = false
            };


            var sucess = new ResultViewModel
            {
                Data = aluno,
                Message = "Aluno encontrado.",
                Success = true
            };

            return aluno != null ? sucess : failed;


        }



    }
}
