using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TR_Verwaltung.Sonstiges;
using System.Data.SqlServerCe;

namespace TR_Verwaltung.Model
{
    public class Lehrer : Model
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Kuerzel { get; set; }
        public string Passwort { get; set; }
        public bool Admin { get; set; }

        public Lehrer(int datenbankid, string vorname, string nachname, string kuerzel, string passwort, bool admin)
        {
            DatenbankId = datenbankid;
            Vorname = vorname;
            Nachname = nachname;
            Kuerzel = kuerzel;
            Passwort = passwort;
            Admin = admin;
        }

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

        public override int Save()
        {
            return Database.executeNonQuery(@"UPDATE Lehrer SET Vorname = '{0}', Nachname = '{1}', Kuerzel = '{2}', Passwort = '{3}', Admin = {4} WHERE ID = {5}", Vorname, Nachname, Kuerzel, Passwort, (Admin == true ? 1 : 0), DatenbankId);
        }

        public void AddKlasse(Klasse klasse)
        {
            if (klasse == null) throw new ArgumentNullException();

            if (Database.executeScalar<int>("SELECT COUNT(LehrerID) FROM Lehrerklasse WHERE LehrerID = {0} AND KlasseID = {1} AND Aktiv = 1", -1, DatenbankId, klasse.DatenbankId) == 0)
            {
                Database.executeNonQuery("INSERT INTO Lehrerklasse (LehrerID, KlasseID, Datum) VALUES ({0}, {1}, GETDATE())", DatenbankId, klasse.DatenbankId);
            }
        }

        public void RemoveKlasse(Klasse klasse)
        {
            if (klasse == null) throw new ArgumentNullException();

            if (Database.executeScalar<int>("SELECT COUNT(LehrerID) FROM Lehrerklasse WHERE LehrerID = {0} AND KlasseID = {1} AND Aktiv = 1", -1, DatenbankId, klasse.DatenbankId) == 1)
            {
                Database.executeNonQuery("UPDATE Lehrerklasse SET Aktiv = 0 WHERE LehrerID = {0} AND KlasseID = {1}", DatenbankId, klasse.DatenbankId);
            }
        }

        public List<Klasse> Klassen
        {
            get
            {
                List<Klasse> listResult = new List<Klasse>();

                SqlCeDataReader sqlReader = Database.executeReader("SELECT KlasseID FROM Lehrerklasse WHERE (Aktiv = 1) AND (LehrerID = {0})", DatenbankId);

                while (sqlReader.Read())
                {
                    listResult.Add(Klasse.GetById(sqlReader.GetInt32(0)));
                }

                return listResult;
            }
        }

        public static Lehrer Create(string vorname, string nachname, string kuerzel, string passwort)
        {
            return Create(vorname, nachname, kuerzel, passwort, false);
        }

        public static Lehrer Create(string vorname, string nachname, string kuerzel, string passwort, bool istAdmin)
        {
            if (vorname == "" || nachname == "" || kuerzel == "") throw new ArgumentNullException();
            
            if (Database.executeScalar<int>(@"SELECT COUNT(ID) FROM Lehrer WHERE Kuerzel = '{0}' OR (Vorname = '{1}' AND Nachname '{2}')", -1, kuerzel, vorname, nachname) == 0)
            {
                Database.executeNonQuery(@"INSERT INTO Lehrer (Vorname, Nachname, Kuerzel, Passwort, Admin) VALUES ('{0}')", vorname, nachname, kuerzel, Utils.Crypto.SHA1.GetString(passwort, Encoding.Default), (istAdmin == true ? 1 : 0));
            }
            return GetByKuerzel(kuerzel);
        }

        public static Lehrer GetById(int datenbankid)
        {
            Dictionary<string, object> result = Database.executeRow(@"SELECT ID, Vorname, Nachname, Kuerzel, Passwort, Admin FROM Lehrer WHERE ID = {0}", datenbankid);
            if (result.Count == 6) return new Lehrer(Convert.ToInt32(result["ID"]), Convert.ToString(result["Vorname"]), Convert.ToString(result["Nachname"]), Convert.ToString(result["Kuerzel"]), Convert.ToString(result["Passwort"]), (Convert.ToInt32(result["Admin"]) == 1 ? true : false));
            return null;
        }

        private static Lehrer GetByKuerzel(string kuerzel)
        {
            Dictionary<string, object> result = Database.executeRow(@"SELECT ID, Vorname, Nachname, Kuerzel, Passwort, Admin FROM Lehrer WHERE Kuerzel = '{0}'", kuerzel);
            if (result.Count == 6) return new Lehrer(Convert.ToInt32(result["ID"]), Convert.ToString(result["Vorname"]), Convert.ToString(result["Nachname"]), Convert.ToString(result["Kuerzel"]), Convert.ToString(result["Passwort"]), (Convert.ToInt32(result["Admin"]) == 1 ? true : false));
            return null;
        }

        public static bool Login(string kuerzel, string passwort)
        {
            if (kuerzel == "" || passwort == "") throw new ArgumentNullException();

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

        public override string ToString()
        {
            return String.Format("{0}, {1} ({2})", Nachname, Vorname, Kuerzel);
        }
    }
}
