using System;
using System.Collections.Generic;
using CVAVT.Models;
using Microsoft.EntityFrameworkCore;

namespace CVAVT.SQLiteDB;

public partial class SQLiteKontext : DbContext
{
    public SQLiteKontext()
    {
    }

    public SQLiteKontext(DbContextOptions<SQLiteKontext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aktivitaet> Aktivitaet { get; set; }
    public virtual DbSet<ActivitaetKategorie> ActivitaetKategorie { get; set; }

    public virtual DbSet<Models.Kategorie> Kategorie { get; set; }

    public virtual DbSet<Leiter> Leiter { get; set; }

    public virtual DbSet<Teilnehmer> Teilnehmer { get; set; }
    public virtual DbSet<ViewTeilnehmerIstAnzahl> ViewTeilnehmerIstAnzahl { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\C#\\CVAVT\\CVAVT\\SQLiteDB\\SQLite.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Aktivitaet>(entity =>
        {
            entity.HasKey(e => e.AktivitaetenId);

            entity.ToTable("Aktivitaet");

            entity.Property(e => e.AktivitaetenId).HasColumnName("AktivitaetenID");
            entity.Property(e => e.AktivitaetenDatum).HasColumnType("DATE");
            entity.Property(e => e.AktivitaetenZeit).HasColumnType("DATETIME");
            entity.Property(e => e.LeiterIdfk).HasColumnName("LeiterIDFK");
            entity.Property(e => e.AktivitaetenMaxTeilnehmer).HasColumnName("AktivitaetenMaxTeilnehmer");
            entity.Property(e => e.AktivitaetenInformation).HasColumnName("AktivitaetenInformation");

            entity.HasOne(d => d.LeiterIdfkNavigation).WithMany(p => p.Aktivitaet)
                .HasForeignKey(d => d.LeiterIdfk)
                .OnDelete(DeleteBehavior.ClientSetNull);

        });

        modelBuilder.Entity<Models.Kategorie>(entity =>
        {
            entity.ToTable("Kategorie");

            entity.Property(e => e.KategorieId).HasColumnName("KategorieID");
        });

        modelBuilder.Entity<Leiter>(entity =>
        {
            entity.ToTable("Leiter");

            entity.Property(e => e.LeiterId).HasColumnName("LeiterID");
        });

        modelBuilder.Entity<Teilnehmer>(entity =>
        {
            entity.ToTable("Teilnehmer");

            entity.Property(e => e.TeilnehmerId).HasColumnName("TeilnehmerID");
            entity.Property(e => e.AktivitaetIdfk).HasColumnName("AktivitaetIDFK");

            // manuell hinzu
            entity.HasOne(d => d.AktivitaetIdfkNavigation).WithMany(p => p.Teilnehmer)
            .HasForeignKey(d => d.AktivitaetIdfk)
            .OnDelete(DeleteBehavior.ClientSetNull);
        });
        modelBuilder.Entity<ViewTeilnehmerIstAnzahl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewTeilnehmerIstAnzahl");

            entity.Property(e => e.AktivitaetIdfk).HasColumnName("AktivitaetIDFK");
        });

        // Für ActivitaetKategorie
        modelBuilder.Entity<Models.ActivitaetKategorie>()
    .HasKey(ak => new { ak.ActivitaetIdfk, ak.KategorieIdfk });

        modelBuilder.Entity<Models.ActivitaetKategorie>()
            .HasOne(ak => ak.ActivitaetIdfkNavigation)
            .WithMany(a => a.ActivitaetKategorie)
            .HasForeignKey(ak => ak.ActivitaetIdfk)
            .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<Models.ActivitaetKategorie>()
            .HasOne(ak => ak.KategorieIdfkNavigation)
            .WithMany(k => k.ActivitaetKategorie)
            .HasForeignKey(ak => ak.KategorieIdfk)
            .OnDelete(DeleteBehavior.ClientSetNull);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
