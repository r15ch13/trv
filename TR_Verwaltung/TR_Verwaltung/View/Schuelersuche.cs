using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TR_Verwaltung.Model;

namespace TR_Verwaltung.View
{
    public delegate void SucheEventHandler(object sender, EventArgs e);

    public partial class Schuelersuche : UserControl
    {
        public List<Schueler> Schueler { get; set; }

        public event SucheEventHandler SucheEnde;

        protected virtual void OnSucheEnde(EventArgs e)
        {
            if (SucheEnde != null)
                SucheEnde(this, e);
        }

        public Schuelersuche()
        {
            InitializeComponent();
        }

        private void buttonSuchen_Click(object sender, EventArgs e)
        {
            if (textBoxSuche.Text.Trim() != "")
                SucheSchueler(textBoxSuche.Text);
        }

        private void SucheSchueler(string str)
        {
            // Dummy bis die richtige Suche da ist
            // Schueler.Suche();
            // Ergebnis in der Schueler Property speichern
            Schueler = new List<Schueler>();
            OnSucheEnde(EventArgs.Empty);
        }

        private void textBoxSuche_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBoxSuche.Text.Trim() != "")
                SucheSchueler(textBoxSuche.Text);
        }

        

        
    }
}
