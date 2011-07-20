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
    public class Schueler : Model
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public Klasse Klasse { get; set; }

        #region "Constructor"
        public Schueler(int datenbankid, string vorname, string nachname, Klasse klasse)
        {
            DatenbankId = datenbankid;
            Vorname = vorname;
            Nachname = nachname;
            Klasse = klasse;
        }

        public Schueler(int datenbankid, string vorname, string nachname)
        {
            DatenbankId = datenbankid;
            Vorname = vorname;
            Nachname = nachname;
        }

        public Schueler(int datenbankid)
        {
            DatenbankId = datenbankid;

            Dictionary<string, object> result = Database.executeRow(@"SELECT  S.Vorname, S.Nachname, SK.KlasseID FROM Schueler AS S INNER JOIN Schuelerklasse AS SK ON S.ID = SK.SchuelerID WHERE (S.ID = {0}) AND (SK.Aktiv = 1) AND (S.Aktiv = 1)", datenbankid);
            if (result.Count == 3)
            {
                Vorname = Convert.ToString(result["Vorname"]);
                Nachname = Convert.ToString(result["Nachname"]);
                if (result["KlasseID"] != null) Klasse = Klasse.GetById(Convert.ToInt32(result["KlasseID"]));
            }
        }
        public Schueler(int datenbankid, Klasse klasse)
        {
            DatenbankId = datenbankid;
            Klasse = klasse;

            Dictionary<string, object> result = Database.executeRow(@"SELECT  S.Vorname, S.Nachname, SK.KlasseID FROM Schueler AS S INNER JOIN Schuelerklasse AS SK ON S.ID = SK.SchuelerID WHERE (S.ID = {0}) AND (SK.Aktiv = 1) AND (S.Aktiv = 1)", datenbankid);
            if (result.Count == 3)
            {
                Vorname = Convert.ToString(result["Vorname"]);
                Nachname = Convert.ToString(result["Nachname"]);
            }
        }
        #endregion

        #region "override"
        public override int Save()
        {
            return Database.executeNonQuery(@"UPDATE Schueler SET Vorname = '{0}', Nachname = '{1}' WHERE ID = {2}", Vorname, Nachname, DatenbankId);
        }

        public override void Delete()
        {
            Database.executeNonQuery(@"UPDATE Schueler SET Aktiv = 0 WHERE ID = {0}", DatenbankId);
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}", Nachname, Vorname);
        }
        #endregion

        #region "Static"
        public static Schueler Create(string vorname, string nachname)
        {
            if (vorname.Trim() == "" || nachname.Trim() == "") throw new ArgumentNullException();

            if (Database.executeScalar<int>(@"SELECT COUNT(ID) FROM Schueler WHERE Vorname = '{0}' AND Nachname = '{1}' AND Aktiv = 1", -1, vorname.Trim(), nachname.Trim()) == 0)
            {
                Database.executeNonQuery(@"INSERT INTO Schueler (Vorname, Nachname) VALUES ('{0}', '{1}')", vorname.Trim(), nachname.Trim());
            }

            int bla = Database.executeScalar<int>(@"SELECT ID AS LastID FROM Persons WHERE ID = @@Identity", -1);
            return GetByName(vorname, nachname);
        }

        public static Schueler GetByName(string vorname, string nachname)
        {
            Dictionary<string, object> result = Database.executeRow(@"SELECT ID, Vorname, Nachname FROM Schueler WHERE Vorname = '{0}' AND Nachname = '{1}' AND Aktiv = 1", vorname, nachname);
            if (result.Count == 3) return new Schueler(Convert.ToInt32(result["ID"]), Convert.ToString(result["Vorname"]), Convert.ToString(result["Nachname"]));
            return null;
        }

        public static DataTable GetSchueler()
        {
            DataTable dtResult = new DataTable();

            dtResult.Columns.Add("SchuelerID");
            dtResult.Columns.Add("Vorname");
            dtResult.Columns.Add("Nachname");
            dtResult.Columns.Add("Klassenbezeichnung");

            SqlCeDataReader sqlReader = Database.executeReader("SELECT S.SchuelerID, S.Vorname, S.Nachname, K.Bezeichnung FROM Schueler S JOIN Schuelerklasse SK ON S.SchuelerID = SK.SchuelerID JOIN Klasse K ON SK.KlasseID = K.ID");

            while (sqlReader.Read())
            {
                dtResult.Rows.Add(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(3));
            }

            return dtResult;
        }

        public static DataTable GetSchuelerImRaum()
        {
            DataTable dtResult = new DataTable();

            dtResult.Columns.Add("SchuelerID");
            dtResult.Columns.Add("Vorname");
            dtResult.Columns.Add("Nachname");
            dtResult.Columns.Add("Klassenbezeichnung");

            SqlCeDataReader sqlReader = Database.executeReader("SELECT S.SchuelerID, S.Vorname, S.Nachname, K.Bezeichnung FROM Schueler S JOIN Schuelerklasse SK ON S.SchuelerID = SK.SchuelerID JOIN Klasse K ON SK.KlasseID = K.ID");

            while (sqlReader.Read())
            {
                dtResult.Rows.Add(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(3));
            }

            return dtResult;
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
        #endregion


        private Klasse GetKlasse()
        {
            return null;
        }
    }
}
