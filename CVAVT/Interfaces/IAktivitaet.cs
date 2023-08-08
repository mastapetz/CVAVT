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
    {
        // gleiche Datentypen
        double AktivitaetenDauer { get; set; }
        public string AktivitaetenName { get; set; }
        public string AktivitaetenArt { get; set; }
        public string AktivitaetenInformation { get; set; }


    }
}
