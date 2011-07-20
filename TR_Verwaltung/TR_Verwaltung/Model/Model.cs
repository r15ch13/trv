using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TR_Verwaltung.Model
{
    public abstract class Model
    {
        public int DatenbankId { get; set; }
        public abstract int Save();
        public abstract void Delete();
        public abstract override string ToString();
    }
}
