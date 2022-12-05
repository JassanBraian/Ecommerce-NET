namespace Presentacion.Core.Reserva
{
    partial class _00039_ABM_Reserva
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
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.nudSeña = new System.Windows.Forms.NumericUpDown();
            this.lblMotivoReserva = new System.Windows.Forms.Label();
            this.btnNuevoMotivoReserva = new System.Windows.Forms.Button();
            this.cmbMotivoReserva = new System.Windows.Forms.ComboBox();
            this.btnBuscarClientes = new System.Windows.Forms.Button();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblMesa = new System.Windows.Forms.Label();
            this.cmbMesa = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEstadoReserva = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeña)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(115, 181);
            this.txtCliente.MaxLength = 9;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(269, 20);
            this.txtCliente.TabIndex = 130;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(437, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 20);
            this.label5.TabIndex = 129;
            this.label5.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(512, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 20);
            this.label4.TabIndex = 128;
            this.label4.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(386, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 20);
            this.label1.TabIndex = 127;
            this.label1.Text = "*";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(411, 63);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(116, 13);
            this.label25.TabIndex = 126;
            this.label25.Text = "Campos Obligatorios (*)";
            // 
            // nudSeña
            // 
            this.nudSeña.DecimalPlaces = 2;
            this.nudSeña.Location = new System.Drawing.Point(115, 125);
            this.nudSeña.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.nudSeña.Name = "nudSeña";
            this.nudSeña.Size = new System.Drawing.Size(96, 20);
            this.nudSeña.TabIndex = 123;
            // 
            // lblMotivoReserva
            // 
            this.lblMotivoReserva.AutoSize = true;
            this.lblMotivoReserva.Location = new System.Drawing.Point(12, 210);
            this.lblMotivoReserva.Name = "lblMotivoReserva";
            this.lblMotivoReserva.Size = new System.Drawing.Size(97, 13);
            this.lblMotivoReserva.TabIndex = 122;
            this.lblMotivoReserva.Text = "Motivo de Reserva";
            // 
            // btnNuevoMotivoReserva
            // 
            this.btnNuevoMotivoReserva.Location = new System.Drawing.Point(390, 207);
            this.btnNuevoMotivoReserva.Name = "btnNuevoMotivoReserva";
            this.btnNuevoMotivoReserva.Size = new System.Drawing.Size(41, 21);
            this.btnNuevoMotivoReserva.TabIndex = 121;
            this.btnNuevoMotivoReserva.Text = "...";
            this.btnNuevoMotivoReserva.UseVisualStyleBackColor = true;
            this.btnNuevoMotivoReserva.Click += new System.EventHandler(this.btnNuevoMotivoReserva_Click_1);
            // 
            // cmbMotivoReserva
            // 
            this.cmbMotivoReserva.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMotivoReserva.FormattingEnabled = true;
            this.cmbMotivoReserva.Location = new System.Drawing.Point(115, 207);
            this.cmbMotivoReserva.Name = "cmbMotivoReserva";
            this.cmbMotivoReserva.Size = new System.Drawing.Size(269, 21);
            this.cmbMotivoReserva.TabIndex = 120;
            // 
            // btnBuscarClientes
            // 
            this.btnBuscarClientes.Location = new System.Drawing.Point(390, 181);
            this.btnBuscarClientes.Name = "btnBuscarClientes";
            this.btnBuscarClientes.Size = new System.Drawing.Size(113, 21);
            this.btnBuscarClientes.TabIndex = 119;
            this.btnBuscarClientes.Text = "Buscar Clientes";
            this.btnBuscarClientes.UseVisualStyleBackColor = true;
            this.btnBuscarClientes.Click += new System.EventHandler(this.btnBuscarClientes_Click_1);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(51, 184);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(58, 13);
            this.lblCliente.TabIndex = 118;
            this.lblCliente.Text = "Dni Cliente";
            // 
            // lblMesa
            // 
            this.lblMesa.AutoSize = true;
            this.lblMesa.Location = new System.Drawing.Point(76, 101);
            this.lblMesa.Name = "lblMesa";
            this.lblMesa.Size = new System.Drawing.Size(33, 13);
            this.lblMesa.TabIndex = 117;
            this.lblMesa.Text = "Mesa";
            // 
            // cmbMesa
            // 
            this.cmbMesa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMesa.FormattingEnabled = true;
            this.cmbMesa.Location = new System.Drawing.Point(115, 98);
            this.cmbMesa.Name = "cmbMesa";
            this.cmbMesa.Size = new System.Drawing.Size(269, 21);
            this.cmbMesa.TabIndex = 115;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(76, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 116;
            this.label6.Text = "Seña";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 132;
            this.label2.Text = "Estado de Reserva";
            // 
            // cmbEstadoReserva
            // 
            this.cmbEstadoReserva.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoReserva.FormattingEnabled = true;
            this.cmbEstadoReserva.Location = new System.Drawing.Point(115, 151);
            this.cmbEstadoReserva.Name = "cmbEstadoReserva";
            this.cmbEstadoReserva.Size = new System.Drawing.Size(152, 21);
            this.cmbEstadoReserva.TabIndex = 131;
            // 
            // _00039_ABM_Reserva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 265);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbEstadoReserva);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.nudSeña);
            this.Controls.Add(this.lblMotivoReserva);
            this.Controls.Add(this.btnNuevoMotivoReserva);
            this.Controls.Add(this.cmbMotivoReserva);
            this.Controls.Add(this.btnBuscarClientes);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.lblMesa);
            this.Controls.Add(this.cmbMesa);
            this.Controls.Add(this.label6);
            this.MaximumSize = new System.Drawing.Size(551, 304);
            this.MinimumSize = new System.Drawing.Size(551, 304);
            this.Name = "_00039_ABM_Reserva";
            this.Text = "Reserva (Alta, Baja y Modificacion)";
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cmbMesa, 0);
            this.Controls.SetChildIndex(this.lblMesa, 0);
            this.Controls.SetChildIndex(this.lblCliente, 0);
            this.Controls.SetChildIndex(this.btnBuscarClientes, 0);
            this.Controls.SetChildIndex(this.cmbMotivoReserva, 0);
            this.Controls.SetChildIndex(this.btnNuevoMotivoReserva, 0);
            this.Controls.SetChildIndex(this.lblMotivoReserva, 0);
            this.Controls.SetChildIndex(this.nudSeña, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtCliente, 0);
            this.Controls.SetChildIndex(this.cmbEstadoReserva, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeña)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.NumericUpDown nudSeña;
        private System.Windows.Forms.Label lblMotivoReserva;
        private System.Windows.Forms.Button btnNuevoMotivoReserva;
        private System.Windows.Forms.ComboBox cmbMotivoReserva;
        private System.Windows.Forms.Button btnBuscarClientes;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblMesa;
        private System.Windows.Forms.ComboBox cmbMesa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbEstadoReserva;
    }
}