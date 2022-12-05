namespace Presentacion.Core.Mesa
{
    partial class _00014_ABM_Mesas
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSalon = new System.Windows.Forms.ComboBox();
            this.BtnAgregarNuevoSalon = new System.Windows.Forms.Button();
            this.nudNumeroMesa = new System.Windows.Forms.NumericUpDown();
            this.txtDescipcion = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumeroMesa)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Numero";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Descripcion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(56, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Salon";
            // 
            // cmbSalon
            // 
            this.cmbSalon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSalon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSalon.FormattingEnabled = true;
            this.cmbSalon.Location = new System.Drawing.Point(101, 168);
            this.cmbSalon.Name = "cmbSalon";
            this.cmbSalon.Size = new System.Drawing.Size(294, 23);
            this.cmbSalon.TabIndex = 7;
            // 
            // BtnAgregarNuevoSalon
            // 
            this.BtnAgregarNuevoSalon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgregarNuevoSalon.Location = new System.Drawing.Point(422, 168);
            this.BtnAgregarNuevoSalon.Name = "BtnAgregarNuevoSalon";
            this.BtnAgregarNuevoSalon.Size = new System.Drawing.Size(59, 23);
            this.BtnAgregarNuevoSalon.TabIndex = 8;
            this.BtnAgregarNuevoSalon.Text = "...";
            this.BtnAgregarNuevoSalon.UseVisualStyleBackColor = true;
            this.BtnAgregarNuevoSalon.Click += new System.EventHandler(this.BtnAgregarNuevoSalon_Click);
            // 
            // nudNumeroMesa
            // 
            this.nudNumeroMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudNumeroMesa.Location = new System.Drawing.Point(101, 93);
            this.nudNumeroMesa.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudNumeroMesa.MaximumSize = new System.Drawing.Size(101, 0);
            this.nudNumeroMesa.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumeroMesa.MinimumSize = new System.Drawing.Size(101, 0);
            this.nudNumeroMesa.Name = "nudNumeroMesa";
            this.nudNumeroMesa.Size = new System.Drawing.Size(101, 21);
            this.nudNumeroMesa.TabIndex = 9;
            this.nudNumeroMesa.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtDescipcion
            // 
            this.txtDescipcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescipcion.Location = new System.Drawing.Point(101, 132);
            this.txtDescipcion.MaxLength = 25;
            this.txtDescipcion.Name = "txtDescipcion";
            this.txtDescipcion.Size = new System.Drawing.Size(380, 21);
            this.txtDescipcion.TabIndex = 10;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(407, 60);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(116, 13);
            this.label25.TabIndex = 80;
            this.label25.Text = "Campos Obligatorios (*)";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(487, 132);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 20);
            this.label21.TabIndex = 81;
            this.label21.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(401, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 20);
            this.label4.TabIndex = 82;
            this.label4.Text = "*";
            // 
            // _00014_ABM_Mesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 234);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.txtDescipcion);
            this.Controls.Add(this.nudNumeroMesa);
            this.Controls.Add(this.BtnAgregarNuevoSalon);
            this.Controls.Add(this.cmbSalon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "_00014_ABM_Mesas";
            this.Text = "Mesa (Alta, Baja y Modificación)";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbSalon, 0);
            this.Controls.SetChildIndex(this.BtnAgregarNuevoSalon, 0);
            this.Controls.SetChildIndex(this.nudNumeroMesa, 0);
            this.Controls.SetChildIndex(this.txtDescipcion, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumeroMesa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSalon;
        private System.Windows.Forms.Button BtnAgregarNuevoSalon;
        private System.Windows.Forms.NumericUpDown nudNumeroMesa;
        private System.Windows.Forms.TextBox txtDescipcion;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label4;
    }
}