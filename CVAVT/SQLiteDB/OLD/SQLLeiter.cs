using System;
using System.Collections.Generic;

namespace CVAVT.SQLiteDB.OLD;

public partial class SQLLeiter
{
    public long LeiterId { get; set; }

    public string LeiterName { get; set; } = null!;

    public virtual ICollection<SQLAktivitaet> Aktivitaets { get; set; } = new List<SQLAktivitaet>();
}
