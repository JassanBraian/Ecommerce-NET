namespace Presentacion.Core.Precio
{
    partial class _00032_ABM_Precio
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
            this.cmbArticulo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudPrecioPublico = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudPrecioCosto = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbListaPrecios = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaActualizacion = new System.Windows.Forms.DateTimePicker();
            this.BtnNuevoArticulo = new System.Windows.Forms.Button();
            this.BtnNuevaListaPrecio = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.BtnCalcular = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioPublico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioCosto)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbArticulo
            // 
            this.cmbArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArticulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbArticulo.FormattingEnabled = true;
            this.cmbArticulo.Location = new System.Drawing.Point(133, 120);
            this.cmbArticulo.Name = "cmbArticulo";
            this.cmbArticulo.Size = new System.Drawing.Size(338, 24);
            this.cmbArticulo.TabIndex = 164;
            this.cmbArticulo.SelectedIndexChanged += new System.EventHandler(this.CmbArticulo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(344, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 161;
            this.label3.Text = "Precio Publico";
            // 
            // nudPrecioPublico
            // 
            this.nudPrecioPublico.DecimalPlaces = 2;
            this.nudPrecioPublico.Location = new System.Drawing.Point(436, 188);
            this.nudPrecioPublico.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.nudPrecioPublico.Name = "nudPrecioPublico";
            this.nudPrecioPublico.Size = new System.Drawing.Size(106, 20);
            this.nudPrecioPublico.TabIndex = 160;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(21, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 15);
            this.label6.TabIndex = 159;
            this.label6.Text = "Precio Costo";
            // 
            // nudPrecioCosto
            // 
            this.nudPrecioCosto.DecimalPlaces = 2;
            this.nudPrecioCosto.Enabled = false;
            this.nudPrecioCosto.Location = new System.Drawing.Point(103, 188);
            this.nudPrecioCosto.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.nudPrecioCosto.Name = "nudPrecioCosto";
            this.nudPrecioCosto.Size = new System.Drawing.Size(120, 20);
            this.nudPrecioCosto.TabIndex = 158;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(39, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 157;
            this.label2.Text = "Lista de Precio";
            // 
            // cmbListaPrecios
            // 
            this.cmbListaPrecios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListaPrecios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbListaPrecios.FormattingEnabled = true;
            this.cmbListaPrecios.Location = new System.Drawing.Point(133, 152);
            this.cmbListaPrecios.Name = "cmbListaPrecios";
            this.cmbListaPrecios.Size = new System.Drawing.Size(338, 24);
            this.cmbListaPrecios.TabIndex = 156;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(80, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 155;
            this.label1.Text = "Articulo";
            // 
            // dtpFechaActualizacion
            // 
            this.dtpFechaActualizacion.Enabled = false;
            this.dtpFechaActualizacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaActualizacion.Location = new System.Drawing.Point(55, 58);
            this.dtpFechaActualizacion.Name = "dtpFechaActualizacion";
            this.dtpFechaActualizacion.Size = new System.Drawing.Size(82, 20);
            this.dtpFechaActualizacion.TabIndex = 153;
            this.dtpFechaActualizacion.Visible = false;
            // 
            // BtnNuevoArticulo
            // 
            this.BtnNuevoArticulo.Location = new System.Drawing.Point(477, 121);
            this.BtnNuevoArticulo.Name = "BtnNuevoArticulo";
            this.BtnNuevoArticulo.Size = new System.Drawing.Size(36, 21);
            this.BtnNuevoArticulo.TabIndex = 165;
            this.BtnNuevoArticulo.Text = "...";
            this.BtnNuevoArticulo.UseVisualStyleBackColor = true;
            this.BtnNuevoArticulo.Click += new System.EventHandler(this.BtnNuevoArticulo_Click);
            // 
            // BtnNuevaListaPrecio
            // 
            this.BtnNuevaListaPrecio.Location = new System.Drawing.Point(477, 155);
            this.BtnNuevaListaPrecio.Name = "BtnNuevaListaPrecio";
            this.BtnNuevaListaPrecio.Size = new System.Drawing.Size(36, 21);
            this.BtnNuevaListaPrecio.TabIndex = 166;
            this.BtnNuevaListaPrecio.Text = "...";
            this.BtnNuevaListaPrecio.UseVisualStyleBackColor = true;
            this.BtnNuevaListaPrecio.Click += new System.EventHandler(this.BtnNuevaListaPrecio_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(519, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 20);
            this.label5.TabIndex = 168;
            this.label5.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(498, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 167;
            this.label4.Text = "Campos Obligatorios (*)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(519, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 20);
            this.label7.TabIndex = 169;
            this.label7.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(548, 188);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 20);
            this.label8.TabIndex = 170;
            this.label8.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(229, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 20);
            this.label10.TabIndex = 171;
            this.label10.Text = "*";
            // 
            // BtnCalcular
            // 
            this.BtnCalcular.Location = new System.Drawing.Point(250, 185);
            this.BtnCalcular.Name = "BtnCalcular";
            this.BtnCalcular.Size = new System.Drawing.Size(75, 23);
            this.BtnCalcular.TabIndex = 173;
            this.BtnCalcular.Text = "Calcular";
            this.BtnCalcular.UseVisualStyleBackColor = true;
            this.BtnCalcular.Click += new System.EventHandler(this.BtnCalcular_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 174;
            this.label9.Text = "Fecha";
            this.label9.Visible = false;
            // 
            // _00032_ABM_Precio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 237);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.BtnCalcular);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BtnNuevaListaPrecio);
            this.Controls.Add(this.BtnNuevoArticulo);
            this.Controls.Add(this.cmbArticulo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudPrecioPublico);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudPrecioCosto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbListaPrecios);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFechaActualizacion);
            this.Name = "_00032_ABM_Precio";
            this.Text = "Precio (Alta, Baja y Modificación)";
            this.Controls.SetChildIndex(this.dtpFechaActualizacion, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cmbListaPrecios, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.nudPrecioCosto, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.nudPrecioPublico, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cmbArticulo, 0);
            this.Controls.SetChildIndex(this.BtnNuevoArticulo, 0);
            this.Controls.SetChildIndex(this.BtnNuevaListaPrecio, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.BtnCalcular, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioPublico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioCosto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbArticulo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudPrecioPublico;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudPrecioCosto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbListaPrecios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaActualizacion;
        private System.Windows.Forms.Button BtnNuevoArticulo;
        private System.Windows.Forms.Button BtnNuevaListaPrecio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button BtnCalcular;
        private System.Windows.Forms.Label label9;
    }
}