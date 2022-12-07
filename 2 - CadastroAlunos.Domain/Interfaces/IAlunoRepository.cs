using ApiCadastroAlunos.ViewModel;

namespace ApiCadastroAlunos.Core.Interfaces;

public interface IAlunoRepository
{
    public Task<ResultViewModel> create(AlunoViewModel aluno);
    public Task<ResultViewModel> Get();
    public Task<ResultViewModel> GetById(int id);
    public Task<ResultViewModel> Update(AlunoViewModel aluno);
    public Task<ResultViewModel> Delete(int id);
}
