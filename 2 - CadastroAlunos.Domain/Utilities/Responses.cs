using ApiCadastroAlunos.Core.Models;
using ApiCadastroAlunos.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ApiCadastroAlunos.Utilities
{
    public static class Responses<T>
    {
        public static ResultViewModel UnauthorizedErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "você não está autorizado!",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel Create(T model)
        {
            var result = new ResultViewModel { Success = false };
            if (model != null)
            {
                result.Success = true;
                result.Data = model;
            }

            if (result.Success && model.GetType() == typeof(Professor))
            {

                result.Message = "Professor criado com sucesso!";
            }
            else if (result.Success && model.GetType() == typeof(Aluno))
            {
                result.Message = "Aluno criado com sucesso!";
            }
            else
            {
                result.Message = "Pessoa já existente!";
            }

            return result;
        }

        public static ResultViewModel Update(T T)
        {

            var sucess = new ResultViewModel()
            {
                Message = "Aluno atualizado com sucesso!",
                Data = T,
                Success = true
            };

            var failed = new ResultViewModel()
            {
                Message = "Aluno não existente!",
                Success = false
            };

            var p = new Professor();
            if (T.GetType() == p.GetType())
            {
                sucess.Message = "Professor atulizado com sucesso!";
                failed.Message = "Professor não existente!";

            }


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
                Message = "Aluno não existente!",
                Success = false,
                Data = T
            };

            var p = new Professor();
            if (T.GetType() == p.GetType())
            {
                sucess.Message = "Professor deletado com sucesso!";
                failed.Message = "Professor não existente!";

            }

            return T != null ? sucess : failed;

        }


        public static ResultViewModel List(List<T> T)
        {

            var failed = new ResultViewModel
            {
                Message = "não existem Alunos.",
                Success = false
            };


            var sucess = new ResultViewModel
            {
                Data = T,
                Message = "Alunos encontrados.",
                Success = true
            };

            var p = new Professor();
            if (T.GetType() == p.GetType())
            {
                sucess.Message = "Professores encontrados.";
                failed.Message = "não existem Professores.";

            }

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

            var p = new Professor();
            if (T.GetType() == p.GetType())
            {
                sucess.Message = "Professor encontrado!";
                failed.Message = "Professor inexistente!";

            }

            return T != null ? sucess : failed;
        }


    }
}
