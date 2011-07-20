using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TR_Verwaltung.Model;

namespace TR_Verwaltung.View
{
    public partial class Startseite : Form
    {
        public Startseite()
        {
            InitializeComponent();

            Klasse ia = Klasse.GetById(10);
            Klasse ae = Klasse.GetById(3);
            Lehrer wo = Lehrer.GetById(1);
            wo.AddKlasse(ia);
            wo.AddKlasse(ae);
            wo.RemoveKlasse(ia);
            List<Klasse> k = wo.Klassen;
        }

        public List<Schueler> Schueler { get; set; }

        private void schuelersuche1_SucheEnde(object sender, EventArgs e)
        {
            if (sender is Schuelersuche)
                Schueler = ((Schuelersuche)sender).Schueler;
        }
    }
}
