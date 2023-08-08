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

    public virtual DbSet<SQLAktivitaet> Aktivitaets { get; set; }

    public virtual DbSet<SQLKategorie> Kategories { get; set; }

    public virtual DbSet<SQLLeiter> Leiters { get; set; }

    public virtual DbSet<SQLTeilnehmer> Teilnehmers { get; set; }
    public virtual DbSet<ViewTeilnehmerIstAnzahl> ViewTeilnehmerIstAnzahl { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=E:\\Programmieren lernen\\c#\\Wifi\\ConventionVereinsAktivitaetenVerwaltungsTool\\CVAVT\\SQLiteDB\\SQLite.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SQLAktivitaet>(entity =>
        {
            entity.HasKey(e => e.AktivitaetenId);

            entity.ToTable("Aktivitaet");

            entity.Property(e => e.AktivitaetenId).HasColumnName("AktivitaetenID");
            entity.Property(e => e.AktivitaetenDatum).HasColumnType("DATE");
            entity.Property(e => e.AktivitaetenZeit).HasColumnType("DATETIME");
            entity.Property(e => e.LeiterIdfk).HasColumnName("LeiterIDFK");

            entity.HasOne(d => d.LeiterIdfkNavigation).WithMany(p => p.Aktivitaets)
                .HasForeignKey(d => d.LeiterIdfk)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasMany(d => d.KategorieIdfks).WithMany(p => p.ActivitaetIdfks)
                .UsingEntity<Dictionary<string, object>>(
                    "ActivitaetKategorie",
                    r => r.HasOne<SQLKategorie>().WithMany()
                        .HasForeignKey("KategorieIdfk")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<SQLAktivitaet>().WithMany()
                        .HasForeignKey("ActivitaetIdfk")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("ActivitaetIdfk", "KategorieIdfk");
                        j.ToTable("ActivitaetKategorie");
                        j.IndexerProperty<int>("ActivitaetIdfk").HasColumnName("ActivitaetIDFK");
                        j.IndexerProperty<int>("KategorieIdfk").HasColumnName("KategorieIDFK");
                    });
        });

        modelBuilder.Entity<SQLKategorie>(entity =>
        {
            entity.ToTable("Kategorie");

            entity.Property(e => e.KategorieId).HasColumnName("KategorieID");
        });

        modelBuilder.Entity<SQLLeiter>(entity =>
        {
            entity.ToTable("Leiter");

            entity.Property(e => e.LeiterId).HasColumnName("LeiterID");
        });

        modelBuilder.Entity<SQLTeilnehmer>(entity =>
        {
            entity.ToTable("Teilnehmer");

            entity.Property(e => e.TeilnehmerId).HasColumnName("TeilnehmerID");
            entity.Property(e => e.AktivitaetIdfk).HasColumnName("AktivitaetIDFK");
        });
        modelBuilder.Entity<SQLViewTeilnehmerIstAnzahl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewTeilnehmerIstAnzahl");

            entity.Property(e => e.AktivitaetIdfk).HasColumnName("AktivitaetIDFK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
