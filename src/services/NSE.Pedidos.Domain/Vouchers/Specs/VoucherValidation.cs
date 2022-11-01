using NetDevPack.Specification;

namespace NSE.Pedidos.Domain.Vouchers.Specs
{
    public sealed class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            var dataSpec = new VoucherDataSpecification();
            var qtdeSpec = new VoucherQuantidadeSpecification();
            var ativoSpec = new VoucherAtivoSpecification();

            Add(nameof(dataSpec), new Rule<Voucher>(dataSpec, "Este voucher está expirado"));
            Add(nameof(qtdeSpec), new Rule<Voucher>(qtdeSpec, "Este voucher já foi utilizado"));
            Add(nameof(ativoSpec), new Rule<Voucher>(ativoSpec, "Este não está mais ativo"));
        }
    }
}