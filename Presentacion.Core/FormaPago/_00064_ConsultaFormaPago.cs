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
using XCommerce.Servicio.Core.FormaDePago;

namespace Presentacion.Core.FormaPago
{
    public partial class _00064_ConsultaFormaPago : FormularioConsulta
    {
        private IFormaDePago FormaDePagoServicio;

        public _00064_ConsultaFormaPago()
            : this(new FormaDePagoServicio())
        {
            InitializeComponent();


            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        public _00064_ConsultaFormaPago(IFormaDePago formadepagoservicio)
        {
            FormaDePagoServicio = formadepagoservicio;
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = FormaDePagoServicio.Obtener(cadenaBuscar);
        }


        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);



            grilla.Columns["Monto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Monto"].HeaderText = @"Monto";
            grilla.Columns["Monto"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Monto"].Visible = true;

            

            grilla.Columns["TipoFormaDePago"].HeaderText = @"Forma de Pago";
            grilla.Columns["TipoFormaDePago"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["TipoFormaDePago"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["TipoFormaDePago"].Visible = true;
            
        }
    }
}
