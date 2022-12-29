using FluentValidation;
using ApiCadastroAlunos.Models;
using ApiCadastroAlunos.Core.Models;

namespace ApiCadastroAlunos.Models.Validators
{
    public class AlunoValidator : AbstractValidator<Aluno>
    {
        public AlunoValidator()
        {
            RuleFor(x => x)
               .NotEmpty()
               .WithMessage("o Aluno não pode ser vazia.")

               .NotNull()
               .WithMessage("o Aluno não pode ser nula.");

            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("O nome não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O nome não pode ser vazio.")

                .MinimumLength(3)
                .WithMessage("O nome deve ter no mínimo 3 caracteres.")

                .MaximumLength(50)
                .WithMessage("O nome deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Sobrenome)
                .NotNull()
                .WithMessage("O sobrenome não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O sobrenome não pode ser vazio.")

                .MinimumLength(3)
                .WithMessage("O sobrenome deve ter no mínimo 3 caracteres.")

                .MaximumLength(50)
                .WithMessage("O sobrenome deve ter no máximo 50 caracteres.");

            RuleFor(x => x.email)
                .NotNull()
                .WithMessage("O email não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O email não pode ser vazio.")

                .MinimumLength(10)
                .WithMessage("O email deve ter no mínimo 10 caracteres.")

                .MaximumLength(180)
                .WithMessage("O email deve ter no máximo 180 caracteres.")

                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                .WithMessage("O email informado não é válido.");

            RuleFor(x => x.celular)
                .NotNull()
                .WithMessage("O número de celular não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O número de celular não pode ser vazio.")

                .MinimumLength(10)
                .WithMessage("O número de celular deve ter no mínimo 10 caracteres.")

                .MaximumLength(15)
                .WithMessage("O número de celular deve ter no máximo 15 caracteres.");

            RuleFor(x => x.professorId)
                .NotEmpty()
                .WithMessage("você deve conter algum professor.")
                
                .NotNull()
                .WithMessage("seu professor não deve ser nulo.");

        }
    }
}
