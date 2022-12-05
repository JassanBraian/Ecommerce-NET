namespace Presentacion.Core.Reserva
{
    partial class _00038_Reserva
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
            this.cmbFiltroActual = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbFiltroActual
            // 
            this.cmbFiltroActual.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFiltroActual.FormattingEnabled = true;
            this.cmbFiltroActual.Location = new System.Drawing.Point(8, 65);
            this.cmbFiltroActual.Name = "cmbFiltroActual";
            this.cmbFiltroActual.Size = new System.Drawing.Size(193, 24);
            this.cmbFiltroActual.TabIndex = 4;
            this.cmbFiltroActual.SelectedIndexChanged += new System.EventHandler(this.cmbFiltroActual_SelectedIndexChanged);
            // 
            // _00038_Reserva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.cmbFiltroActual);
            this.Name = "_00038_Reserva";
            this.Text = "Reservas";
            this.Load += new System.EventHandler(this._00038_Reserva_Load);
            this.Controls.SetChildIndex(this.cmbFiltroActual, 0);
            ((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFiltroActual;
    }
}