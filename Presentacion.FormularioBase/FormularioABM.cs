using Presentacion.Helpers;
using System;
using System.Windows.Forms;

namespace Presentacion.FormularioBase
{
    public partial class FormularioAbm : FormularioBase
    {
        // Declaracion de Variables / Atributos
        protected TipoOperacion TipoOperacion;
        protected long? EntidadId;

        public bool RealizoAlgunaOperacion { get; set; }

        //Constructor Principal
        public FormularioAbm()
        {
            InitializeComponent();
        }


        // Constructor Sobrecargado
        public FormularioAbm(TipoOperacion tipoOperacion, long? entidadId)
            : this() // => Constructor Principal
        {
            TipoOperacion = tipoOperacion;
            EntidadId = entidadId;

            RealizoAlgunaOperacion = false;

            AsignarImagenBotones();
        }

        private void AsignarImagenBotones()
        {
            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnEjecutar.Text = @"Eliminar";
                btnEjecutar.Image = Constantes.Imagen.BotonEliminar;
            }
            else
            {
                btnEjecutar.Text = @"Guardar";
                btnEjecutar.Image = Constantes.Imagen.BotonGuardar;
            }

            btnLimpiar.Image = Constantes.Imagen.BotonActualizar;
            btnSalir.Image = Constantes.Imagen.BotonSalir;
        }

        public virtual void FormularioABM_Load(object sender, EventArgs e)
        {
            if (TipoOperacion == TipoOperacion.Eliminar
                || TipoOperacion == TipoOperacion.Modificar)
                CargarDatos(EntidadId);
        }

        public virtual void BtnSalir_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        public virtual void btnEjecutar_Click(object sender, System.EventArgs e)
        {
            EjecutarComando();
            
        }

        public virtual void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Esta seguro de Limpiar los Datos", @"Atención", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Limpiar(this);
            }
        }
        
        public virtual void EjecutarComando()
        {
            switch (TipoOperacion)
            {
                case TipoOperacion.Nuevo:
                    if (EjecutarComandoNuevo())
                    {
                        MessageBox.Show(@"Los datos se Guardaron Correctamente.", @"Atención", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        Limpiar(this);
                        RealizoAlgunaOperacion = true;
                    }
                    break;
                case TipoOperacion.Eliminar:
                    if (EjecutarComandoEliminar())
                    {
                        MessageBox.Show(@"Los datos se Eliminaron Correctamente.", @"Atención", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        RealizoAlgunaOperacion = true;
                        this.Close();
                    }
                    break;
                case TipoOperacion.Modificar:
                    if (EjecutarComandoModificar())
                    {
                        MessageBox.Show(@"Los datos se Modificaron Correctamente.", @"Atención", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        RealizoAlgunaOperacion = true;
                        this.Close();
                    }
                    break;
            }
        }

        public virtual bool EjecutarComandoModificar()
        {
            return false;
        }

        public virtual bool EjecutarComandoEliminar()
        {
            return false;
        }

        public virtual bool EjecutarComandoNuevo()
        {
            return false;
        }
        
        public virtual void CargarDatos(long? entidadId)
        {

        }

        public virtual void Inicializador(long? entidadId)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F4)
            {
                if (EjecutarComandoNuevo())
                {
                    MessageBox.Show(@"Los datos se Guardaron Correctamente.", @"Atención", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    Limpiar(this);
                    RealizoAlgunaOperacion = true;
                }
            }

            if (keyData == Keys.F2)
            {
                if (MessageBox.Show(@"Esta seguro de Limpiar los Datos", @"Atención", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Limpiar(this);
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
