using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

public partial class PalestraContext : IdentityDbContext<AspNetUser>
{
    public PalestraContext()
    {
    }

    public PalestraContext(DbContextOptions<PalestraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaEsercizio> CategoriaEsercizios { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Esercizio> Esercizios { get; set; }

    public virtual DbSet<EsercizioMuscolo> EsercizioMuscolos { get; set; }

    public virtual DbSet<EsercizioSchedum> EsercizioScheda { get; set; }

    public virtual DbSet<Muscolo> Muscolos { get; set; }

    public virtual DbSet<Pagamento> Pagamentos { get; set; }

    public virtual DbSet<Schedum> Scheda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CategoriaEsercizio>(entity =>
        {
            entity.ToTable("CategoriaEsercizio");

            entity.Property(e => e.FkCategoria).HasColumnName("Fk_Categoria");
            entity.Property(e => e.FkEsercizio).HasColumnName("Fk_Esercizio");

            entity.HasOne(d => d.FkCategoriaNavigation).WithMany(p => p.CategoriaEsercizios)
                .HasForeignKey(d => d.FkCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoriaEsercizio_Categoria");

            entity.HasOne(d => d.FkEsercizioNavigation).WithMany(p => p.CategoriaEsercizios)
                .HasForeignKey(d => d.FkEsercizio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CategoriaEsercizio_Esercizio");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pk_Categoria");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente");

            entity.Property(e => e.Cognome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkUtente)
                .HasMaxLength(450)
                .HasColumnName("Fk_Utente");
            entity.Property(e => e.HaveAccess).HasColumnName("haveAccess");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroTelefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkUtenteNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.FkUtente)
                .HasConstraintName("FK_Cliente_AspNetUsers");
        });

        modelBuilder.Entity<Esercizio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pk_Esercizio");

            entity.ToTable("Esercizio");
        });

        modelBuilder.Entity<EsercizioMuscolo>(entity =>
        {
            entity.ToTable("EsercizioMuscolo");

            entity.Property(e => e.FkEsercizio).HasColumnName("Fk_Esercizio");
            entity.Property(e => e.FkMuscolo).HasColumnName("Fk_Muscolo");

            entity.HasOne(d => d.FkEsercizioNavigation).WithMany(p => p.EsercizioMuscolos)
                .HasForeignKey(d => d.FkEsercizio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EsercizioMuscolo_Esercizio");

            entity.HasOne(d => d.FkMuscoloNavigation).WithMany(p => p.EsercizioMuscolos)
                .HasForeignKey(d => d.FkMuscolo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EsercizioMuscolo_Muscolo");
        });

        modelBuilder.Entity<EsercizioSchedum>(entity =>
        {
            entity.Property(e => e.FkEsercizio).HasColumnName("Fk_Esercizio");
            entity.Property(e => e.FkScheda).HasColumnName("Fk_Scheda");
            entity.Property(e => e.Note)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.NumeroRipetizioni)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkEsercizioNavigation).WithMany(p => p.EsercizioScheda)
                .HasForeignKey(d => d.FkEsercizio)
                .HasConstraintName("FK_EsercizioScheda_Esercizio");

            entity.HasOne(d => d.FkSchedaNavigation).WithMany(p => p.EsercizioScheda)
                .HasForeignKey(d => d.FkScheda)
                .HasConstraintName("FK_EsercizioScheda_Scheda");
        });

        modelBuilder.Entity<Muscolo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pk_Muscolo");

            entity.ToTable("Muscolo");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Pagamenti");

            entity.ToTable("Pagamento");

            entity.Property(e => e.FkCliente).HasColumnName("Fk_Cliente");
            entity.Property(e => e.FkUtente)
                .HasMaxLength(450)
                .HasColumnName("Fk_Utente");
            entity.Property(e => e.ImportoPagamento).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.FkCliente)
                .HasConstraintName("FK_Pagamenti_Cliente");

            entity.HasOne(d => d.FkUtenteNavigation).WithMany(p => p.Pagamentos)
                .HasForeignKey(d => d.FkUtente)
                .HasConstraintName("FK_Pagamenti_AspNetUsers");
        });

        modelBuilder.Entity<Schedum>(entity =>
        {
            entity.Property(e => e.FkCliente).HasColumnName("Fk_Cliente");
            entity.Property(e => e.FkUtente)
                .HasMaxLength(450)
                .HasColumnName("Fk_Utente");
            entity.Property(e => e.Note)
                .HasMaxLength(256)
                .IsUnicode(false);

            entity.HasOne(d => d.FkClienteNavigation).WithMany(p => p.Scheda)
                .HasForeignKey(d => d.FkCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Scheda_Cliente");

            entity.HasOne(d => d.FkUtenteNavigation).WithMany(p => p.Scheda)
                .HasForeignKey(d => d.FkUtente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Scheda_AspNetUsers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
