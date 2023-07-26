using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVAVT.Models
{
    public partial class Leiter
    {
        public override bool Equals(object obj)
        {
            if (obj is Leiter)
            {
                Leiter leiterCombo = obj as Leiter;
                return this.LeiterId == leiterCombo.LeiterId;
            }
            return false;
        }

        public override string ToString()
        {
            return this.LeiterName.ToString();
        }
    }

}
