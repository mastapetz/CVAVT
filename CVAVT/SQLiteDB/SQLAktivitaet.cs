// Auto generiert (Command siehe mainwindowviewmode in kommentaren) und umbennant
// --
using CVAVT.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CVAVT.SQLiteDB;

public partial class SQLAktivitaet
{
    public int AktivitaetenId { get; set; }

    public string AktivitaetenName { get; set; }

    public string AktivitaetenArt { get; set; }

    public DateTime AktivitaetenDatum { get; set; }

    public DateTime AktivitaetenZeit { get; set; }

    public double AktivitaetenDauer { get; set; }

    public int? AktivitaetenMaxTeilnehmer { get; set; }

    public int AktivitaetenVorwissenNoetig { get; set; }

    public string? AktivitaetenInformation { get; set; }

    public int LeiterIdfk { get; set; }

    public virtual SQLLeiter LeiterIdfkNavigation { get; set; }

    public virtual ICollection<SQLKategorie> KategorieIdfks { get; set; } = new List<SQLKategorie>();

    // Um Ist auch im Mainwindow anzuzeigen
    public int AktivitaetenIstTeilnehmer
    {
        get
        {
            using (SQLiteKontext context = new SQLiteKontext())
            {
                var viewTeilnehmer = context.ViewTeilnehmerIstAnzahl
                    .FirstOrDefault(v => v.AktivitaetIdfk == this.AktivitaetenId);

                if (viewTeilnehmer != null)
                {
                    return viewTeilnehmer.TeilnehmerIstAnzahl ?? 0;
                }

                return 0;
            }
        }
    }
}
