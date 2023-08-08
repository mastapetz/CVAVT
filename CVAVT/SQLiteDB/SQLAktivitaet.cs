// Auto generiert (Command siehe mainwindowviewmode in kommentaren) und umbennant
// --
using System;
using System.Collections.Generic;
using System.Linq;

namespace CVAVT.SQLiteDB;

public partial class SQLAktivitaet
{
    public long AktivitaetenId { get; set; }

    public string AktivitaetenName { get; set; } = null!;

    public string AktivitaetenArt { get; set; } = null!;

    public byte[] AktivitaetenDatum { get; set; } = null!;

    public byte[] AktivitaetenZeit { get; set; } = null!;

    public double AktivitaetenDauer { get; set; }

    public long? AktivitaetenMaxTeilnehmer { get; set; }

    public long AktivitaetenVorwissenNoetig { get; set; }

    public string? AktivitaetenInformation { get; set; }

    public long LeiterIdfk { get; set; }

    public virtual SQLLeiter LeiterIdfkNavigation { get; set; } = null!;

    public virtual ICollection<SQLKategorie> KategorieIdfks { get; set; } = new List<SQLKategorie>();
}
