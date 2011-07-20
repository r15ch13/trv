using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using TR_Verwaltung.Sonstiges;
using System.Diagnostics;

namespace TR_Verwaltung.Model
{
    public class Klasse : Model
    {
        public string Bezeichnung { get; set; }

        public Klasse(int datenbankid, string bezeichnung)
        {
            DatenbankId = datenbankid;
            Bezeichnung = bezeichnung;
        }

        public override int Save()
        {
            return Database.executeNonQuery(@"UPDATE Klasse SET Bezeichnung = '{0}' WHERE ID = {1}", Bezeichnung, DatenbankId);
        }

        public List<Schueler> Schueler
        {
            get
            {
                List<Schueler> listResult = new List<Schueler>();

                SqlCeDataReader sqlReader = Database.executeReader("SELECT SchuelerID FROM Schuelerklasse WHERE (Aktiv = 1) AND (KlasseID = {0})", DatenbankId);

                while (sqlReader.Read())
                {
                    listResult.Add(new Schueler(sqlReader.GetInt32(0), this));
                }

                return listResult;
            }
        }


        public static Klasse GetById(int datenbankid)
        {
            Dictionary<string, object> result = Database.executeRow(@"SELECT ID, Bezeichnung FROM Klasse WHERE ID = {0}", datenbankid);
            if (result.Count == 2) return new Klasse(Convert.ToInt32(result["ID"]), Convert.ToString(result["Bezeichnung"]));
            return null;
        }

        public static Klasse GetByBezeichnung(string bezeichnung)
        {
            Dictionary<string, object> result = Database.executeRow(@"SELECT ID, Bezeichnung FROM Klasse WHERE Bezeichnung = '{0}'", bezeichnung);
            if (result.Count == 2) return new Klasse(Convert.ToInt32(result["ID"]), Convert.ToString(result["Bezeichnung"]));
            return null;
        }

        public override string ToString()
        {
            return Bezeichnung;
        }

        public static Klasse Create(string bezeichnung)
        {
            if (bezeichnung == "") throw new ArgumentNullException();

            if (Database.executeScalar<int>(@"SELECT COUNT(ID) FROM Klasse WHERE Bezeichnung = '{0}'", -1, bezeichnung) == 0)
            {
                Database.executeNonQuery(@"INSERT INTO Klasse (Bezeichnung) VALUES ('{0}')", bezeichnung);
            }
            return GetByBezeichnung(bezeichnung);
        }

        public void AddSchueler(Schueler schueler)
        {
            if (schueler == null) throw new ArgumentNullException();

            if (Database.executeScalar<int>("SELECT COUNT(ID) FROM Schuelerklasse WHERE SchuelerID = {0} AND KlasseID = {1} AND Aktiv = 1", -1, schueler.DatenbankId, DatenbankId) == 0)
            {
                Database.executeNonQuery("INSERT INTO Schuelerklasse (SchuelerID, KlasseID, Datum) VALUES ({0}, {1}, GETDATE())", schueler.DatenbankId, DatenbankId);
            }
        }

        public void RemoveKlasse(Klasse klasse)
        {
            if (klasse == null) throw new ArgumentNullException();

            if (Database.executeScalar<int>("SELECT COUNT(SchuelerID) FROM Schuelerklasse WHERE SchuelerID = {0} AND KlasseID = {1} AND Aktiv = 1", -1, DatenbankId, klasse.DatenbankId) == 0)
            {
                Database.executeNonQuery("UPDATE Schuelerklasse SET Aktiv = 0 WHERE SchuelerID = {0} AND KlasseID = {1}", DatenbankId, klasse.DatenbankId);
            }
        }
    }
}
