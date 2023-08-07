using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CVAVT.SQLiteDB
{
    public class SQLiteContext : DbContext
    {
        public DbSet<Models.Aktivitaet> Aktivitaet { get; set; }
        public DbSet<Models.Leiter> Leiter { get; set; }
        public DbSet<Models.Teilnehmer> Teilnehmer { get; set; }
        public DbSet<Models.Kategorie> Kategorie { get; set; }
        public DbSet<Models.ActivitaetKategorie> ActivitaetKategorie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=E:\\Programmieren lernen\\c#\\Wifi\\ConventionVereinsAktivitaetenVerwaltungsTool\\CVAVT\\SQLiteDB\\SQLite.db;Version=3;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.ActivitaetKategorie>(entity =>
            {
                entity.HasKey(e => new { e.ActivitaetIdfk, e.KategorieIdfk });

                entity.HasOne(d => d.ActivitaetIdfkNavigation)
                    .WithMany(p => p.ActivitaetKategorie)
                    .HasForeignKey(d => d.ActivitaetIdfk)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.KategorieIdfkNavigation)
                    .WithMany(p => p.ActivitaetKategorie)
                    .HasForeignKey(d => d.KategorieIdfk)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Models.Aktivitaet>(entity =>
            {
                entity.HasOne(d => d.LeiterIdfkNavigation)
                    .WithMany(p => p.Aktivitaet)
                    .HasForeignKey(d => d.LeiterIdfk)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Models.Kategorie>(entity =>
            {
                entity.HasOne(d => d.KategorieId);




            });

            // ...
            // Define other relationships and constraints here
            // ...

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
