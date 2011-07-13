using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Data;
using TR_Verwaltung.Sonstiges;
using System.Data.SqlServerCe;

namespace TR_Verwaltung.Model
{
    public class Schueler
    {
        public int DatenbankId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public Klasse Klasse { get; set; }

        public Schueler(int datenbankid, string vorname, string nachname, Klasse klasse)
        {
            DatenbankId = datenbankid;
            Vorname = vorname;
            Nachname = nachname;
            Klasse = klasse;
        }

        public Schueler(int datenbankid, Klasse klasse)
        {
            DatenbankId = datenbankid;
            Klasse = klasse;

            Dictionary<string, object> result = Database.executeRow(@"SELECT Vorname, Nachname FROM Schueler WHERE ID = {0}", datenbankid);
            if (result.Count == 2)
            {
                Vorname = Convert.ToString(result["Vorname"]);
                Nachname = Convert.ToString(result["Nachname"]);
            }
        }

        public static List<Schueler> findByName(string str)
        {
            return DemoData().Where(x =>
                                x.Nachname.ToLower().Contains(str) ||
                                x.Vorname.ToLower().Contains(str)).ToList();
        }
        public static List<Schueler> findByKlasse(string str)
        {
            return DemoData().Where(x => x.Klasse.ToString().ToLower().Contains(str)).ToList();
        }

        public static object Statistik()
        {
            return null;
        }

        public static List<Schueler> DemoData()
        {
            List<Schueler> schueler = new List<Schueler>();
            /*
            schueler.Add(new Schueler("Atiqullah", "Zeyarmal", "IFK 210"));
            schueler.Add(new Schueler("Cakmakci", "Ismail", "IFK 210"));
            schueler.Add(new Schueler("Demukaj", "Mehmet", "IFK 210"));
            schueler.Add(new Schueler("Gödderz", "Romina", "JoA 210"));
            schueler.Add(new Schueler("Noory", "Sabrine", "WKJ 210"));
            schueler.Add(new Schueler("Husic", "Elvisa", "BGG 210"));
            */
            return schueler;
        }

        public static List<Schueler> Testdaten()
        {
            string datei = @"..\..\..\Testschueler.csv";
            StreamReader sr = new StreamReader(datei, Encoding.Default);
            string text = sr.ReadToEnd();

            List<Schueler> Testschueler = new List<Schueler>();

            string[] lines = text.Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                //vorname, nachname, klasse
                //Schueler s = new Schueler(lines[i].Split(';')[0].Replace('"', ' ').Trim(), lines[i].Split(';')[1].Replace('"', ' ').Trim(), lines[i].Split(';')[2].Replace('"', ' ').Trim());
                //Testschueler.Add(s);
            }

            //Klassen und Schüler hinzufügen falls noch nicht vorhanden 
            foreach (Schueler typ in Testschueler)
            {
                if (Database.executeScalar<int>(String.Format("SELECT COUNT(ID) FROM Klasse WHERE Bezeichnung = '{0}'", typ.Klasse), 0) == 0)
                {
                    Database.executeNonQuery(String.Format("INSERT INTO Klasse (Bezeichnung) VALUES ('{0}')", typ.Klasse));
                }
                typ.DatenbankId = Database.InsertSchueler(typ);
            }


            return Testschueler;
        }
        /*
        public static Schueler neuerSchueler()
        {

        }
        */

        public static DataTable getSchueler()
        {
            DataTable dtResult = new DataTable();

            dtResult.Columns.Add("SchuelerID");
            dtResult.Columns.Add("Vorname");
            dtResult.Columns.Add("Nachname");
            dtResult.Columns.Add("Klassenbezeichnung");

            SqlCeDataReader sqlReader = Database.executeReader("SELECT S.ID, S.Vorname, S.Nachname, K.Bezeichnung FROM Schueler S JOIN Schuelerklasse SK ON S.ID = SK.SchuelerID JOIN Klasse K ON SK.KlasseID = K.ID");

            while (sqlReader.Read())
            {
                dtResult.Rows.Add(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(3));
            }
            
            return dtResult;
        }
    }
}
