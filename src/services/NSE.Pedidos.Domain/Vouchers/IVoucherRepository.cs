using NSE.Core.Data;

namespace NSE.Pedidos.Domain.Vouchers
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher?> GetByCodigoAsync(string codigo);
        void Update(Voucher voucher);
    }
}