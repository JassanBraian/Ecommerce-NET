using Presentacion.FormularioBase;
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
    public partial class _00043_ConsultaCaja : FormularioConsulta

    {
        private readonly ICajaServicio _cajaServicio;

        public _00043_ConsultaCaja()
            : this(new CajaServicio())
        {
            InitializeComponent();


            
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        public _00043_ConsultaCaja(ICajaServicio cajaServicio)
        {
            _cajaServicio = cajaServicio;
        }
        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _cajaServicio.Obtener(cadenaBuscar);
        }


        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);
            grilla.Columns["MontoApertura"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["MontoApertura"].HeaderText = @"MontoApertura";
            grilla.Columns["MontoApertura"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["MontoApertura"].Visible = true;

            grilla.Columns["MontoCierre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["MontoCierre"].HeaderText = @"MontoCierre";
            grilla.Columns["MontoCierre"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["MontoCierre"].Visible = true;

            grilla.Columns["MontoSistema"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["MontoSistema"].HeaderText = @"MontoSistema";
            grilla.Columns["MontoSistema"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["MontoSistema"].Visible = true;

            grilla.Columns["Diferencia"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Diferencia"].HeaderText = @"Diferencia";
            grilla.Columns["Diferencia"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Diferencia"].Visible = true;


        }

        public override void EjecutarNuevo()
        {
            var fCaja = new _00042_AbrirCaja();
            fCaja.ShowDialog();


        }

    }
}
