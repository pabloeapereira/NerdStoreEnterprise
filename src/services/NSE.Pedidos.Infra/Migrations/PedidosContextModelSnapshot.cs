﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NSE.Pedidos.Infra.Data;

#nullable disable

namespace NSE.Pedidos.Infra.Migrations
{
    [DbContext(typeof(PedidosContext))]
    partial class PedidosContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.HasSequence<int>("MinhaSequencia")
                .StartsAt(1000L);

            modelBuilder.Entity("NSE.Pedidos.Domain.Pedidos.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PedidoStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("VoucherUtilizado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("VoucherId");

                    b.ToTable("Pedidos", (string)null);
                });

            modelBuilder.Entity("NSE.Pedidos.Domain.Pedidos.PedidoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProdutoImagem")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ProdutoNome")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("PedidoItens", (string)null);
                });

            modelBuilder.Entity("NSE.Pedidos.Domain.Vouchers.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataUtilizacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Percentual")
                        .HasColumnType("decimal(5,4)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("TipoDesconto")
                        .HasColumnType("int");

                    b.Property<bool>("Utilizado")
                        .HasColumnType("bit");

                    b.Property<decimal?>("ValorDesconto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Vouchers", (string)null);
                });

            modelBuilder.Entity("NSE.Pedidos.Domain.Pedidos.Pedido", b =>
                {
                    b.HasOne("NSE.Pedidos.Domain.Vouchers.Voucher", "Voucher")
                        .WithMany()
                        .HasForeignKey("VoucherId");

                    b.OwnsOne("NSE.Pedidos.Domain.Pedidos.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<Guid>("PedidoId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Bairro");

                            b1.Property<string>("Cep")
                                .IsRequired()
                                .HasMaxLength(8)
                                .HasColumnType("varchar(8)")
                                .HasColumnName("Cep");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Cidade");

                            b1.Property<string>("Complemento")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Complemento");

                            b1.Property<string>("Estado")
                                .IsRequired()
                                .HasMaxLength(2)
                                .HasColumnType("varchar(2)")
                                .HasColumnName("Estado");

                            b1.Property<string>("Logradouro")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Logradouro");

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("Numero");

                            b1.HasKey("PedidoId");

                            b1.ToTable("Pedidos");

                            b1.WithOwner()
                                .HasForeignKey("PedidoId");
                        });

                    b.Navigation("Endereco")
                        .IsRequired();

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("NSE.Pedidos.Domain.Pedidos.PedidoItem", b =>
                {
                    b.HasOne("NSE.Pedidos.Domain.Pedidos.Pedido", "Pedido")
                        .WithMany("PedidoItens")
                        .HasForeignKey("PedidoId")
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("NSE.Pedidos.Domain.Pedidos.Pedido", b =>
                {
                    b.Navigation("PedidoItens");
                });
#pragma warning restore 612, 618
        }
    }
}
