using FluentValidation;
using FluentValidation.Results;
using NSE.Core.Messages;

namespace NSE.Clientes.API.Application.Commands
{
    public class RegistrarClienteCommand: Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegistrarClienteCommand(Guid id, string nome, string email, string cpf)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegistrarClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        internal class RegistrarClienteValidation : AbstractValidator<RegistrarClienteCommand>
        {
            public RegistrarClienteValidation()
            {
                RuleFor(c => c.Id).NotEqual(Guid.Empty).WithMessage("O campo {PropertyName} do cliente inválido.");

                RuleFor(c => c.Nome).NotEmpty().WithMessage("O campo {PropertyName} do cliente não foi informado.")
                    .MaximumLength(200).WithMessage("O campo {PropertyName} do cliente deve ter no máximo {MaxLength}");
    
            RuleFor(c => c.Cpf).Must(CpfValid).WithMessage("O campo {PropertyName} informado não é válido.");

                RuleFor(c => c.Email).Must(EmailValid).WithMessage("O campo {PropertyName} informado não é válido.");
            }

            protected bool CpfValid(string cpf) => Core.DomainObjects.Cpf.Validar(cpf);

            protected bool EmailValid(string email) => Core.DomainObjects.Email.Validar(email);
        }
    }
}