﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CVAVT.Models;

public partial class CVAVTContext : DbContext
{
    //public CVAVTContext() { }
    public CVAVTContext(DbContextOptions<CVAVTContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivitaetKategorie> ActivitaetKategorie { get; set; }

    public virtual DbSet<Aktivitaet> Aktivitaet { get; set; }

    public virtual DbSet<Kategorie> Kategorie { get; set; }

    public virtual DbSet<Leiter> Leiter { get; set; }

    public virtual DbSet<Teilnehmer> Teilnehmer { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-0O4SDSG\\SQLEXPRESS;Initial Catalog=CVAVT;Integrated Security=True; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivitaetKategorie>(entity =>
        {
            entity.HasKey(e => new { e.ActivitaetIdfk, e.KategorieIdfk });

            entity.Property(e => e.ActivitaetIdfk).HasColumnName("ActivitaetIDFK");
            entity.Property(e => e.KategorieIdfk).HasColumnName("KategorieIDFK");

            entity.HasOne(d => d.ActivitaetIdfkNavigation).WithMany(p => p.ActivitaetKategorie)
                .HasForeignKey(d => d.ActivitaetIdfk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivitaetKategorie_Aktivitaet");

            entity.HasOne(d => d.KategorieIdfkNavigation).WithMany(p => p.ActivitaetKategorie)
                .HasForeignKey(d => d.KategorieIdfk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivitaetKategorie_Kategorie");
        });

        modelBuilder.Entity<Aktivitaet>(entity =>
        {
            entity.HasKey(e => e.AktivitaetenId).HasName("PK__Aktivita__68838C520C3AFF0F");

            entity.Property(e => e.AktivitaetenId).HasColumnName("AktivitaetenID");
            entity.Property(e => e.AktivitaetenArt)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.AktivitaetenDatum).HasColumnType("date");
            entity.Property(e => e.AktivitaetenInformation).HasMaxLength(100);
            entity.Property(e => e.AktivitaetenName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.AktivitaetenZeit).HasColumnType("datetime");
            entity.Property(e => e.LeiterIdfk).HasColumnName("LeiterIDFK");

            entity.HasOne(d => d.LeiterIdfkNavigation).WithMany(p => p.Aktivitaet)
                .HasForeignKey(d => d.LeiterIdfk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Aktivitaet_Leiter");
        });

        modelBuilder.Entity<Kategorie>(entity =>
        {
            entity.HasKey(e => e.KategorieId).HasName("PK__Kategori__32DA9122EF254834");

            entity.Property(e => e.KategorieId).HasColumnName("KategorieID");
            entity.Property(e => e.KategorieName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Leiter>(entity =>
        {
            entity.HasKey(e => e.LeiterId).HasName("PK__Leiter__AFA09D08EC4C1CFA");

            entity.Property(e => e.LeiterId).HasColumnName("LeiterID");
            entity.Property(e => e.LeiterName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Teilnehmer>(entity =>
        {
            entity.HasKey(e => e.TeilnehmerId).HasName("PK__Teilnehm__EF4CA5C6F765B838");

            entity.Property(e => e.TeilnehmerId).HasColumnName("TeilnehmerID");
            entity.Property(e => e.AktivitaetIdfk).HasColumnName("AktivitaetIDFK");
            entity.Property(e => e.TeilnehmerName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.AktivitaetIdfkNavigation).WithMany(p => p.Teilnehmer)
                .HasForeignKey(d => d.AktivitaetIdfk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teilnehmer_Aktivitaet");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}