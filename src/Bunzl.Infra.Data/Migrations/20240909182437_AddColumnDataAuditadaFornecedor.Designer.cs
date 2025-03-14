﻿// <auto-generated />
using System;
using Bunzl.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bunzl.Infra.Data.Migrations
{
    [DbContext(typeof(BunzlContext))]
    [Migration("20240909182437_AddColumnDataAuditadaFornecedor")]
    partial class AddColumnDataAuditadaFornecedor
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bunzl.Domain.Entities.Auditoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EntidadeId")
                        .HasColumnType("uuid");

                    b.Property<string>("EntidadeNome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Funcao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TipoAuditoria")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UsuarioAlteracao")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsuarioCriacao")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Auditoria", (string)null);
                });

            modelBuilder.Entity("Bunzl.Domain.Entities.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataUltimaAtualizacao")
                        .HasColumnType("timestamptz");

                    b.Property<string>("EmpresaBunzlId")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("UsuarioAlteracao")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsuarioCriacao")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Empresa", (string)null);
                });

            modelBuilder.Entity("Bunzl.Domain.Entities.Fornecedor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bairro")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Cep")
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("CodigoERP")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("CodigoIdentificador")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Contato")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataFabricaAuditada")
                        .HasColumnType("timestamptz");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("EstadoProvincia")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FabricasAuditadas")
                        .HasColumnType("text");

                    b.Property<bool>("FlagAtivo")
                        .HasColumnType("boolean");

                    b.Property<bool>("FlagFabricaAuditadaBunzl")
                        .HasColumnType("boolean");

                    b.Property<bool>("FlagJaEhParceiroBunzl")
                        .HasColumnType("boolean");

                    b.Property<bool>("FlagVeioDoERP")
                        .HasColumnType("boolean");

                    b.Property<string>("InformacoesFabricas")
                        .HasColumnType("text");

                    b.Property<string>("InformacoesGerais")
                        .HasColumnType("text");

                    b.Property<string>("InformacoesMercados")
                        .HasColumnType("text");

                    b.Property<string>("InformacoesPoliticas")
                        .HasColumnType("text");

                    b.Property<string>("InformacoesProdutos")
                        .HasColumnType("text");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Pais")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefone")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("TelefoneArea")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<Guid>("UsuarioAlteracao")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsuarioCriacao")
                        .HasColumnType("uuid");

                    b.Property<string>("Website")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("WhatsApp")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("WhatsAppArea")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.HasKey("Id");

                    b.ToTable("Fornecedor", (string)null);
                });

            modelBuilder.Entity("Bunzl.Domain.Entities.FornecedorDadoBancario", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("FornecedorId");

                    b.Property<string>("Agencia")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Bairro")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Cep")
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)");

                    b.Property<string>("Cidade")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EstadoProvincia")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Logradouro")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("NomeBanco")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("NomeBeneficiario")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Numero")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("NumeroBanco")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("NumeroContaCorrente")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Observacao")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Pais")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Swift")
                        .HasMaxLength(9)
                        .HasColumnType("character varying(9)");

                    b.Property<Guid>("UsuarioAlteracao")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsuarioCriacao")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("FornecedorDadoBancario", (string)null);
                });

            modelBuilder.Entity("Bunzl.Domain.Entities.FornecedorDocumento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FornecedorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Observacao")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("UsuarioAlteracao")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsuarioCriacao")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("FornecedorDocumento", (string)null);
                });

            modelBuilder.Entity("Bunzl.Domain.Entities.FornecedorObservacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FornecedorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<Guid>("UsuarioAlteracao")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsuarioCriacao")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FornecedorId");

                    b.ToTable("FornecedorObservacao", (string)null);
                });

            modelBuilder.Entity("Bunzl.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Area")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<Guid?>("ChaveCadastro")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ChaveResetSenha")
                        .HasColumnType("uuid");

                    b.Property<string>("CodigoOtp")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataChaveResetSenha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataGeracaoCodigoOtp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DataPrimeiroLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<bool>("FlagAtivo")
                        .HasColumnType("boolean");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("PerfilPermissao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Senha")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<DateTime?>("UltimoLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UsuarioAlteracao")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsuarioCriacao")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("Fornecedor_x_Empresa", b =>
                {
                    b.Property<Guid>("FornecedorId")
                        .HasColumnType("uuid")
                        .HasColumnName("FornecedorId");

                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("uuid")
                        .HasColumnName("EmpresaId");

                    b.HasKey("FornecedorId", "EmpresaId");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Fornecedor_x_Empresa");
                });

            modelBuilder.Entity("Usuario_x_Empresa", b =>
                {
                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("uuid")
                        .HasColumnName("EmpresaId");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("UsuarioId");

                    b.HasKey("EmpresaId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Usuario_x_Empresa", (string)null);
                });

            modelBuilder.Entity("Usuario_x_Fornecedor", b =>
                {
                    b.Property<Guid>("FornecedorId")
                        .HasColumnType("uuid")
                        .HasColumnName("FornecedorId");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uuid")
                        .HasColumnName("UsuarioId");

                    b.HasKey("FornecedorId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Usuario_x_Fornecedor", (string)null);
                });

            modelBuilder.Entity("Bunzl.Domain.Entities.FornecedorDadoBancario", b =>
                {
                    b.HasOne("Bunzl.Domain.Entities.Fornecedor", "Fornecedor")
                        .WithOne("FornecedorDadoBancario")
                        .HasForeignKey("Bunzl.Domain.Entities.FornecedorDadoBancario", "Id");

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("Bunzl.Domain.Entities.FornecedorDocumento", b =>
                {
                    b.HasOne("Bunzl.Domain.Entities.Fornecedor", "Fornecedor")
                        .WithMany("FornecedorDocumentos")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("Bunzl.Domain.Entities.FornecedorObservacao", b =>
                {
                    b.HasOne("Bunzl.Domain.Entities.Fornecedor", "Fornecedor")
                        .WithMany("FornecedorObservacoes")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("Fornecedor_x_Empresa", b =>
                {
                    b.HasOne("Bunzl.Domain.Entities.Empresa", null)
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bunzl.Domain.Entities.Fornecedor", null)
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Usuario_x_Empresa", b =>
                {
                    b.HasOne("Bunzl.Domain.Entities.Empresa", null)
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Usuario_x_Empresa_EmpresaId");

                    b.HasOne("Bunzl.Domain.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Usuario_x_Empresa_UsuarioId");
                });

            modelBuilder.Entity("Usuario_x_Fornecedor", b =>
                {
                    b.HasOne("Bunzl.Domain.Entities.Fornecedor", null)
                        .WithMany()
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Usuario_x_Fornecedor_FornecedorId");

                    b.HasOne("Bunzl.Domain.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Usuario_x_Fornecedor_UsuarioId");
                });

            modelBuilder.Entity("Bunzl.Domain.Entities.Fornecedor", b =>
                {
                    b.Navigation("FornecedorDadoBancario");

                    b.Navigation("FornecedorDocumentos");

                    b.Navigation("FornecedorObservacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
