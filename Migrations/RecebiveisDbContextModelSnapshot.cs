﻿// <auto-generated />
using System;
using GerenciadorRecebiveisAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GerenciadorRecebiveisAPI.Migrations
{
    [DbContext(typeof(RecebiveisDbContext))]
    partial class RecebiveisDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("GerenciadorRecebiveisAPI.Models.Carrinho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Carrinhos");
                });

            modelBuilder.Entity("GerenciadorRecebiveisAPI.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)");

                    b.Property<decimal>("Faturamento")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<int>("Ramo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("GerenciadorRecebiveisAPI.Models.NotaFiscal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CarrinhoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("CarrinhoId");

                    b.ToTable("NotasFiscais");
                });

            modelBuilder.Entity("GerenciadorRecebiveisAPI.Models.NotaFiscal", b =>
                {
                    b.HasOne("GerenciadorRecebiveisAPI.Models.Carrinho", null)
                        .WithMany("NotasFiscais")
                        .HasForeignKey("CarrinhoId");
                });

            modelBuilder.Entity("GerenciadorRecebiveisAPI.Models.Carrinho", b =>
                {
                    b.Navigation("NotasFiscais");
                });
#pragma warning restore 612, 618
        }
    }
}
