namespace Presentacion.Core.Caja
{
    partial class _00042_AbrirCaja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_00042_AbrirCaja));
            this.BtnAbrirCaja = new System.Windows.Forms.Button();
            this.nudMontoSistema = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudCierre = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMontoApertura = new System.Windows.Forms.NumericUpDown();
            this.dtpFechaApertura = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoSistema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCierre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoApertura)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAbrirCaja
            // 
            this.BtnAbrirCaja.BackColor = System.Drawing.Color.LightGray;
            this.BtnAbrirCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAbrirCaja.Location = new System.Drawing.Point(227, 210);
            this.BtnAbrirCaja.Name = "BtnAbrirCaja";
            this.BtnAbrirCaja.Size = new System.Drawing.Size(158, 40);
            this.BtnAbrirCaja.TabIndex = 27;
            this.BtnAbrirCaja.Text = "Abrir Caja";
            this.BtnAbrirCaja.UseVisualStyleBackColor = false;
            this.BtnAbrirCaja.Click += new System.EventHandler(this.BtnAbrirCerrar_Click_1);
            // 
            // nudMontoSistema
            // 
            this.nudMontoSistema.DecimalPlaces = 2;
            this.nudMontoSistema.Enabled = false;
            this.nudMontoSistema.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMontoSistema.Location = new System.Drawing.Point(168, 168);
            this.nudMontoSistema.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.nudMontoSistema.Minimum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            -2147483648});
            this.nudMontoSistema.Name = "nudMontoSistema";
            this.nudMontoSistema.Size = new System.Drawing.Size(291, 26);
            this.nudMontoSistema.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(38, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 20);
            this.label5.TabIndex = 25;
            this.label5.Text = "Monto Sistema";
            // 
            // nudCierre
            // 
            this.nudCierre.DecimalPlaces = 2;
            this.nudCierre.Enabled = false;
            this.nudCierre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCierre.Location = new System.Drawing.Point(168, 124);
            this.nudCierre.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.nudCierre.Minimum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            -2147483648});
            this.nudCierre.Name = "nudCierre";
            this.nudCierre.Size = new System.Drawing.Size(291, 26);
            this.nudCierre.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(54, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "Monto Cierre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Monto Apertura";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "Fecha de Apertura";
            // 
            // nudMontoApertura
            // 
            this.nudMontoApertura.DecimalPlaces = 2;
            this.nudMontoApertura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMontoApertura.Location = new System.Drawing.Point(168, 88);
            this.nudMontoApertura.Maximum = new decimal(new int[] {
            1874919423,
            2328306,
            0,
            0});
            this.nudMontoApertura.Minimum = new decimal(new int[] {
            1241513983,
            370409800,
            542101,
            -2147483648});
            this.nudMontoApertura.Name = "nudMontoApertura";
            this.nudMontoApertura.Size = new System.Drawing.Size(291, 26);
            this.nudMontoApertura.TabIndex = 19;
            this.nudMontoApertura.ValueChanged += new System.EventHandler(this.nudMontoApertura_ValueChanged_1);
            // 
            // dtpFechaApertura
            // 
            this.dtpFechaApertura.Enabled = false;
            this.dtpFechaApertura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaApertura.Location = new System.Drawing.Point(168, 40);
            this.dtpFechaApertura.Name = "dtpFechaApertura";
            this.dtpFechaApertura.Size = new System.Drawing.Size(291, 26);
            this.dtpFechaApertura.TabIndex = 17;
            // 
            // _00042_AbrirCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(518, 294);
            this.Controls.Add(this.BtnAbrirCaja);
            this.Controls.Add(this.nudMontoSistema);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudCierre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudMontoApertura);
            this.Controls.Add(this.dtpFechaApertura);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(534, 333);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(534, 333);
            this.Name = "_00042_AbrirCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Abrir Caja";
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoSistema)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCierre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoApertura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnAbrirCaja;
        private System.Windows.Forms.NumericUpDown nudMontoSistema;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudCierre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMontoApertura;
        private System.Windows.Forms.DateTimePicker dtpFechaApertura;
    }
}