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

        public int Save()
        {
            return Database.executeNonQuery(@"UPDATE Schueler SET Vorname = '{0}', Nachname = '{1}' WHERE ID = {2}", Vorname, Nachname, DatenbankId);
        }

        /*
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
