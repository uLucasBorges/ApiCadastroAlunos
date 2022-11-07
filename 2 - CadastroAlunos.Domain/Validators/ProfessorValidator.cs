using ApiCadastroAlunos.Core.Models;
using FluentValidation;

namespace ApiCadastroAlunos.Models.Validators
{
    public class ProfessorValidator : AbstractValidator<Professor>
    {
        public ProfessorValidator()
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
                .WithMessage("O nome deve ter no mínimo 3 caracteres.")

                .MaximumLength(50)
                .WithMessage("O nome deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Cep)
                .NotNull()
                .WithMessage("O cep não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O cep não pode ser vazio.")

                .MinimumLength(5)
                .WithMessage("O cep deve ter no mínimo 10 caracteres.")

                .MaximumLength(15)
                .WithMessage("O cep deve ter no máximo 180 caracteres.");

            RuleFor(x => x.logradouro)
                .NotNull()
                .WithMessage("O logradouro não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O logradouro de celular não pode ser vazio.")

                .MinimumLength(10)
                .WithMessage("O logradouro deve ter no mínimo 10 caracteres.")

                .MaximumLength(100)
                .WithMessage("O logradouro deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Cidade)
                .NotNull()
                .WithMessage("A cidade não pode ser nula.")

                .NotEmpty()
                .WithMessage("A cidade não pode ser vazia.")

                .MinimumLength(3)
                .WithMessage("o nome da cidade deve ter no mínimo 3 caracteres.")

                .MaximumLength(50)
                .WithMessage("o nome da cidade deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Cpf)
               .NotNull()
               .WithMessage("O Cpf não pode ser nula.")

               .NotEmpty()
               .WithMessage("o Cpf não pode ser vazia.")

               .MinimumLength(1)
               .WithMessage("o Cpf deve ter no mínimo 1 caracteres.")

               .MaximumLength(11)
               .WithMessage("o Cpf deve ter no máximo 11 caracteres.");
            


            RuleFor(x => x.Materia)
               .NotNull()
               .WithMessage("A Materia não pode ser nula.")

               .NotEmpty()
               .WithMessage("A Materia não pode ser vazia.")

               .MinimumLength(5)
               .WithMessage("o nome da Materia deve ter no mínimo 5 caracteres.")

               .MaximumLength(20)
               .WithMessage("o nome da Materia deve ter no máximo 20 caracteres.");

 
        }
    }
}
