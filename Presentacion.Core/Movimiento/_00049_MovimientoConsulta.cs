using Presentacion.FormularioBase;
using Presentacion.Helpers;
using Servicios.Core.Movimiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion.Core.Movimiento
{
    public partial class _00049_MovimientoConsulta : FormularioConsulta
    {
        private readonly IMovimientoServicio _movimientoServicio;

        public _00049_MovimientoConsulta()
            : this(new MovimientoServicio())

        {
            InitializeComponent();

            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;          
            btnEliminar.Enabled = false;

        }

        public _00049_MovimientoConsulta(IMovimientoServicio movimientoServicio)
        {
            _movimientoServicio = movimientoServicio;
        }
        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _movimientoServicio.Obtener(cadenaBuscar);
        }


        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);


            grilla.Columns["Fecha"].Visible = true;
            grilla.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Fecha"].HeaderText = @"Fecha";
            grilla.Columns["Fecha"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Descripcion";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Descripcion"].Visible = true;

            grilla.Columns["TipoFormaPago"].HeaderText = @"Tipo Forma Pago";
            grilla.Columns["TipoFormaPago"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["TipoFormaPago"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["TipoFormaPago"].Visible = true;

            grilla.Columns["Monto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Monto"].HeaderText = @"Monto";
            grilla.Columns["Monto"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Monto"].Visible = true;

            grilla.Columns["TipoMovimiento"].HeaderText = @"TipoMovimiento";
            grilla.Columns["TipoMovimiento"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["TipoMovimiento"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["TipoMovimiento"].Visible = true;





        }
    }
}
