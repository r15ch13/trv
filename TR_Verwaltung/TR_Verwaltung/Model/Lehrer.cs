using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TR_Verwaltung.Sonstiges;

namespace TR_Verwaltung.Model
{
    public class Lehrer
    {
        public int DatenbankId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Kuerzel { get; set; }
        public string Passwort { get; set; }
        public bool Admin { get; set; }

        public Lehrer(int datenbankid)
        {
            DatenbankId = datenbankid;

            Dictionary<string, object> result = Database.executeRow(@"SELECT Vorname, Nachname, Passwort, Kuerzel FROM Lehrer WHERE ID = {0}", datenbankid);
            if (result.Count == 2)
            {
                Vorname = Convert.ToString(result["Vorname"]);
                Nachname = Convert.ToString(result["Nachname"]);
                Kuerzel = Convert.ToString(result["Kuerzel"]);
                Passwort = Convert.ToString(result["Passwort"]);
                Admin = Convert.ToInt32(result["Admin"]) == 1 ? true : false;
            }
        }

        public int Save()
        {
            return Database.executeNonQuery(@"UPDATE Lehrer SET Vorname = '{0}', Nachname = '{1}', Kuerzel = '{2}', Passwort = '{3}', Admin = {4} WHERE ID = {5}", Vorname, Nachname, Kuerzel, Passwort, (Admin == true ? 1 : 0), DatenbankId);
        }

        public void AddKlasse(Klasse klasse)
        {
            if (klasse == null) throw new ArgumentNullException();

            if (Database.executeScalar<int>("SELECT COUNT(ID) FROM Lehrerklasse WHERE KlasseID = {0} AND LehrerID = {1}", -1, klasse.DatenbankId, DatenbankId) == 0)
            {
                Database.executeNonQuery("INSERT INTO Lehrerklasse (LehrerID, KlasseID, Datum) VALUES ({0}, {1}, GETDATE())", klasse.DatenbankId, DatenbankId);
            }
        }

        public List<Klasse> Klassen
        {
            get
            {
                List<Schueler> listResult = new List<Klasse>();

                SqlCeDataReader sqlReader = Database.executeReader("SELECT KlasseID FROM Lehrerklasse WHERE (Aktiv = 1) AND (LehrerID = {0})", DatenbankId);

                while (sqlReader.Read())
                {
                    listResult.Add(new Klasse(sqlReader.GetInt32(0), this));
                }

                return listResult;
            }
        }

        public static bool Login(string kuerzel, string passwort)
        {
            if (Database.executeScalar<int>("SELECT COUNT(ID) FROM Lehrer WHERE Kuerzel = '{0}' AND Passwort = '{1}'", -1, kuerzel, Utils.Crypto.SHA1.GetString(passwort, Encoding.Default)) == 1)
            {
                return true;
            }
            return false;
        }

        public static List<Schueler> findByName(string name)
        {
            return null;
        }
    }
}
