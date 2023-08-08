using System;
using System.Collections.Generic;

namespace CVAVT.SQLiteDB.OLD;

public partial class SQLTeilnehmer
{
    public long TeilnehmerId { get; set; }

    public string TeilnehmerName { get; set; } = null!;

    public long AktivitaetIdfk { get; set; }
}
