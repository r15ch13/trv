using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TR_Verwaltung.Sonstiges;
using System.Diagnostics;

namespace TR_Verwaltung.Model
{
    public class Klasse
    {
        public int DatenbankId { get; set; }
        public string Bezeichnung { get; set; }

        public Klasse(int datenbankid, string bezeichnung)
        {
            DatenbankId = datenbankid;
            Bezeichnung = bezeichnung;
        }

        public int Save()
        {
            return Database.executeNonQuery(@"UPDATE Klasse SET Bezeichnung = '{0}' WHERE ID = {1}", new object[] { Bezeichnung, DatenbankId });
        }

        public List<Schueler> Schueler
        {
            get
            {
                //List<int> bla = Database.executeReader("SELECT SchuelerID FROM Schuelerklasse WHERE (Aktiv = 1) AND (KlasseID = 4)");
                return new List<Schueler>();
            }
        }

        public static Klasse GetByBezeichnung(string bezeichnung)
        {
            Dictionary<string, object> result = Database.executeRow(@"SELECT ID, Bezeichnung FROM Klasse WHERE Bezeichnung = '{0}'", new string[] { bezeichnung });
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

    }
}
