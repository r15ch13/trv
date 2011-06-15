﻿using System;
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
        public MainForm()
        {
            InitializeComponent();
            panelLogin.BringToFront();
            panelLogin.Dock = DockStyle.Fill;
            PopulateSchueler(Schueler.DemoData());
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

        public void PopulateSchueler(List<Schueler> data)
        {
            BindingList<Schueler> bl = new BindingList<Schueler>(data);
            BindingSource bs = new BindingSource();
            bs.DataSource = bl;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = bs;
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            List<Schueler> data = Schueler.findByName(textBox3.Text);
            PopulateSchueler(data);
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
