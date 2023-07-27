namespace Savas.Desktop
{
    partial class l2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(l2));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.surelabel = new System.Windows.Forms.Label();
            this.ucaksavarPanel = new System.Windows.Forms.Panel();
            this.SavasAlaniPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.surelabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1690, 61);
            this.panel1.TabIndex = 6;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(12, 12);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(51, 35);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 29;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click_1);
            // 
            // surelabel
            // 
            this.surelabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.surelabel.BackColor = System.Drawing.Color.Navy;
            this.surelabel.Font = new System.Drawing.Font("DS-Digital", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.surelabel.ForeColor = System.Drawing.Color.White;
            this.surelabel.Location = new System.Drawing.Point(1581, 9);
            this.surelabel.Name = "surelabel";
            this.surelabel.Size = new System.Drawing.Size(97, 45);
            this.surelabel.TabIndex = 0;
            this.surelabel.Text = "0:00";
            this.surelabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ucaksavarPanel
            // 
            this.ucaksavarPanel.BackColor = System.Drawing.Color.Tan;
            this.ucaksavarPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ucaksavarPanel.Location = new System.Drawing.Point(0, 779);
            this.ucaksavarPanel.Name = "ucaksavarPanel";
            this.ucaksavarPanel.Size = new System.Drawing.Size(1690, 32);
            this.ucaksavarPanel.TabIndex = 5;
            // 
            // SavasAlaniPanel
            // 
            this.SavasAlaniPanel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.SavasAlaniPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SavasAlaniPanel.Location = new System.Drawing.Point(0, 61);
            this.SavasAlaniPanel.Name = "SavasAlaniPanel";
            this.SavasAlaniPanel.Size = new System.Drawing.Size(1690, 718);
            this.SavasAlaniPanel.TabIndex = 7;
            // 
            // l2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1690, 811);
            this.Controls.Add(this.SavasAlaniPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucaksavarPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "l2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "orta";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.l2_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label surelabel;
        private System.Windows.Forms.Panel ucaksavarPanel;
        private System.Windows.Forms.Panel SavasAlaniPanel;
    }
}