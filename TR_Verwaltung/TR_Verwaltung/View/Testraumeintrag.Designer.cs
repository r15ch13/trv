namespace TR_Verwaltung.View
{
    partial class Testraumeintrag
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Schuelereintragung = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Schülername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trv_dbDataSet = new TR_Verwaltung.bin.Debug.trv_dbDataSet();
            this.schuelerBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.schuelerTableAdapter = new TR_Verwaltung.bin.Debug.trv_dbDataSetTableAdapters.SchuelerTableAdapter();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Schuelereintragung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trv_dbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schuelerBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Schuelereintragung
            // 
            this.Schuelereintragung.Controls.Add(this.comboBox1);
            this.Schuelereintragung.Controls.Add(this.textBox3);
            this.Schuelereintragung.Controls.Add(this.textBox2);
            this.Schuelereintragung.Controls.Add(this.label7);
            this.Schuelereintragung.Controls.Add(this.label6);
            this.Schuelereintragung.Controls.Add(this.button1);
            this.Schuelereintragung.Controls.Add(this.label5);
            this.Schuelereintragung.Controls.Add(this.listBox1);
            this.Schuelereintragung.Controls.Add(this.Schülername);
            this.Schuelereintragung.Controls.Add(this.label1);
            this.Schuelereintragung.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Schuelereintragung.Location = new System.Drawing.Point(0, 0);
            this.Schuelereintragung.Name = "Schuelereintragung";
            this.Schuelereintragung.Size = new System.Drawing.Size(665, 387);
            this.Schuelereintragung.TabIndex = 0;
            this.Schuelereintragung.TabStop = false;
            this.Schuelereintragung.Text = "Schuelereintragung";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(498, 276);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 25);
            this.textBox3.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(498, 236);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 25);
            this.textBox2.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(402, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 19);
            this.label7.TabIndex = 12;
            this.label7.Text = "Uhrzeit";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(402, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 19);
            this.label6.TabIndex = 11;
            this.label6.Text = "Datum";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(498, 332);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "Schüler eintragen";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(313, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Überweisender Lehrer";
            // 
            // listBox1
            // 
            this.listBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedIndex", this.schuelerBindingSource, "ID", true));
            this.listBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.schuelerBindingSource, "ID", true));
            this.listBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.schuelerBindingSource, "ID", true));
            this.listBox1.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.schuelerBindingSource, "ID", true));
            this.listBox1.DataSource = this.schuelerBindingSource;
            this.listBox1.DisplayMember = "Vorname";
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(6, 24);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(301, 344);
            this.listBox1.TabIndex = 7;
            this.listBox1.ValueMember = "Nachname";
            // 
            // Schülername
            // 
            this.Schülername.Location = new System.Drawing.Point(385, 21);
            this.Schülername.Name = "Schülername";
            this.Schülername.Size = new System.Drawing.Size(264, 25);
            this.Schülername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(313, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schüler";
            // 
            // trv_dbDataSet
            // 
            this.trv_dbDataSet.DataSetName = "trv_dbDataSet";
            this.trv_dbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // schuelerBindingSource
            // 
            this.schuelerBindingSource.DataMember = "Schueler";
            this.schuelerBindingSource.DataSource = this.trv_dbDataSet;
            // 
            // schuelerTableAdapter
            // 
            this.schuelerTableAdapter.ClearBeforeFill = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(481, 86);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(168, 25);
            this.comboBox1.TabIndex = 13;
            // 
            // Testraumeintrag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Schuelereintragung);
            this.Name = "Testraumeintrag";
            this.Size = new System.Drawing.Size(665, 387);
            this.Schuelereintragung.ResumeLayout(false);
            this.Schuelereintragung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trv_dbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schuelerBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Schuelereintragung;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox Schülername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource schuelerBindingSource;
        private TR_Verwaltung.bin.Debug.trv_dbDataSet trv_dbDataSet;
        private TR_Verwaltung.bin.Debug.trv_dbDataSetTableAdapters.SchuelerTableAdapter schuelerTableAdapter;
        private System.Windows.Forms.ComboBox comboBox1;

    }
}
