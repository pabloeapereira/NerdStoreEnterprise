using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Pedidos.Domain.Vouchers;

namespace NSE.Pedidos.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly PedidosContext _context;

        public VoucherRepository(PedidosContext context)
        {
            _context = context;
        }

        public Task<Voucher?> GetByCodigoAsync(string codigo) =>
            _context.Vouchers.FirstOrDefaultAsync(v => v.Codigo == codigo);

        public void Update(Voucher voucher) => _context.Vouchers.Update(voucher);

        public IUnitOfWork UnitOfWork => _context;

        public void Dispose() => _context.Dispose();
    }
}