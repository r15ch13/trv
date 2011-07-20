using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TR_Verwaltung.Sonstiges;
using System.Data.SqlServerCe;

namespace TR_Verwaltung.Model
{
    public class Aufenthalt : Model
    {
        public Schueler Schueler { get; set; }
        public Klasse Klasse { get; set; }
        public Lehrer ULehrer { get; set; }
        public Lehrer TRLehrer { get; set; }
        public int Ueberweisungsnummer { get; set; }
        

        public override int Save()
        {
            throw new NotImplementedException();
        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
