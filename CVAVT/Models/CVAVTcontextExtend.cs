using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVAVT.Models
{
    public partial class CVAVTContext
    {
        public CVAVTContext() { }
        //    public CVAVTContext(DbContextOptions<CVAVTContext> options)
        //: base(options)
        //    {
        //    }
        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //    => optionsBuilder.UseSqlServer("Data Source=DESKTOP-0O4SDSG\\SQLEXPRESS;Initial Catalog=CVAVT;Integrated Security=True; Encrypt=False");
    }
}
