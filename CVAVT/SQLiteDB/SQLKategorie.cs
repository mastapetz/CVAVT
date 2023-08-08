﻿using System;
using System.Collections.Generic;

namespace CVAVT.SQLiteDB;

public partial class SQLKategorie
{
    public long KategorieId { get; set; }

    public string KategorieName { get; set; } = null!;

    public virtual ICollection<SQLAktivitaet> ActivitaetIdfks { get; set; } = new List<SQLAktivitaet>();
}