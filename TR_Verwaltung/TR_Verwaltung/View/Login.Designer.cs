﻿namespace TR_Verwaltung.View
{
    partial class Login
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Passworteingabe = new System.Windows.Forms.TextBox();
            this.LehrerKuerzel = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Passwort *)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Kuerzel";
            // 
            // Passworteingabe
            // 
            this.Passworteingabe.Location = new System.Drawing.Point(110, 84);
            this.Passworteingabe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Passworteingabe.Name = "Passworteingabe";
            this.Passworteingabe.PasswordChar = '*';
            this.Passworteingabe.Size = new System.Drawing.Size(200, 25);
            this.Passworteingabe.TabIndex = 5;
            // 
            // LehrerKuerzel
            // 
            this.LehrerKuerzel.Location = new System.Drawing.Point(110, 41);
            this.LehrerKuerzel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LehrerKuerzel.Name = "LehrerKuerzel";
            this.LehrerKuerzel.Size = new System.Drawing.Size(200, 25);
            this.LehrerKuerzel.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(209, 135);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 41);
            this.button1.TabIndex = 3;
            this.button1.Text = "Einloggen";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 195);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Passworteingabe);
            this.Controls.Add(this.LehrerKuerzel);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Passworteingabe;
        private System.Windows.Forms.TextBox LehrerKuerzel;
        private System.Windows.Forms.Button button1;
    }
}