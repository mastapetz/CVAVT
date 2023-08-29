﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using CVAVT.SQLiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CVAVT.Models;

public partial class Aktivitaet
{
    public int AktivitaetenId { get; set; }

    public string AktivitaetenName { get; set; }

    public string AktivitaetenArt { get; set; }

    public DateTime AktivitaetenDatum { get; set; }

    public DateTime AktivitaetenZeit { get; set; }

    public double AktivitaetenDauer { get; set; }

    public int? AktivitaetenMaxTeilnehmer { get; set; }

    public bool AktivitaetenVorwissenNoetig { get; set; }

    public string AktivitaetenInformation { get; set; }

    public int LeiterIdfk { get; set; }

    public virtual ICollection<ActivitaetKategorie> ActivitaetKategorie { get; set; } = new List<ActivitaetKategorie>();

    public virtual Leiter LeiterIdfkNavigation { get; set; }

    public virtual ICollection<Teilnehmer> Teilnehmer { get; set; } = new List<Teilnehmer>();


    // Um Ist auch im Mainwindow anzuzeigen
//    public int AktivitaetenIstTeilnehmer
//    {
//        get
//        {
//            using (SQLiteKontext context = new SQLiteKontext() )
//            {
//                var viewTeilnehmer = context.ViewTeilnehmerIstAnzahl
//                    .FirstOrDefault(v => v.AktivitaetIdfk == this.AktivitaetenId);

//                if (viewTeilnehmer != null)
//                {
//                    return viewTeilnehmer.TeilnehmerIstAnzahl ?? 0;
//                }

//                return 0;
//            }
//        }
//    }
}