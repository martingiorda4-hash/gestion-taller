using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestion_taller
{
    public partial class AgregarClientes : Form
    {
        private Trabajo editar;
        public AgregarClientes()
        {
            InitializeComponent();
            editar = null;
    
        }
        public AgregarClientes(Trabajo t)
        {
            InitializeComponent();
            this.editar = t; 
        }
        private void AgregarClientes_Load(object sender, EventArgs e)
        {
            if(editar != null)
            {
                txtCliente.Text = editar.Cliente;
                txtfecha.Value = editar.Fecha;
                txtAuto.Text = editar.Vehiculo.ToString();
                txtDescripcion.Text = editar.Descripcion;
                txtPrecio.Text = editar.Precio.ToString();

                btnRegistrar.Text = "Guardar cambios";
            }
           
        }

        

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
           if(!ValidarCampos())return;
            DateTime fecha = txtfecha.Value;
            string cliente = txtCliente.Text;
            string vehiculo = txtAuto.Text;
            string descripcion = txtDescripcion.Text;
            int precio = int.Parse(txtPrecio.Text);


            Controller controller = new Controller();

            if(editar == null)
            {
                Trabajo t = new Trabajo(fecha,cliente,vehiculo,descripcion,precio);
                controller.AgregarTrabajo(t);
                MessageBox.Show("Trabajo registrado!","Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                editar.Fecha = fecha;
                editar.Cliente = cliente;
                editar.Vehiculo = vehiculo;
                editar.Descripcion = descripcion;
                editar.Precio = precio;

                controller.Editar(editar);
                MessageBox.Show("Trabajo editado!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool ValidarCampos()
        {
            if(txtCliente.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el cliente", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtCliente.Focus();
                return false;
            }
            if(txtAuto.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el auto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAuto.Focus();
                return false;
            }
            if (txtDescripcion.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar la descripcion del trabajo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescripcion.Focus();
                return false;
            }
            if (txtPrecio.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar el precio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
                return false;
            }
            if (!int.TryParse(txtPrecio.Text.Trim(), out int precio))
            {
                MessageBox.Show("Debe ingresar el precio en formato correcto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
                return false;
            }
            return true;
        }
    }
}
