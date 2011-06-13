using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TR_Verwaltung
{
    class TraningsRaumDaten
    {
        public int DatenbankId { get; set; }
        public Lehrer Lehrer { get; set; }
        public Lehrer TRLehrer { get; set; }
        public Schueler Schueler { get; set; }
        public DateTime Datum { get; set; }
        public int UebertragungsNr { get; set; }
        public string Massnahme { get; set; }
    }
}
