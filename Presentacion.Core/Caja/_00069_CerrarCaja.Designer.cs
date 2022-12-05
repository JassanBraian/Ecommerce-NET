namespace Presentacion.Core.Caja
{
    partial class _00069_CerrarCaja
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
            this.BtnCerrarCaja = new System.Windows.Forms.Button();
            this.nudMontoSistema = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudCierre = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nudMontoApertura = new System.Windows.Forms.NumericUpDown();
            this.dtpFechaCierre = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaApertura = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoSistema)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCierre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoApertura)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnCerrarCaja
            // 
            this.BtnCerrarCaja.BackColor = System.Drawing.Color.LightGray;
            this.BtnCerrarCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCerrarCaja.Location = new System.Drawing.Point(227, 240);
            this.BtnCerrarCaja.Name = "BtnCerrarCaja";
            this.BtnCerrarCaja.Size = new System.Drawing.Size(158, 40);
            this.BtnCerrarCaja.TabIndex = 38;
            this.BtnCerrarCaja.Text = "Cerrar Caja";
            this.BtnCerrarCaja.UseVisualStyleBackColor = false;
            this.BtnCerrarCaja.Click += new System.EventHandler(this.BtnCerrarCaja_Click);
            // 
            // nudMontoSistema
            // 
            this.nudMontoSistema.DecimalPlaces = 2;
            this.nudMontoSistema.Enabled = false;
            this.nudMontoSistema.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMontoSistema.Location = new System.Drawing.Point(168, 196);
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
            this.nudMontoSistema.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(38, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "Monto Sistema";
            // 
            // nudCierre
            // 
            this.nudCierre.DecimalPlaces = 2;
            this.nudCierre.Enabled = false;
            this.nudCierre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCierre.Location = new System.Drawing.Point(168, 152);
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
            this.nudCierre.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(54, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 34;
            this.label4.Text = "Monto Cierre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 33;
            this.label3.Text = "Monto Apertura";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 20);
            this.label2.TabIndex = 32;
            this.label2.Text = "Fecha de Cierre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 31;
            this.label1.Text = "Fecha de Apertura";
            // 
            // nudMontoApertura
            // 
            this.nudMontoApertura.DecimalPlaces = 2;
            this.nudMontoApertura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMontoApertura.Location = new System.Drawing.Point(168, 116);
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
            this.nudMontoApertura.TabIndex = 30;
            // 
            // dtpFechaCierre
            // 
            this.dtpFechaCierre.Enabled = false;
            this.dtpFechaCierre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaCierre.Location = new System.Drawing.Point(168, 61);
            this.dtpFechaCierre.Name = "dtpFechaCierre";
            this.dtpFechaCierre.Size = new System.Drawing.Size(291, 26);
            this.dtpFechaCierre.TabIndex = 29;
            // 
            // dtpFechaApertura
            // 
            this.dtpFechaApertura.Enabled = false;
            this.dtpFechaApertura.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaApertura.Location = new System.Drawing.Point(168, 18);
            this.dtpFechaApertura.Name = "dtpFechaApertura";
            this.dtpFechaApertura.Size = new System.Drawing.Size(291, 26);
            this.dtpFechaApertura.TabIndex = 28;
            // 
            // _00069_CerrarCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OrangeRed;
            this.ClientSize = new System.Drawing.Size(495, 307);
            this.Controls.Add(this.BtnCerrarCaja);
            this.Controls.Add(this.nudMontoSistema);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudCierre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudMontoApertura);
            this.Controls.Add(this.dtpFechaCierre);
            this.Controls.Add(this.dtpFechaApertura);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(511, 346);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(511, 346);
            this.Name = "_00069_CerrarCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cerrar Caja";
            this.Load += new System.EventHandler(this._00069_CerrarCaja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoSistema)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCierre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMontoApertura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnCerrarCaja;
        private System.Windows.Forms.NumericUpDown nudMontoSistema;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudCierre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMontoApertura;
        private System.Windows.Forms.DateTimePicker dtpFechaCierre;
        private System.Windows.Forms.DateTimePicker dtpFechaApertura;
    }
}