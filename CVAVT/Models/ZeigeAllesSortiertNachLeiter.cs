﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace CVAVT.Models;

public partial class ZeigeAllesSortiertNachLeiter
{
    public string LeiterName { get; set; }

    public string AktivitaetenName { get; set; }

    public string AktivitaetenArt { get; set; }

    public DateTime AktivitaetenDatum { get; set; }

    public DateTime AktivitaetenZeit { get; set; }

    public double AktivitaetenDauer { get; set; }

    public int? AktivitaetenMaxTeilnehmer { get; set; }

    public bool AktivitaetenVorwissenNoetig { get; set; }

    public string AktivitaetenInformation { get; set; }

    public string KategorieName { get; set; }
}