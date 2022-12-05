using Presentacion.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Caja;
using XCommerce.Servicio.Core.Caja.DTOs;

namespace Presentacion.Core.Caja
{
    public partial class _00042_AbrirCaja : Form
    {
        private readonly ICajaServicio _cajaServicio;
        public _00042_AbrirCaja()
        {
            InitializeComponent();
            _cajaServicio = new CajaServicio();

            nudMontoApertura.Enabled = true;
            nudMontoSistema.Enabled = false;
            nudCierre.Enabled = false;

            nudMontoApertura.KeyPress += Validacion.NoSimbolos;
            nudMontoApertura.KeyPress += Validacion.NoLetras;

        }

        private void BtnAbrirCerrar_Click_1(object sender, EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja())
            {
                MessageBox.Show("No puede Abrir Caja debido a que ya se encuentra Abierta");

                return;
            }

            _cajaServicio.AbrirCaja(nudMontoApertura.Value, dtpFechaApertura.Value, Validacion.UsuarioLogeado);

            MessageBox.Show("La Caja fue Abierta Correctamente");

            this.Close();
        }

        private void nudMontoApertura_ValueChanged_1(object sender, EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja() == false)
            {
                nudMontoSistema.Value = nudMontoApertura.Value;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult result = MessageBox.Show("Esta seguro que desea Salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
