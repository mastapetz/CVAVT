using CVAVT.Models;
using CVAVT.SQLiteDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVAVT.Factories
{
    internal class DbContextFactory
    {
        public static DbContext Create(bool useMSSQLVerbindung)
        {
            if (useMSSQLVerbindung)
            {
                var options = new DbContextOptions<CVAVTContext>();
                return new CVAVTContext(options);
            }
            else
            {
                var options = new DbContextOptions<SQLiteKontext>();
                return new SQLiteKontext(options);
            }
        }
    }
}
