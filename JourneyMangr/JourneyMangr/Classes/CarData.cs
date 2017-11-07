using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMangr
{
    public class CarData
    {
        public CarData(string nev, long futottkm, long kmallas, int fogyasztas, string szerviz, long ar)
        {
            this.nev = nev;
            this.futottkm = futottkm;
            this.kmallas = kmallas;
            this.szerviz = szerviz;
            this.ar = ar;
        }
        public string nev { get; set; }
        public long futottkm { get; set; }
        public long kmallas { get; set; }
        public string szerviz { get; set; }
        public int fogyasztas { get; set; }
      public  long ar { get; set; }
        
    }
}
