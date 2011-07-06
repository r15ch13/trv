using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TR_Verwaltung.Sonstiges;

namespace TR_Verwaltung.Model
{
    public class Klasse
    {
        private int _DatenbankId;
        public int DatenbankId { get { return _DatenbankId; } set { _DatenbankId = value; } }
        public string Bezeichnung { get; set; }

        public Klasse(string bezeichnung)
        {
            this.Bezeichnung = bezeichnung;
            this.DatenbankId = GetIDByBezeichnung(this.Bezeichnung);
        }

        public List<Schueler> Schueler
        {
            get
            {
                return null;
            }
        }

        public static int GetIDByBezeichnung(string bez)
        {
            return Database.executeScalar<int>(String.Format("SELECT ID FROM Klasse WHERE Bezeichnung = '{0}'", bez), -1);
        }

    }
}
