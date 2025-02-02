﻿// <auto-generated />
using System;
using Catalogo.Api.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Catalogo.Api.Migrations;

[DbContext(typeof(CatalogoContext))]
[Migration("20250109021956_PopulaProdutos")]
partial class PopulaProdutos
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.4")
            .HasAnnotation("Relational:MaxIdentifierLength", 64);

        MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

        modelBuilder.Entity("Catalogo.Api.Models.Categoria", b =>
            {
                b.Property<int>("CategoriaId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CategoriaId"));

                b.Property<string>("ImagemUrl")
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnType("varchar(300)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnType("varchar(80)");

                b.HasKey("CategoriaId");

                b.ToTable("Categorias");
            });

        modelBuilder.Entity("Catalogo.Api.Models.Produto", b =>
            {
                b.Property<int>("ProdutoId")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ProdutoId"));

                b.Property<int>("CategoriaId")
                    .HasColumnType("int");

                b.Property<DateTime>("DataCadastro")
                    .HasColumnType("datetime(6)");

                b.Property<string>("Descricao")
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnType("varchar(300)");

                b.Property<float>("Estoque")
                    .HasColumnType("float");

                b.Property<string>("ImagemUrl")
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnType("varchar(300)");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasMaxLength(80)
                    .HasColumnType("varchar(80)");

                b.Property<decimal>("Preco")
                    .HasColumnType("decimal(10, 2)");

                b.HasKey("ProdutoId");

                b.HasIndex("CategoriaId");

                b.ToTable("Produtos");
            });

        modelBuilder.Entity("Catalogo.Api.Models.Produto", b =>
            {
                b.HasOne("Catalogo.Api.Models.Categoria", "Categoria")
                    .WithMany("Produtos")
                    .HasForeignKey("CategoriaId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Categoria");
            });

        modelBuilder.Entity("Catalogo.Api.Models.Categoria", b =>
            {
                b.Navigation("Produtos");
            });
#pragma warning restore 612, 618
    }
}
