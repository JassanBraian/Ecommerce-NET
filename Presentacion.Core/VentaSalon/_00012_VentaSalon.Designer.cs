using System.Windows.Forms;

namespace Presentacion.Core.VentaSalon
{
    partial class _00012_VentaSalon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_00012_VentaSalon));
            this.ctrolMesa1 = new Presentacion.Core.Mesa.Control.ctrolMesa();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevoSalon = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNuevaMesa = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNuevaReserva = new System.Windows.Forms.ToolStripButton();
            this.btnReservas = new System.Windows.Forms.ToolStripButton();
            this.btnUnirMesas = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrolMesa1
            // 
            this.ctrolMesa1.Location = new System.Drawing.Point(34, 105);
            this.ctrolMesa1.MesaId = ((long)(0));
            this.ctrolMesa1.Name = "ctrolMesa1";
            this.ctrolMesa1.Size = new System.Drawing.Size(80, 80);
            this.ctrolMesa1.TabIndex = 4;
            // 
            // btnSalir
            // 
            this.btnSalir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(36, 51);
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSalir,
            this.btnNuevoSalon,
            this.toolStripSeparator1,
            this.btnNuevaMesa,
            this.btnUnirMesas,
            this.toolStripSeparator2,
            this.btnNuevaReserva,
            this.btnReservas});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 54);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevoSalon
            // 
            this.btnNuevoSalon.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevoSalon.Image")));
            this.btnNuevoSalon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevoSalon.Name = "btnNuevoSalon";
            this.btnNuevoSalon.Size = new System.Drawing.Size(78, 51);
            this.btnNuevoSalon.Text = "Nuevo Salon";
            this.btnNuevoSalon.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevoSalon.Click += new System.EventHandler(this.btnNuevoSalon_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // btnNuevaMesa
            // 
            this.btnNuevaMesa.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaMesa.Image")));
            this.btnNuevaMesa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevaMesa.Name = "btnNuevaMesa";
            this.btnNuevaMesa.Size = new System.Drawing.Size(76, 51);
            this.btnNuevaMesa.Text = "Nueva Mesa";
            this.btnNuevaMesa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevaMesa.Click += new System.EventHandler(this.btnNuevaMesa_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 54);
            // 
            // btnNuevaReserva
            // 
            this.btnNuevaReserva.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevaReserva.Image")));
            this.btnNuevaReserva.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevaReserva.Name = "btnNuevaReserva";
            this.btnNuevaReserva.Size = new System.Drawing.Size(88, 51);
            this.btnNuevaReserva.Text = "Nueva Reserva";
            this.btnNuevaReserva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNuevaReserva.Click += new System.EventHandler(this.btnNuevaReserva_Click);
            // 
            // btnReservas
            // 
            this.btnReservas.Image = ((System.Drawing.Image)(resources.GetObject("btnReservas.Image")));
            this.btnReservas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReservas.Name = "btnReservas";
            this.btnReservas.Size = new System.Drawing.Size(95, 51);
            this.btnReservas.Text = "Reservas del Dia";
            this.btnReservas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReservas.Click += new System.EventHandler(this.btnReservas_Click);
            // 
            // btnUnirMesas
            // 
            this.btnUnirMesas.Image = ((System.Drawing.Image)(resources.GetObject("btnUnirMesas.Image")));
            this.btnUnirMesas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnirMesas.Name = "btnUnirMesas";
            this.btnUnirMesas.Size = new System.Drawing.Size(69, 51);
            this.btnUnirMesas.Text = "Unir Mesas";
            this.btnUnirMesas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUnirMesas.Click += new System.EventHandler(this.btnUnirMesas_Click);
            // 
            // _00012_VentaSalon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ctrolMesa1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_00012_VentaSalon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Salon";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this._00012_VentaSalon_Activated);
            this.Load += new System.EventHandler(this._00012_VentaSalon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Mesa.Control.ctrolMesa ctrolMesa1;
        private ToolStripButton btnSalir;
        private ToolStrip toolStrip1;
        private ToolStripButton btnNuevoSalon;
        private ToolStripButton btnNuevaMesa;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnNuevaReserva;
        private ToolStripButton btnReservas;
        private ToolStripButton btnUnirMesas;
    }
}