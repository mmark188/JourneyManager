using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMangr
{
    public class CarData
    {
        public CarData(string nev, long futottkm, long kmallas, string szerviz, long ar)
        {
            this.nev = nev;
            this.futottkm=futottkm;
            this.kmallas = kmallas;
            this.szerviz = szerviz;
            this.ar = ar;
        }
        string nev { get; set; }
        long futottkm { get; set; }
        long kmallas { get; set; }
        string szerviz { get; set; }
        long ar { get; set; }
        
    }
}
