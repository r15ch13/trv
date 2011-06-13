using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TR_Verwaltung
{
    class Lehrer
    {
        public int DatenbankId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Kuerzel { get; set; }
        public bool istAdmin { get; set; }

        public static bool Login(string benutzername, string passwort)
        {
            if(benutzername == "test") return true;
            return false;
        }

        public static List<Schueler> findByName(string name)
        {
            return null;
        }
    }
}
