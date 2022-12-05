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
using XCommerce.Servicio.Seguridad.Usuario;
using XCommerce.Servicio.Seguridad.Usuario.DTOs;

namespace Presentacion.Seguridad.Usuarios
{
    public partial class _00009_Usuarios : FormularioBase.FormularioBase
    {
        private readonly IUsuarioServicio _usuarioServicio;
        private object _entidad;

        public _00009_Usuarios()
        {
            InitializeComponent();

            _usuarioServicio = new UsuarioServicio();
          
            _entidad = null;
        }

        private void ActualizarDatos(string cadenaBuscar)
        {
            dgvGrilla.DataSource = _usuarioServicio.Obtener(!string.IsNullOrEmpty(cadenaBuscar)
                ? cadenaBuscar 
                : string.Empty);

            FormatearGrilla();
        }

        private void FormatearGrilla()
        {
            for (int i = 0; i < dgvGrilla.ColumnCount; i++)
            {
                dgvGrilla.Columns[i].Visible = false;
            }

            dgvGrilla.Columns["ApyNom"].Visible = true;
            dgvGrilla.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";
            dgvGrilla.Columns["ApyNom"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvGrilla.Columns["Nombre"].Visible = true;
            dgvGrilla.Columns["Nombre"].Width = 200;
            dgvGrilla.Columns["Nombre"].HeaderText = @"Usuario";
            dgvGrilla.Columns["Nombre"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgvGrilla.Columns["EstaBloqueadoStr"].Visible = true;
            dgvGrilla.Columns["EstaBloqueadoStr"].Width = 100;
            dgvGrilla.Columns["EstaBloqueadoStr"].HeaderText = @"Bloqueado";
            dgvGrilla.Columns["EstaBloqueadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrilla.Columns["EstaBloqueadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void _00009_Usuarios_Load(object sender, EventArgs e)
        {
            ActualizarDatos(string.Empty);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(txtBuscar.Text);
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                ActualizarDatos(txtBuscar.Text);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(string.Empty);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        
            private void btnNuevo_Click(object sender, EventArgs e)
            {
                if (_entidad == null) return;

                var usuarioSeleccionado = (UsuarioDto)_entidad;

                _usuarioServicio.Crear(usuarioSeleccionado.PersonaId,
                    usuarioSeleccionado.ApellidoPersona, usuarioSeleccionado.NombrePersona);

                ActualizarDatos(string.Empty);

            }


        

        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _entidad = dgvGrilla.RowCount > 0
                ? dgvGrilla.Rows[e.RowIndex].DataBoundItem
                : null;
        }

        private void btnBloqDesb_Click(object sender, EventArgs e)
        {
            if (_entidad == null || ((UsuarioDto)_entidad).Nombre != @"NO ASIGNADO") return;

            var usuarioSeleccionado = (UsuarioDto)_entidad;

            _usuarioServicio.CambiarEstado(usuarioSeleccionado.Nombre,
                !usuarioSeleccionado.EstaBloqueado); // siempre pasa el contrario

            var mensaje = usuarioSeleccionado.EstaBloqueado 
                ? @"El usuario ha sido Desbloqueado"
                : @"El usuario ha sido Bloqueado";

            ActualizarDatos(string.Empty);

            MessageBox.Show(mensaje, @"Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
