﻿using System;
using System.Windows.Forms;

namespace Presentacion.FormularioBase
{
    public partial class FormularioConsulta : FormularioBase
    {
        protected long? EntidadId;
        protected bool PuedeEjecutarComando;
        protected object EntidadSeleccionada;

        public FormularioConsulta()
        {
            InitializeComponent();

            btnImprimir.Visible = false;

            // Asigncacion de Imagenes a Botones
            btnNuevo.Image = Constantes.Imagen.BotonNuevo;
            btnEliminar.Image = Constantes.Imagen.BotonEliminar;
            btnModificar.Image = Constantes.Imagen.BotonModificar;
            btnActualizar.Image = Constantes.Imagen.BotonActualizar;
            btnImprimir.Image = Constantes.Imagen.BotonImprimir;
            btnSalir.Image = Constantes.Imagen.BotonSalir;
            imgBuscar.Image = Constantes.Imagen.Buscar;

            // Asignamos los Colores
            menuAccesoRapido.BackColor = Constantes.Color.ColorMenu;
            btnNuevo.ForeColor = Constantes.Color.ColorLetraMenu;
            btnEliminar.ForeColor = Constantes.Color.ColorLetraMenu;
            btnModificar.ForeColor = Constantes.Color.ColorLetraMenu;
            btnActualizar.ForeColor = Constantes.Color.ColorLetraMenu;
            btnImprimir.ForeColor = Constantes.Color.ColorLetraMenu;
            btnSalir.ForeColor = Constantes.Color.ColorLetraMenu;

            // Asignamos el Evento
            txtBuscar.Enter += Control_Enter;
            txtBuscar.Leave += Control_Leave;

            // Inicializacion de Variables/Atributos
            EntidadId = null;
            PuedeEjecutarComando = false;

            AsignarEventoEnterLeave(this);
        }

        private bool HayDatosCargados()
        {
            return dgvGrilla.RowCount > 0;
        }

        private void BtnSalir_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        // =========================================================== //

        private void BtnNuevo_Click(object sender, System.EventArgs e)
        {
            EjecutarNuevo();
        }

        public virtual void EjecutarNuevo()
        {
            
        }

        // =========================================================== //

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            EjecutarEliminar();
        }

        public virtual void EjecutarEliminar()
        {
            if (HayDatosCargados())
            {
                if (!EntidadId.HasValue)
                {
                    MessageBox.Show(@"Por favor seleccione un registro.");
                    PuedeEjecutarComando = false;
                    return;
                }

                PuedeEjecutarComando = true;
            }
            else
            {
                MessageBox.Show(@"No hay Datos cargados");
            }
        }

        // =========================================================== //

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            EjecutarModificar();
        }

        public virtual void EjecutarModificar()
        {
            if (HayDatosCargados())
            {
                if (!EntidadId.HasValue)
                {
                    MessageBox.Show(@"Por favor seleccione un registro.");
                    PuedeEjecutarComando = false;
                    return;
                }

                PuedeEjecutarComando = true;
            }
            else
            {
                MessageBox.Show(@"No hay Datos cargados");
            }
        }

        // =========================================================== //

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, string.Empty);
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, txtBuscar.Text);
        }

        public virtual void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            
        }

        private void DgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            RowEnter(e);
        }

        public virtual void RowEnter(DataGridViewCellEventArgs e)
        {
            if (HayDatosCargados())
            {
                EntidadId = (long?) dgvGrilla["Id", e.RowIndex].Value;
                EntidadSeleccionada = dgvGrilla.Rows[e.RowIndex].DataBoundItem;
            }
            else
            {
                EntidadId = null;
                EntidadSeleccionada = null;
            }
        }

        private void FormularioConsulta_Load(object sender, EventArgs e)
        {
            EjecutarLoadFormulario();
            FormatearGrilla(dgvGrilla);
        }

        public virtual void EjecutarLoadFormulario()
        {
            ActualizarDatos(dgvGrilla, string.Empty);
        }

        // =========================================================== //

        public virtual void FormatearGrilla(DataGridView grilla)
        {
            for (var i = 0; i < grilla.ColumnCount; i++)
            {
                grilla.Columns[i].Visible = false;
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                EjecutarNuevo();
            }

            if (keyData == Keys.F2)
            {
                EjecutarEliminar();

            }
            if (keyData== Keys.F3)
            {
                EjecutarModificar();
            }
            if (keyData == Keys.F4)
            {
                ActualizarDatos(dgvGrilla, string.Empty);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
