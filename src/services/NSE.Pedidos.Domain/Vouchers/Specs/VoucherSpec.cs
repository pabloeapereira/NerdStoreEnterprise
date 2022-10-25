using NetDevPack.Specification;
using System.Linq.Expressions;

namespace NSE.Pedidos.Domain.Vouchers.Specs
{
    public sealed class VoucherDataSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression() =>
            voucher => voucher.DataValidade >= DateTime.Now;
    }

    public sealed class VoucherQuantidadeSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression() =>
            voucher => voucher.Quantidade > 0;
    }

    public sealed class VoucherAtivoSpecification : Specification<Voucher>
    {
        public override Expression<Func<Voucher, bool>> ToExpression() =>
            voucher => voucher.Ativo && !voucher.Utilizado;
    }
}