namespace Presentacion.Core.VentaSalon
{
    partial class _00078_UnirMesas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_00078_UnirMesas));
            this.dgvgrilla = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnUnirMesas = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtFormaPago = new System.Windows.Forms.ToolStripLabel();
            this.cmbSalon = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cmbMesa = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAgregarMesa = new System.Windows.Forms.ToolStripButton();
            this.btnEliminarMesa = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvgrilla)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvgrilla
            // 
            this.dgvgrilla.AllowUserToAddRows = false;
            this.dgvgrilla.AllowUserToDeleteRows = false;
            this.dgvgrilla.BackgroundColor = System.Drawing.Color.White;
            this.dgvgrilla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvgrilla.Location = new System.Drawing.Point(0, 84);
            this.dgvgrilla.MultiSelect = false;
            this.dgvgrilla.Name = "dgvgrilla";
            this.dgvgrilla.ReadOnly = true;
            this.dgvgrilla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvgrilla.Size = new System.Drawing.Size(897, 358);
            this.dgvgrilla.TabIndex = 14;
            this.dgvgrilla.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvgrilla_RowEnter);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUnirMesas,
            this.btnCancelar,
            this.toolStripSeparator1,
            this.txtFormaPago,
            this.cmbSalon,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.cmbMesa,
            this.toolStripSeparator3,
            this.btnAgregarMesa,
            this.btnEliminarMesa});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(897, 54);
            this.toolStrip1.TabIndex = 209;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnUnirMesas
            // 
            this.btnUnirMesas.ForeColor = System.Drawing.Color.Black;
            this.btnUnirMesas.Image = ((System.Drawing.Image)(resources.GetObject("btnUnirMesas.Image")));
            this.btnUnirMesas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnirMesas.Name = "btnUnirMesas";
            this.btnUnirMesas.Size = new System.Drawing.Size(69, 51);
            this.btnUnirMesas.Text = "Unir Mesas";
            this.btnUnirMesas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUnirMesas.Click += new System.EventHandler(this.btnUnirMesas_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(57, 51);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // txtFormaPago
            // 
            this.txtFormaPago.ForeColor = System.Drawing.Color.Black;
            this.txtFormaPago.Name = "txtFormaPago";
            this.txtFormaPago.Size = new System.Drawing.Size(90, 51);
            this.txtFormaPago.Text = "Filtrar por Salon";
            // 
            // cmbSalon
            // 
            this.cmbSalon.BackColor = System.Drawing.Color.White;
            this.cmbSalon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSalon.Name = "cmbSalon";
            this.cmbSalon.Size = new System.Drawing.Size(150, 54);
            this.cmbSalon.SelectedIndexChanged += new System.EventHandler(this.cmbSalon_SelectedIndexChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 51);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.BackColor = System.Drawing.Color.White;
            this.toolStripLabel2.ForeColor = System.Drawing.Color.Black;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(98, 51);
            this.toolStripLabel2.Text = "Seleccionar Mesa";
            // 
            // cmbMesa
            // 
            this.cmbMesa.BackColor = System.Drawing.Color.White;
            this.cmbMesa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMesa.Name = "cmbMesa";
            this.cmbMesa.Size = new System.Drawing.Size(100, 54);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 54);
            // 
            // btnAgregarMesa
            // 
            this.btnAgregarMesa.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregarMesa.Image")));
            this.btnAgregarMesa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgregarMesa.Name = "btnAgregarMesa";
            this.btnAgregarMesa.Size = new System.Drawing.Size(84, 51);
            this.btnAgregarMesa.Text = "Agregar Mesa";
            this.btnAgregarMesa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAgregarMesa.Click += new System.EventHandler(this.btnAgregarMesa_Click);
            // 
            // btnEliminarMesa
            // 
            this.btnEliminarMesa.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminarMesa.Image")));
            this.btnEliminarMesa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminarMesa.Name = "btnEliminarMesa";
            this.btnEliminarMesa.Size = new System.Drawing.Size(85, 51);
            this.btnEliminarMesa.Text = "Eliminar Mesa";
            this.btnEliminarMesa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEliminarMesa.Click += new System.EventHandler(this.btnEliminarMesa_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.OrangeRed;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 6);
            this.panel1.TabIndex = 210;
            // 
            // _00078_UnirMesas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(897, 442);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgvgrilla);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(913, 481);
            this.MinimumSize = new System.Drawing.Size(913, 481);
            this.Name = "_00078_UnirMesas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Union de Mesas";
            this.Load += new System.EventHandler(this._00078_UnirMesas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvgrilla)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.DataGridView dgvgrilla;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnUnirMesas;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel txtFormaPago;
        private System.Windows.Forms.ToolStripComboBox cmbSalon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cmbMesa;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnAgregarMesa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton btnEliminarMesa;
    }
}