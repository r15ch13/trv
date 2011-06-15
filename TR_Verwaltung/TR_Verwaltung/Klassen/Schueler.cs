using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace TR_Verwaltung
{
    public class Schueler
    {
        private int DatenbankId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Klasse { get; set; }

        public Schueler(string vorname, string nachname, string klasse)
        {
            this.Vorname = vorname;
            this.Nachname = nachname;
            this.Klasse = klasse;
        }

        public static List<Schueler> findByName(string str)
        {
            return DemoData().Where(x =>
                                x.Nachname.ToLower().Contains(str) ||
                                x.Vorname.ToLower().Contains(str)).ToList();
        }
        public static List<Schueler> findByKlasse(string str)
        {
            return DemoData().Where(x => x.Klasse.ToLower().Contains(str)).ToList();
        }

        public static object Statistik()
        {
            return null;
        }

        public static List<Schueler> DemoData()
        {
            List<Schueler> schueler = new List<Schueler>();
            schueler.Add(new Schueler("Atiqullah", "Zeyarmal", "IFK 210"));
            schueler.Add(new Schueler("Cakmakci", "Ismail", "IFK 210"));
            schueler.Add(new Schueler("Demukaj", "Mehmet", "IFK 210"));
            schueler.Add(new Schueler("Gödderz", "Romina", "JoA 210"));
            schueler.Add(new Schueler("Noory", "Sabrine", "WKJ 210"));
            schueler.Add(new Schueler("Husic", "Elvisa", "BGG 210"));
            return schueler;
        }
    }
}
