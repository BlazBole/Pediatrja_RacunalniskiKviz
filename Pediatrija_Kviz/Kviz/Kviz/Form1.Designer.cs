namespace Kviz
{
    partial class Form1
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
            this.lblVprasanje = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNaprej = new System.Windows.Forms.Button();
            this.btnPreveri = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblVprasanje
            // 
            this.lblVprasanje.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblVprasanje.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVprasanje.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVprasanje.Location = new System.Drawing.Point(8, 37);
            this.lblVprasanje.Name = "lblVprasanje";
            this.lblVprasanje.Size = new System.Drawing.Size(1161, 101);
            this.lblVprasanje.TabIndex = 0;
            this.lblVprasanje.Text = "Vprašanje";
            this.lblVprasanje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Menu;
            this.panel1.BackgroundImage = global::Kviz.Properties.Resources.labelBackground;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(8, 156);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1161, 381);
            this.panel1.TabIndex = 1;
            // 
            // btnNaprej
            // 
            this.btnNaprej.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNaprej.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnNaprej.Location = new System.Drawing.Point(1055, 543);
            this.btnNaprej.Name = "btnNaprej";
            this.btnNaprej.Size = new System.Drawing.Size(114, 51);
            this.btnNaprej.TabIndex = 2;
            this.btnNaprej.Text = "Naprej";
            this.btnNaprej.UseVisualStyleBackColor = false;
            this.btnNaprej.Click += new System.EventHandler(this.btnNaprej_Click);
            // 
            // btnPreveri
            // 
            this.btnPreveri.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPreveri.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPreveri.Location = new System.Drawing.Point(528, 543);
            this.btnPreveri.Name = "btnPreveri";
            this.btnPreveri.Size = new System.Drawing.Size(164, 51);
            this.btnPreveri.TabIndex = 3;
            this.btnPreveri.Text = "Preveri";
            this.btnPreveri.UseVisualStyleBackColor = false;
            this.btnPreveri.Click += new System.EventHandler(this.btnPreveri_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1052, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.BackgroundImage = global::Kviz.Properties.Resources.backgroundImage;
            this.ClientSize = new System.Drawing.Size(1181, 606);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPreveri);
            this.Controls.Add(this.btnNaprej);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblVprasanje);
            this.Name = "Form1";
            this.Text = "Vprasanja za izpit";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVprasanje;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNaprej;
        private System.Windows.Forms.Button btnPreveri;
        private System.Windows.Forms.Label label1;
    }
}

