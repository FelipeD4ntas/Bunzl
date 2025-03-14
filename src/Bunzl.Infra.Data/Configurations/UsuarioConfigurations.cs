using Bunzl.Domain.Entities;
using Bunzl.Infra.CrossCutting.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bunzl.Infra.Data.Configurations;

public class UsuarioConfigurations : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Senha)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(p => p.Telefone)
            .IsRequired(false)
            .HasMaxLength(30);

        builder.Property(p => p.FlagAtivo)
            .IsRequired();

        builder.Property(p => p.FlagVeioDoERP)
            .IsRequired();

        builder.Property(p => p.UltimoLogin)
            .IsRequired(false);

        builder.Property(p => p.ChaveResetSenha)
            .IsRequired(false);

        builder.Property(p => p.PerfilPermissao)
            .IsRequired();

        builder.Property(p => p.CodigoOtp)
            .IsRequired(false)
            .HasMaxLength(50);

        builder.Property(p => p.Area)
            .IsRequired(false)
            .HasMaxLength(5);

        builder
            .HasMany(u => u.Empresas)
            .WithMany(e => e.Usuarios)
            .UsingEntity<Dictionary<string, object>>(
                "Usuario_x_Empresa",
                j => j
                    .HasOne<Empresa>()
                    .WithMany()
                    .HasForeignKey("EmpresaId") 
                    .HasConstraintName("FK_Usuario_x_Empresa_EmpresaId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Usuario>()
                    .WithMany()
                    .HasForeignKey("UsuarioId")
                    .HasConstraintName("FK_Usuario_x_Empresa_UsuarioId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.Property("UsuarioId").HasColumnName("UsuarioId");
                    j.Property("EmpresaId").HasColumnName("EmpresaId");
                    j.ToTable("Usuario_x_Empresa");
                });


        builder
            .HasMany(u => u.Fornecedores)
            .WithMany(f => f.Usuarios)
            .UsingEntity<Dictionary<string, object>>(
                "Usuario_x_Fornecedor",
                j => j
                    .HasOne<Fornecedor>()
                    .WithMany()
                    .HasForeignKey("FornecedorId")
                    .HasConstraintName("FK_Usuario_x_Fornecedor_FornecedorId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<Usuario>()
                    .WithMany()
                    .HasForeignKey("UsuarioId")
                    .HasConstraintName("FK_Usuario_x_Fornecedor_UsuarioId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.Property("UsuarioId").HasColumnName("UsuarioId");
                    j.Property("FornecedorId").HasColumnName("FornecedorId");
                    j.ToTable("Usuario_x_Fornecedor");
            });

        builder.ToTable(TabelasResources.Usuario);
    }
}