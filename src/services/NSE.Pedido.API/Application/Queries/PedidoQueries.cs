﻿using AutoMapper;
using Dapper;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedidos.Domain.Pedidos;

namespace NSE.Pedido.API.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<PedidoDTO?> GetUltimoPedidoAsync(Guid clienteId);
        Task<IEnumerable<PedidoDTO>> GetListByClienteIdAsync(Guid clienteId);
        Task<PedidoDTO> GetPedidosAutorizadosAsync();
    }
    public sealed class PedidoQueries: IPedidoQueries
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoQueries(IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<PedidoDTO?> GetUltimoPedidoAsync(Guid clienteId)
        {
            const string sql = @"SELECT TOP 1
                P.ID AS ProdutoId, P.CODIGO, P.VOUCHERUTILIZADO, P.DESCONTO,P.VALORTOTAL, P.PEDIDOSTATUS,
                P.LOGRADOURO, P.NUMERO, P.BAIRRO, P.CEP, P.COMPLEMENTO, P.CIDADE, P.ESTADO,
                PIT.ID AS ProdutoItemId, PIT.PRODUTONOME, PIT.QUANTIDADE, PIT.PRODUTOIMAGEM, PIT.ValorUnitario
                FROM PEDIDOS P
                INNER JOIN PEDIDOITENS PIT ON P.ID = PIT.PEDIDOID
                WHERE P.CLIENTEID = @clienteId
                AND P.DATACADASTRO between DATEADD(minute, -3,  GETDATE()) and DATEADD(minute, 0,  GETDATE())
                AND P.PEDIDOSTATUS = 1
                ORDER BY P.DATACADASTRO DESC";

            var pedido = await _pedidoRepository.GetDbConnection().QueryAsync<dynamic>(sql,
                new{clienteId});
            return MapearPedido(pedido);
        }

        public async Task<IEnumerable<PedidoDTO>> GetListByClienteIdAsync(Guid clienteId) =>
            _mapper.Map<IEnumerable<PedidoDTO>>(await _pedidoRepository.GetListByClienteIdAsync(clienteId));

        public async Task<PedidoDTO> GetPedidosAutorizadosAsync()
        {
            const string sql = 
                @"""SELECT TOP 1 P.ID as PedidoId, P.ID, P.CLIENTEID,
                 PI.ID as PedidoItemId, PI.ID, PI.PRODUTOID, PI.QUANTIDADE,
                FROM PEDIDOS P INNER JOIN PEDIDOITENS PI ON P.ID = PI.PEDIDOID
                WHERE P.PEDIDOSTATUS = 1
                ORDER BY P.DATACADASTRO
                """;

            var pedido = await _pedidoRepository.GetDbConnection().QueryAsync<PedidoDTO, PedidoItemDTO, PedidoDTO>(sql,
                (p, pi) =>
                {
                    p.PedidoItens = new List<PedidoItemDTO>
                    { 
                        pi
                    };
                    
                    return p;
                },splitOn: "PedidoId,PedidoItemId");

            return pedido.FirstOrDefault();
        }



        private PedidoDTO MapearPedido(dynamic result)
        {
            if (!((List<object>)result).Any()) return null;
            var pedido = new PedidoDTO
            {
                Codigo = result[0].CODIGO,
                Status = result[0].STATUS,
                ValorTotal = result[0].VALORTOTAL,
                Desconto = result[0].DESCONTO,
                VoucherUtilizado = result[0].VOUCHERUTILIZADO,
                PedidoItens = new List<PedidoItemDTO>(),
                Endereco = new()
                {
                    Logradouro = result[0].LOGRADOURO,
                    Bairro = result[0].BAIRRO,
                    Cep = result[0].CEP,
                    Cidade = result[0].CIDADE,
                    Complemento = result[0].COMPLEMENTO,
                    Estado = result[0].ESTADO,
                    Numero = result[0].NUMERO
                }
            };
            foreach (var item in result)
            {
                pedido.PedidoItens.Add(new ()
                {
                    Nome = item.PRODUTONOME,
                    Valor = item.VALORUNITARIO,
                    Quantidade = item.QUANTIDADE,
                    Imagem = item.PRODUTOIMAGEM
                });
            }

            return pedido;
        }
    }
}
