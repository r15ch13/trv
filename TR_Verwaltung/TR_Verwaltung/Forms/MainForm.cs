using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TR_Verwaltung
{
    public partial class MainForm : Form
    {
        System.Random rnd = new System.Random(5);

        public MainForm()
        {
            InitializeComponent();
            panelLogin.BringToFront();
            panelLogin.Dock = DockStyle.Fill;
            PopulateSchueler();
        }

        private void eingabeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelLogin.BringToFront();
            panelLogin.Dock = DockStyle.Fill;
        }

        private void schuelererfassungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelErfassung.BringToFront();
            panelErfassung.Dock = DockStyle.Fill;
        }

        private void benutzereinstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelEinstellungen.BringToFront();
            panelEinstellungen.Dock = DockStyle.Fill;
        }

        class DummySchueler
        {
            public DummySchueler(Random rnd)
            {
                Name = String.Format("Schueller Name {0}", rnd.Next(100));
                Klasse = String.Format("IA{0}", rnd.Next(100));
            }
            public string Name { get; set; }
            public string Klasse { get; set; }
        }

        public void PopulateSchueler()
        {
            int itemcount = rnd.Next(10,30);

            BindingList<DummySchueler> schueler = new BindingList<DummySchueler>();

            for (int i = 0; i < itemcount ; i++)
            {
                schueler.Add(new DummySchueler(rnd));
            }

            BindingSource asd = new BindingSource();
            asd.DataSource = schueler;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = asd;
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            PopulateSchueler();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Lehrer.Login(textBox1.Text, textBox2.Text))
            {
                panelErfassung.BringToFront();
                panelErfassung.Dock = DockStyle.Fill;
            }
        }

    }
}
