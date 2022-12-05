using System;
using Presentacion.Helpers;
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
    public partial class _00069_CerrarCaja : Form
    {
        private readonly ICajaServicio _cajaServicio = new CajaServicio();

        public _00069_CerrarCaja()
        {
            InitializeComponent();

            nudMontoApertura.Enabled = false;
            nudMontoSistema.Enabled = false;
            nudCierre.Enabled = true;

            nudCierre.KeyPress += Validacion.NoSimbolos;
            nudCierre.KeyPress += Validacion.NoLetras;
        }

        private void BtnCerrarCaja_Click(object sender, EventArgs e)
        {
            if (_cajaServicio.HayMesasAbiertas())
            {
                MessageBox.Show($"No se puede cerrar Caja porque hay mesas abiertas o mesas reservadas. Por favor cierre toda las mesas.");
                return;
            }
            else
            {
                CajaDto cajaAbierta = _cajaServicio.ObtenerCajaAbierta();

                var ultimaCajaId = _cajaServicio.CerrarCaja(nudCierre.Value, cajaAbierta.FechaApertura, cajaAbierta.MontoApertura);

                var cajaCerrada = _cajaServicio.ObtenerDatosCajaCerrada(ultimaCajaId);


                MessageBox.Show($"La caja se Abrio con: ${cajaCerrada.MontoApertura} y se cerro con: " +
                    $"${cajaCerrada.MontoCierre}. El monto en el sistema es: ${cajaCerrada.MontoSistema}. " +
                    $"La diferencia es: ${cajaCerrada.Diferencia}.");

                _cajaServicio.CrearDetalleCaja(ultimaCajaId, cajaCerrada.MontoCierre);

                var fMovimientosCaja = new _00070_MovimientosCaja(cajaCerrada.Id);
                fMovimientosCaja.ShowDialog();

                MessageBox.Show("La Caja fue Cerrada con Exito");

                this.Close();
            }
        }

        private void _00069_CerrarCaja_Load(object sender, EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja())
            {
                var caja = _cajaServicio.ObtenerCajaAbierta();

                dtpFechaApertura.Value = caja.FechaApertura;
                nudMontoApertura.Value = caja.MontoApertura;
                nudCierre.Value = caja.MontoCierre;
                nudMontoSistema.Value = caja.MontoSistema;
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
