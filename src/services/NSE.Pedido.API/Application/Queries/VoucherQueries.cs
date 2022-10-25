using AutoMapper;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedidos.Domain.Vouchers;

namespace NSE.Pedido.API.Application.Queries
{
    public interface IVoucherQueries
    {
        Task<VoucherDTO> GetByCodigoAsync(string codigo);
    }
    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IMapper _mapper;

        public VoucherQueries(IVoucherRepository voucherRepository, IMapper mapper)
        {
            _voucherRepository = voucherRepository;
            _mapper = mapper;
        }

        public async Task<VoucherDTO> GetByCodigoAsync(string codigo)
        {
            var voucher = await _voucherRepository.GetByCodigoAsync(codigo);

            if (voucher is null) return default;

            if (!voucher.EstaValidoParaUtilizacao()) return default;

            return _mapper.Map<VoucherDTO>(voucher);
        }
    }
}