using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TR_Verwaltung
{
    class Schueler
    {
        public int DatenbankId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Klasse { get; set; }

        public static List<Schueler> findByName(string name)
        {
            return null;
        }
        public static List<Schueler> findByKlasse(string klasse)
        {
            return null;
        }

        public static object Statistik()
        {
            return null;
        }
    }
}
