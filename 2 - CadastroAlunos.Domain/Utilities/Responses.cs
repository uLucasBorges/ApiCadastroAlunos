using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.Utilities
{
    public static class Responses
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

    }
}
