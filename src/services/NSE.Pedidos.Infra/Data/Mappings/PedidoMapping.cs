using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Pedidos.Domain.Pedidos;

namespace NSE.Pedidos.Infra.Data.Mappings
{
    public sealed class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(p => p.Endereco, e =>
            {
                e.Property(pe => pe.Logradouro).HasColumnName("Logradouro");

                e.Property(pe => pe.Numero).HasColumnName("Numero");

                e.Property(pe => pe.Complemento).HasColumnName("Complemento");

                e.Property(pe => pe.Bairro).HasColumnName("Bairro");

                e.Property(pe => pe.Cep).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnName("Cep");

                e.Property(pe => pe.Cidade).HasColumnName("Cidade");

                e.Property(pe => pe.Estado).HasMaxLength(2).HasColumnType("varchar(2)").HasColumnName("Estado");
            });

            builder.Property(x => x.Codigo).HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

            builder.HasMany(c => c.PedidoItens)
                .WithOne(pe => pe.Pedido)
                .HasForeignKey(pe => pe.PedidoId);
        }
    }
}