namespace ApiCadastroAlunos.ViewModel
{
    public class ResultViewModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public dynamic Data { get; set; }

        public ResultViewModel()
        {
        }

        public ResultViewModel(string message, bool success, dynamic data)
        {
            Message = message;
            Success = success;
            Data = data;
        }
    }
}
