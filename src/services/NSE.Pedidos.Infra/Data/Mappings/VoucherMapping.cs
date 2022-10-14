using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Pedidos.Domain.Vouchers;

namespace NSE.Pedidos.Infra.Data.Mappings
{
    public class VoucherMapping : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> b)
        {
            b.ToTable("Vouchers");
            b.HasKey(v => v.Id);

            b.Property(v => v.Codigo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            b.Property(v => v.Percentual)
                .HasColumnType("decimal(5,4)");

            b.Property(v => v.ValorDesconto)
                .HasColumnType("decimal(18,2)");

        }
    }
}