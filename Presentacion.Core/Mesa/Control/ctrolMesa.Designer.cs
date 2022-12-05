namespace Presentacion.Core.Mesa.Control
{
    partial class ctrolMesa
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblNumeroMesa = new System.Windows.Forms.Label();
            this.menuCtrol = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuAbrirMesa = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCerrarMesa = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.menuCancelarReserva = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCtrol.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblNumeroMesa
            // 
            this.lblNumeroMesa.ContextMenuStrip = this.menuCtrol;
            this.lblNumeroMesa.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNumeroMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroMesa.ForeColor = System.Drawing.Color.White;
            this.lblNumeroMesa.Location = new System.Drawing.Point(0, 0);
            this.lblNumeroMesa.Name = "lblNumeroMesa";
            this.lblNumeroMesa.Size = new System.Drawing.Size(91, 57);
            this.lblNumeroMesa.TabIndex = 0;
            this.lblNumeroMesa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNumeroMesa.DoubleClick += new System.EventHandler(this.LblNumeroMesa_DoubleClick);
            // 
            // menuCtrol
            // 
            this.menuCtrol.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbrirMesa,
            this.menuCerrarMesa,
            this.menuCancelarReserva});
            this.menuCtrol.Name = "menuCtrol";
            this.menuCtrol.Size = new System.Drawing.Size(181, 92);
            // 
            // menuAbrirMesa
            // 
            this.menuAbrirMesa.Name = "menuAbrirMesa";
            this.menuAbrirMesa.Size = new System.Drawing.Size(180, 22);
            this.menuAbrirMesa.Text = "Abrir Mesa";
            this.menuAbrirMesa.Click += new System.EventHandler(this.MenuAbrirMesa_Click);
            // 
            // menuCerrarMesa
            // 
            this.menuCerrarMesa.Name = "menuCerrarMesa";
            this.menuCerrarMesa.Size = new System.Drawing.Size(180, 22);
            this.menuCerrarMesa.Text = "Cerrar Mesa";
            this.menuCerrarMesa.Click += new System.EventHandler(this.menuCerrarMesa_Click);
            // 
            // lblPrecio
            // 
            this.lblPrecio.ContextMenuStrip = this.menuCtrol;
            this.lblPrecio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.ForeColor = System.Drawing.Color.White;
            this.lblPrecio.Location = new System.Drawing.Point(0, 57);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(91, 32);
            this.lblPrecio.TabIndex = 1;
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuCancelarReserva
            // 
            this.menuCancelarReserva.Name = "menuCancelarReserva";
            this.menuCancelarReserva.Size = new System.Drawing.Size(180, 22);
            this.menuCancelarReserva.Text = "Cancelar Reserva";
            this.menuCancelarReserva.Click += new System.EventHandler(this.menuCancelarReserva_Click);
            // 
            // ctrolMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.lblNumeroMesa);
            this.Name = "ctrolMesa";
            this.Size = new System.Drawing.Size(91, 89);
            this.menuCtrol.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNumeroMesa;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.ContextMenuStrip menuCtrol;
        private System.Windows.Forms.ToolStripMenuItem menuAbrirMesa;
        private System.Windows.Forms.ToolStripMenuItem menuCerrarMesa;
        private System.Windows.Forms.ToolStripMenuItem menuCancelarReserva;
    }
}
