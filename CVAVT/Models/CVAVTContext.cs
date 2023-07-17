﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CVAVT.Models;

public partial class CVAVTContext : DbContext
{
    public CVAVTContext(DbContextOptions<CVAVTContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivitaetKategorie> ActivitaetKategorie { get; set; }

    public virtual DbSet<Aktivitaet> Aktivitaet { get; set; }

    public virtual DbSet<Kategorie> Kategorie { get; set; }

    public virtual DbSet<Leiter> Leiter { get; set; }

    public virtual DbSet<Teilnehmer> Teilnehmer { get; set; }

    public virtual DbSet<ZeigeAllesSortiertNachLeiter> ZeigeAllesSortiertNachLeiter { get; set; }

    public virtual DbSet<ZeigeAllesWichtige> ZeigeAllesWichtige { get; set; }

    public virtual DbSet<ZeigeNachLeiter2> ZeigeNachLeiter2 { get; set; }

    public virtual DbSet<ZeigeTeilnehmerListe> ZeigeTeilnehmerListe { get; set; }

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
            entity.HasKey(e => e.AktivitaetenId);

            entity.Property(e => e.AktivitaetenId)
                .ValueGeneratedNever()
                .HasColumnName("AktivitaetenID");
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
            entity.Property(e => e.KategorieId)
                .ValueGeneratedNever()
                .HasColumnName("KategorieID");
            entity.Property(e => e.KategorieName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Leiter>(entity =>
        {
            entity.Property(e => e.LeiterId)
                .ValueGeneratedNever()
                .HasColumnName("LeiterID");
            entity.Property(e => e.LeiterName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Teilnehmer>(entity =>
        {
            entity.Property(e => e.TeilnehmerId)
                .ValueGeneratedNever()
                .HasColumnName("TeilnehmerID");
            entity.Property(e => e.AktivitaetIdfk).HasColumnName("AktivitaetIDFK");
            entity.Property(e => e.TeilnehmerName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.AktivitaetIdfkNavigation).WithMany(p => p.Teilnehmer)
                .HasForeignKey(d => d.AktivitaetIdfk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teilnehmer_Aktivitaet");
        });

        modelBuilder.Entity<ZeigeAllesSortiertNachLeiter>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ZeigeAllesSortiertNachLeiter");

            entity.Property(e => e.AktivitaetenArt)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.AktivitaetenDatum).HasColumnType("date");
            entity.Property(e => e.AktivitaetenInformation).HasMaxLength(100);
            entity.Property(e => e.AktivitaetenName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.AktivitaetenZeit).HasColumnType("datetime");
            entity.Property(e => e.KategorieName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.LeiterName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<ZeigeAllesWichtige>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ZeigeAllesWichtige");

            entity.Property(e => e.AktivitaetenArt)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.AktivitaetenDatum).HasColumnType("date");
            entity.Property(e => e.AktivitaetenInformation).HasMaxLength(100);
            entity.Property(e => e.AktivitaetenName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.AktivitaetenZeit).HasColumnType("datetime");
            entity.Property(e => e.KategorieName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.LeiterName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TeilnehmerName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<ZeigeNachLeiter2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ZeigeNachLeiter2");

            entity.Property(e => e.AktivitaetenDatum).HasColumnType("date");
            entity.Property(e => e.AktivitaetenName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.LeiterName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<ZeigeTeilnehmerListe>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ZeigeTeilnehmerListe");

            entity.Property(e => e.AktivitaetenName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TeilnehmerName)
                .IsRequired()
                .HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}