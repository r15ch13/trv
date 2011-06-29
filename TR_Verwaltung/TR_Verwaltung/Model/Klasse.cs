using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TR_Verwaltung.Model
{
    public class Klasse
    {
        public int DatenbankId { get; set; }
        public string Kuerzel { get; set; }
        public List<Schueler> Schueler
        {
            get
            {
                return null;
            }
        }

    }
}
