using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace CadastroAlunos.Core.Entities;

public abstract class Base
{


    internal List<string> _errors;
    public IReadOnlyCollection<string> Erros => _errors;


    public bool IsValid => _errors.Count == 0;

    private void AddErrorList(IList<ValidationFailure> errors)
    {
        foreach (var error in errors)
            _errors.Add(error.ErrorMessage);
    }

    public bool Validate<T, J>(T validator, J obj)
        where T : AbstractValidator<J>
    {
        var validation = validator.Validate(obj);

        if (validation.Errors.Count > 0)
            AddErrorList(validation.Errors);

        return _errors.Count == 0;
    }

}
