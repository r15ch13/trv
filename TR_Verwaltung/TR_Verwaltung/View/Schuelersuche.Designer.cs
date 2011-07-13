namespace TR_Verwaltung.View
{
    partial class Schuelersuche
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonSuchen = new System.Windows.Forms.Button();
            this.textBoxSuche = new System.Windows.Forms.TextBox();
            this.labelSchuelerSuchen = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSuchen
            // 
            this.buttonSuchen.Location = new System.Drawing.Point(324, 22);
            this.buttonSuchen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonSuchen.Name = "buttonSuchen";
            this.buttonSuchen.Size = new System.Drawing.Size(75, 28);
            this.buttonSuchen.TabIndex = 41;
            this.buttonSuchen.Text = "Suchen";
            this.buttonSuchen.UseVisualStyleBackColor = true;
            this.buttonSuchen.Click += new System.EventHandler(this.buttonSuchen_Click);
            // 
            // textBoxSuche
            // 
            this.textBoxSuche.Location = new System.Drawing.Point(3, 24);
            this.textBoxSuche.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxSuche.Name = "textBoxSuche";
            this.textBoxSuche.Size = new System.Drawing.Size(315, 25);
            this.textBoxSuche.TabIndex = 39;
            this.textBoxSuche.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSuche_KeyDown);
            // 
            // labelSchuelerSuchen
            // 
            this.labelSchuelerSuchen.AutoSize = true;
            this.labelSchuelerSuchen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSchuelerSuchen.Location = new System.Drawing.Point(3, 0);
            this.labelSchuelerSuchen.Name = "labelSchuelerSuchen";
            this.labelSchuelerSuchen.Size = new System.Drawing.Size(100, 19);
            this.labelSchuelerSuchen.TabIndex = 40;
            this.labelSchuelerSuchen.Text = "Schüler suchen";
            // 
            // Schuelersuche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.buttonSuchen);
            this.Controls.Add(this.textBoxSuche);
            this.Controls.Add(this.labelSchuelerSuchen);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Schuelersuche";
            this.Size = new System.Drawing.Size(402, 54);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSuchen;
        private System.Windows.Forms.TextBox textBoxSuche;
        private System.Windows.Forms.Label labelSchuelerSuchen;

    }
}
