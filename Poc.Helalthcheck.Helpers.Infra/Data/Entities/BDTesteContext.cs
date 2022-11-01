using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Poc.Healthcheck.Helpers.Infra.Data.Entities
{
    public partial class BDTesteContext : DbContext
    {
        public BDTesteContext()
        {
        }

        public BDTesteContext(DbContextOptions<BDTesteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artigo> Artigos { get; set; } = null!;
        public virtual DbSet<Comentario> Comentarios { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artigo>(entity =>
            {
                entity.ToTable("Artigo");

                entity.Property(e => e.Corpo).HasColumnType("text");

                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Marcacao)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Artigos)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Artigo_Usuario");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.ToTable("Comentario");

                entity.Property(e => e.Ativo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DataCriacao).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdArtigoNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdArtigo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Artigo_Comentario");

                entity.HasOne(d => d.IdComentarioNavigation)
                    .WithMany(p => p.InverseIdComentarioNavigation)
                    .HasForeignKey(d => d.IdComentario)
                    .HasConstraintName("FK_Comentario_Comentario");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Comentario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
