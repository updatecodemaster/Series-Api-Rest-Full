﻿// <auto-generated />
using DocumentacaoSwagger.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DocumentacaoSwagger.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20220614231553_PrimeiraMigracao")]
    partial class PrimeiraMigracao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("DocumentacaoSwagger.Models.Serie", b =>
                {
                    b.Property<int>("SerieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Categoria")
                        .HasColumnType("longtext");

                    b.Property<string>("Elenco")
                        .HasColumnType("longtext");

                    b.Property<int>("NumeroEpisodios")
                        .HasColumnType("int");

                    b.Property<string>("Sinopse")
                        .HasColumnType("longtext");

                    b.Property<int>("Temporada")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.HasKey("SerieId");

                    b.ToTable("Series");
                });
#pragma warning restore 612, 618
        }
    }
}
