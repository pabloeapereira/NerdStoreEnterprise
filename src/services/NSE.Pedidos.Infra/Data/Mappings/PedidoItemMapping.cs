using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Pedidos.Domain.Pedidos;

namespace NSE.Pedidos.Infra.Data.Mappings
{
    public sealed class PedidoItemMapping : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItens");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProdutoNome)
                .IsRequired()
                .HasMaxLength(250)
                .HasColumnType("varchar(250)");

            builder.Property(x => x.ValorUnitario)
                .HasColumnType("decimal(18,2)");
        }
    }
}