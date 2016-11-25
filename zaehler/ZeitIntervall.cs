using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zaehlerNS
{
    public enum ZeitIntervall
    {
        all, Sekunde, Minute, Stunde, Tag, Woche, Monat, Jahr, Jahrzehnt, none 
    } 

    public class Interval
    {
        static public DateTime abrunden(DateTime dt, ZeitIntervall intervall)
        {
            // todo xxx falsch für Monate und Jahre, muss extra behandelt werden da nicht immer gleich lang
            var delta = dt.Ticks % toTimespan(intervall).Ticks;
            return new DateTime(dt.Ticks - delta, dt.Kind);
        }


        static public  TimeSpan toTimespan(ZeitIntervall interval)
        {
            TimeSpan ts;
            switch (interval)
            {
                case ZeitIntervall.all:
                    ts = new TimeSpan(0,0,1);
                    break;
                case ZeitIntervall.Sekunde:
                    ts = new TimeSpan(0,0,1);
                    break;
                case ZeitIntervall.Minute:
                    ts = new TimeSpan(0, 1, 0);
                    break;
                case ZeitIntervall.Stunde:
                    ts = new TimeSpan(1, 0, 0);
                    break;
                case ZeitIntervall.Tag:
                    ts = new TimeSpan(1,0, 0, 0);
                    break;
                case ZeitIntervall.Woche:
                    ts = new TimeSpan(7, 0, 0, 0);
                    break;
                case ZeitIntervall.Monat:
                    ts = new TimeSpan(31, 0, 0, 0);
                    break;
                case ZeitIntervall.Jahr:
                    ts = new TimeSpan(365, 0, 0, 0);
                    break;
                case ZeitIntervall.Jahrzehnt:
                    ts = new TimeSpan(3653, 0, 0, 0);
                    break;
                case ZeitIntervall.none:
                    ts = new TimeSpan(999999, 0, 0, 0);
                    break;
                default:
                    ts = new TimeSpan(0, 0, 0);
                    break;
            }
            return ts;
        }
    }
}
