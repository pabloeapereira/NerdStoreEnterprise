using FluentValidation.Results;
using MediatR;
using NSE.Clientes.API.Application.Events;
using NSE.Clientes.API.Data.Repository;
using NSE.Clientes.API.Models;
using NSE.Core.Messages;

namespace NSE.Clientes.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, 
        IRequestHandler<RegistrarClienteCommand, ValidationResult>,
        IRequestHandler<AdicionarEnderecoCommand,ValidationResult>
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

            var cliente = (new Cliente(request.Id, request.Nome, request.Email, request.Cpf));
            await _clienteRepository.AddAsync(cliente);

            cliente.AddEvent(new ClienteRegistradoEvent(request.Id, request.Nome, request.Email, request.Cpf));

            return await CommitAsync(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AdicionarEnderecoCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid()) return request.ValidationResult;

            var endereco = new Endereco(request.Logradouro, request.Numero, request.Complemento, request.Bairro, request.Cep, request.Cidade, request.Estado);
            await _clienteRepository.AddAsync(endereco);

            return await CommitAsync(_clienteRepository.UnitOfWork);
        }

        #region Private Methods
        private async ValueTask<bool> ClienteNaoExisteAsync(string cpf)
        {
            if (!await _clienteRepository.ClienteExistsByCpfAsync(cpf)) return true;

            AddError("Este CPF já está em uso.");
            return false;
        }
        #endregion
    }
}