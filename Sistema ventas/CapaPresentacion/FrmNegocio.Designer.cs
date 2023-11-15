namespace CapaPresentacion
{
    partial class FrmNegocio
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
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnguardarcambios = new FontAwesome.Sharp.IconButton();
            this.txtdireccion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtruc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtnombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnsubir = new FontAwesome.Sharp.IconButton();
            this.label2 = new System.Windows.Forms.Label();
            this.piclogo = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piclogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 482);
            this.label1.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.White;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(32, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(165, 24);
            this.label9.TabIndex = 19;
            this.label9.Text = "Detalle del negocio";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.btnguardarcambios);
            this.groupBox1.Controls.Add(this.txtdireccion);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtruc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtnombre);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnsubir);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.piclogo);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 248);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // btnguardarcambios
            // 
            this.btnguardarcambios.BackColor = System.Drawing.Color.Silver;
            this.btnguardarcambios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnguardarcambios.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnguardarcambios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnguardarcambios.ForeColor = System.Drawing.Color.Black;
            this.btnguardarcambios.IconChar = FontAwesome.Sharp.IconChar.Check;
            this.btnguardarcambios.IconColor = System.Drawing.Color.Black;
            this.btnguardarcambios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnguardarcambios.IconSize = 19;
            this.btnguardarcambios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnguardarcambios.Location = new System.Drawing.Point(211, 173);
            this.btnguardarcambios.Name = "btnguardarcambios";
            this.btnguardarcambios.Size = new System.Drawing.Size(196, 23);
            this.btnguardarcambios.TabIndex = 65;
            this.btnguardarcambios.Text = "Guardar cambios";
            this.btnguardarcambios.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnguardarcambios.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnguardarcambios.UseVisualStyleBackColor = false;
            this.btnguardarcambios.Click += new System.EventHandler(this.btnguardarcambios_Click);
            // 
            // txtdireccion
            // 
            this.txtdireccion.BackColor = System.Drawing.Color.White;
            this.txtdireccion.Location = new System.Drawing.Point(211, 147);
            this.txtdireccion.Name = "txtdireccion";
            this.txtdireccion.Size = new System.Drawing.Size(196, 20);
            this.txtdireccion.TabIndex = 64;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 63;
            this.label5.Text = "Direccion:";
            // 
            // txtruc
            // 
            this.txtruc.BackColor = System.Drawing.Color.White;
            this.txtruc.Location = new System.Drawing.Point(211, 92);
            this.txtruc.Name = "txtruc";
            this.txtruc.Size = new System.Drawing.Size(196, 20);
            this.txtruc.TabIndex = 62;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(208, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "R.U.C";
            // 
            // txtnombre
            // 
            this.txtnombre.BackColor = System.Drawing.Color.White;
            this.txtnombre.Location = new System.Drawing.Point(211, 36);
            this.txtnombre.Name = "txtnombre";
            this.txtnombre.Size = new System.Drawing.Size(196, 20);
            this.txtnombre.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(208, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 59;
            this.label3.Text = "Nombre Negocio:";
            // 
            // btnsubir
            // 
            this.btnsubir.BackColor = System.Drawing.Color.Silver;
            this.btnsubir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnsubir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnsubir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsubir.ForeColor = System.Drawing.Color.Black;
            this.btnsubir.IconChar = FontAwesome.Sharp.IconChar.Upload;
            this.btnsubir.IconColor = System.Drawing.Color.Black;
            this.btnsubir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnsubir.IconSize = 18;
            this.btnsubir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsubir.Location = new System.Drawing.Point(20, 163);
            this.btnsubir.Name = "btnsubir";
            this.btnsubir.Size = new System.Drawing.Size(145, 23);
            this.btnsubir.TabIndex = 58;
            this.btnsubir.Text = "Subir";
            this.btnsubir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnsubir.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnsubir.UseVisualStyleBackColor = false;
            this.btnsubir.Click += new System.EventHandler(this.btnsubir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Logo:";
            // 
            // piclogo
            // 
            this.piclogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.piclogo.Location = new System.Drawing.Point(20, 36);
            this.piclogo.Name = "piclogo";
            this.piclogo.Size = new System.Drawing.Size(145, 121);
            this.piclogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.piclogo.TabIndex = 0;
            this.piclogo.TabStop = false;
            // 
            // FrmNegocio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 482);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Name = "FrmNegocio";
            this.Text = "FrmNegocio";
            this.Load += new System.EventHandler(this.FrmNegocio_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.piclogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox piclogo;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnsubir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtnombre;
        private System.Windows.Forms.TextBox txtruc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtdireccion;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconButton btnguardarcambios;
    }
}