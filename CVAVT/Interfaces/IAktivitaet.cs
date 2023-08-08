using CVAVT.Models;
using CVAVT.SQLiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVAVT.Interfaces
{
    public interface IAktivitaet
    {/*
        // gleiche Datentypen
        double AktivitaetenDauer { get; set; }
        public string AktivitaetenName { get; set; }
        public string AktivitaetenArt { get; set; }
        public string AktivitaetenInformation { get; set; }

        // unterschiedliche Datentypen => Konvert
        long AktivitaetenMaxTeilnehmer { get; set; } // Hier verwenden wir long für die Schnittstelle
        int GetMaxTeilnehmerAsInt(); // Konvertiert long zu int
        void SetMaxTeilnehmerFromInt(int maxTeilnehmer); // Konvertiert int zu long
                                                         // ...

        //---------------
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


        //int AktivitaetenId { get; set; }
        //DateTime GetAktivitaetenDatum();
        //void SetAktivitaetenDatum(DateTime datum);
        */
    }
}
