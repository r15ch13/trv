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
    public partial class TestSuche : Form
    {
        private search s = new search();

        public TestSuche()
        {
            InitializeComponent();
        }

        private void TestSuche_Load(object sender, EventArgs e)
        {
            
            
        }

        private void cmdSuchen_Click(object sender, EventArgs e)
        {
            DataTable res = s.searchstr(txtVorname.Text, txtNachname.Text, txtKlasse.Text);
            dgvResults.DataSource = res;
        }
    }
}
