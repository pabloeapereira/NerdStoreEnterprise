using FluentValidation.Results;
using MediatR;
using NSE.Clientes.API.Models;
using NSE.Core.Messages;

namespace NSE.Clientes.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarClienteCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            if (!await ClienteNaoExisteAsync(request.Cpf)) return ValidationResult;

            await _clienteRepository.AddAsync(new Cliente(request.Id, request.Nome, request.Email, request.Cpf));

            return await CommitAsync(_clienteRepository.UnitOfWork);
        }

        private async ValueTask<bool> ClienteNaoExisteAsync(string cpf)
        {
            if (!await _clienteRepository.ClienteExistsByCpfAsync(cpf)) return true;

            AddError("Este CPF já está em uso.");
            return false;
        }
    }
}