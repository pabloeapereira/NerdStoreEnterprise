using AutoMapper;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedidos.Domain.Vouchers;

namespace NSE.Pedido.API.AutoMappers
{
    public sealed class VoucherMapper : Profile
    {
        public VoucherMapper()
        {
            CreateMap<Voucher, VoucherDTO>();
        }
    }
}