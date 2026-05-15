using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestion_taller
{
    public partial class Trabajos : Form
    {
        public Trabajos()
        {
            InitializeComponent();
           dgv.AutoGenerateColumns = false;
            
            //botonEditar.Enabled = false;
            //botonEliminar.Enabled = false;
            
        }
        private void Trabajos_Load(object sender, EventArgs e)
        {
            RefrescarGrilla();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
             
               Trabajo trabajoSeleccionado = (Trabajo)dgv.CurrentRow.DataBoundItem;
                

               AgregarClientes agregarClientes = new AgregarClientes(trabajoSeleccionado);
                agregarClientes.ShowDialog();
            

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv != null)
            {

                Trabajo trabajoSeleccionado = (Trabajo)dgv.CurrentRow.DataBoundItem;
                
                DialogResult respuesta = MessageBox.Show("Estas seguro de que querés eliminar este trabajo?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (respuesta == DialogResult.Yes)
                {
                    Controller controller = new Controller();
                    controller.Eliminar(trabajoSeleccionado.Id);
                    MessageBox.Show("Trabajo eliminado!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarGrilla();
                }
                
            }
        }
        public void RefrescarGrilla()
        {
           
            Controller controller = new Controller();
           
            dgv.DataSource = controller.Listar();

            dgv.ClearSelection();
            botonEditar.Enabled = false;
            botonEliminar.Enabled = false;

            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarTrabajosFecha();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            
            Controller controller = new Controller();
            dgv.DataSource = controller.Listar();

            
            

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            int total = controller.TotalDelDia(fechaDesde.Value);

            MessageBox.Show($"Total del dia: ${total}", "Total", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        private void BuscarTrabajosFecha()
        {
           
            DateTime desde = fechaDesde.Value.Date;
            DateTime hasta = fechaHasta.Value.Date.AddDays(1).AddSeconds(-1);

                Controller controller = new Controller();
                var _fecha = controller.ListarPorFecha(desde, hasta);
            if(_fecha.Count == 0)
            {
                MessageBox.Show("No hay trabajos en esa fecha", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dgv.DataSource = _fecha;
            }
           
            
            
             
        }
       

       
        private void txtbuscador_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if(txtbuscador.Text.Trim() == "")
                {
                    MessageBox.Show("Debe ingresar algo a buscar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Controller controller = new Controller();
                dgv.DataSource = controller.Buscador(txtbuscador.Text);
            }
            
        }

        private void botonBuscador_Click(object sender, EventArgs e)
        {
            
            if(txtbuscador.Text.Trim() == "")
            {
                MessageBox.Show("Debe ingresar algo a buscar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtbuscador.Focus();
                return;
            } 
            Controller controller = new Controller();
            dgv.DataSource = controller.Buscador(txtbuscador.Text);
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if(dgv.CurrentRow != null && dgv.CurrentRow.Index >= 0)
            {
                botonEditar.Enabled = true;
                botonEliminar.Enabled = true;
            }
            else
            {
                botonEditar.Enabled = false;
                botonEliminar.Enabled = false;
            }
           
        }
    }
}
