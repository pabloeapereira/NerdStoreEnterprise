using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> b)
        {
            b.ToTable("Produtos");
            b.HasKey(x => x.Id);
            b.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)")
                .HasMaxLength(250);

            b.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

            b.Property(x => x.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)")
                .HasMaxLength(250);

            b.Property(x => x.DataCadastro)
                .IsRequired()
                .HasColumnType("datetime");
        }
    }
}