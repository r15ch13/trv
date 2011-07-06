using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Data;
using TR_Verwaltung.Sonstiges;

namespace TR_Verwaltung.Model
{
    public class Schueler
    {
        public int DatenbankId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Klasse { get; set; }

        public Schueler(string vorname, string nachname, string klasse)
        {
            this.DatenbankId = -1;
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

        public static List<Schueler> Testdaten()
        {
            string datei = @"C:\Dokumente und Einstellungen\eppel_marcel\Desktop\trv\TR_Verwaltung\Testschueler.csv";
            StreamReader sr = new StreamReader(datei,Encoding.Default);
            string text = sr.ReadToEnd();

            List<Schueler> Testschueler = new List<Schueler>();

            string[] lines = text.Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                //vorname, nachname, klasse
                Schueler s = new Schueler(lines[i].Split(';')[0].Replace('"', ' ').Trim(), lines[i].Split(';')[1].Replace('"', ' ').Trim(), lines[i].Split(';')[2].Replace('"', ' ').Trim());
                Testschueler.Add(s);
            }
            
            //Klassen und Schüler hinzufügen falls noch nicht vorhanden 
            foreach(Schueler typ in Testschueler)
            {
                if (Database.executeScalar<int>(String.Format("SELECT COUNT(ID) FROM Klasse WHERE Bezeichnung = '{0}'", typ.Klasse), 0) == 0)
                {
                    Database.executeNonQuery(String.Format("INSERT INTO Klasse (Bezeichnung) VALUES ('{0}')", typ.Klasse));
                }
                typ.DatenbankId = Database.InsertSchueler(typ);
            }


            return Testschueler;
        }
    }
}
