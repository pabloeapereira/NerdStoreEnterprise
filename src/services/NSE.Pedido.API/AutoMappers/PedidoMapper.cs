using AutoMapper;
using NSE.Pedido.API.Application.Commands;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedidos.Domain.Pedidos;

namespace NSE.Pedido.API.AutoMappers
{
    public sealed class PedidoMapper : Profile
    {
        public PedidoMapper()
        {
            CreateMap<Pedidos.Domain.Pedidos.Pedido, PedidoDTO>()
                .ForMember(d => d.Data, o => o.MapFrom(s => s.DataCadastro))
                .ReverseMap();


            CreateMap<PedidoItem, PedidoItemDTO>()
                .ForMember(d => d.Nome, o => o.MapFrom(s => s.ProdutoNome))
                .ForMember(d => d.Imagem, o => o.MapFrom(s => s.ProdutoImagem))
                .ForMember(d => d.Valor, o => o.MapFrom(s => s.ValorUnitario))
                .ReverseMap();

            CreateMap<Endereco, EnderecoDTO>()
                .ReverseMap();

        }
    }
}